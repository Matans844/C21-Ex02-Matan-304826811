using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using C21_Ex02_Matan_304826811.Views;
using C21_Ex02_Matan_304826811.Toolkit;
using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.GameLogic
{
	public class Referee
	{
		public Player Winner { get; set; }

		public Board BoardToReferee { get; }

		public Referee(Board i_Board)
		{
			this.BoardToReferee = i_Board;
		}

		public bool IsGameDrawn(BoardCell i_LastDiscPlaced)
		{
			return this.isBoardExhausted() && !this.hasGameWinnerForBoard(i_LastDiscPlaced);
		}

		public bool IsGameFinished(BoardCell i_LastDiscPlaced)
		{
			return this.hasGameWinnerForBoard(i_LastDiscPlaced) || this.isBoardExhausted();
		}

		private bool isBoardExhausted()
		{
			return this.BoardToReferee.NumOfCellVacanciesInBoard == 0;

			// Another possibility was to loop over the available column vacancies.
			// It can be implemented with a foreach loop that can be upgraded to the following LINQ statement:
			// return this.BoardToReferee.NumOfCellVacanciesInColumn.All(i_ColumnVacancy => i_ColumnVacancy == 0);
		}

		private bool hasGameWinnerForBoard(BoardCell i_LastDiscPlaced)
		{
			bool hasWinnerConnection = false;

			foreach (eDirectionOfDiscConnection direction in Enum.GetValues(typeof(eDirectionOfDiscConnection)))
			{
				if (isLastDiscInConnectionValid(i_LastDiscPlaced, direction))
				{
					hasWinnerConnection = isWinningConnection(i_LastDiscPlaced, direction);

					if (hasWinnerConnection)
					{
						this.updateWinner(i_LastDiscPlaced);
						this.BoardToReferee.GameForBoard.GameUserInterfaceAdmin.MyMessageCreator.UpdateResultsMessage();
					}
				}
			}

			return hasWinnerConnection;
		}

		private void updateWinner(BoardCell i_WinningDiscPlaced)
		{
			this.Winner = (i_WinningDiscPlaced.CellType == eBoardCellType.XDisc)
							? this.BoardToReferee.GameForBoard.Player1WithXs
							: this.BoardToReferee.GameForBoard.Player2WithOs;
			this.BoardToReferee.BoardState = eBoardState.FinishedInWin;
		}

		private bool isWinningConnection(BoardCell i_FocalBoardCell, eDirectionOfDiscConnection i_DirectionOfConnection)
		{
			// To improve readability, 'var' type is used instead of the built-in types 'bool', 'uint'.
			var hasWinningConnection = false;
			var focalRow = i_FocalBoardCell.Row;
			var focalColumn = i_FocalBoardCell.Column;

			switch (i_DirectionOfConnection)
			{
				case eDirectionOfDiscConnection.Right:
					hasWinningConnection = i_FocalBoardCell.HasSameTypeAs(
						BoardToReferee.BoardCellMatrix[focalRow, focalColumn + 1],
						BoardToReferee.BoardCellMatrix[focalRow, focalColumn + 2],
						BoardToReferee.BoardCellMatrix[focalRow, focalColumn + 3]);
					break;

				case eDirectionOfDiscConnection.UpRight:
					hasWinningConnection = i_FocalBoardCell.HasSameTypeAs(
						BoardToReferee.BoardCellMatrix[focalRow - 1, focalColumn + 1],
						BoardToReferee.BoardCellMatrix[focalRow - 2, focalColumn + 2],
						BoardToReferee.BoardCellMatrix[focalRow - 3, focalColumn + 3]);
					break;

				case eDirectionOfDiscConnection.Up:
					hasWinningConnection = i_FocalBoardCell.HasSameTypeAs(
						BoardToReferee.BoardCellMatrix[focalRow - 1, focalColumn],
						BoardToReferee.BoardCellMatrix[focalRow - 2, focalColumn],
						BoardToReferee.BoardCellMatrix[focalRow - 3, focalColumn]);
					break;

				case eDirectionOfDiscConnection.UpLeft:
					hasWinningConnection = i_FocalBoardCell.HasSameTypeAs(
						BoardToReferee.BoardCellMatrix[focalRow - 1, focalColumn - 1],
						BoardToReferee.BoardCellMatrix[focalRow - 2, focalColumn - 2],
						BoardToReferee.BoardCellMatrix[focalRow - 3, focalColumn - 3]);
					break;

				case eDirectionOfDiscConnection.Left:
					hasWinningConnection = i_FocalBoardCell.HasSameTypeAs(
						BoardToReferee.BoardCellMatrix[focalRow, focalColumn - 1],
						BoardToReferee.BoardCellMatrix[focalRow, focalColumn - 2],
						BoardToReferee.BoardCellMatrix[focalRow, focalColumn - 3]);
					break;

				case eDirectionOfDiscConnection.DownLeft:
					hasWinningConnection = i_FocalBoardCell.HasSameTypeAs(
						BoardToReferee.BoardCellMatrix[focalRow + 1, focalColumn - 1],
						BoardToReferee.BoardCellMatrix[focalRow + 2, focalColumn - 2],
						BoardToReferee.BoardCellMatrix[focalRow + 3, focalColumn - 3]);
					break;

				case eDirectionOfDiscConnection.Down:
					hasWinningConnection = i_FocalBoardCell.HasSameTypeAs(
						BoardToReferee.BoardCellMatrix[focalRow + 1, focalColumn],
						BoardToReferee.BoardCellMatrix[focalRow + 2, focalColumn],
						BoardToReferee.BoardCellMatrix[focalRow + 3, focalColumn]);
					break;

				case eDirectionOfDiscConnection.DownRight:
					hasWinningConnection = i_FocalBoardCell.HasSameTypeAs(
						BoardToReferee.BoardCellMatrix[focalRow + 1, focalColumn + 1],
						BoardToReferee.BoardCellMatrix[focalRow + 2, focalColumn + 2],
						BoardToReferee.BoardCellMatrix[focalRow + 3, focalColumn + 3]);
					break;
			}

			return hasWinningConnection;
		}

		private bool isLastDiscInConnectionValid(BoardCell i_FocalBoardCell, eDirectionOfDiscConnection i_DirectionOfConnection)
		{
			bool isDistantDiscAvailable = false;
			uint focalRow = i_FocalBoardCell.Row;
			uint focalColumn = i_FocalBoardCell.Column;
			int numOfMatrixRows = BoardToReferee.BoardCellMatrix.GetLength(0);
			int numOfMatrixColumns = BoardToReferee.BoardCellMatrix.GetLength(1);

			switch (i_DirectionOfConnection)
			{
				case eDirectionOfDiscConnection.Right:
					isDistantDiscAvailable = numOfMatrixColumns > focalColumn + Game.k_LengthOfWinningConnection;
					break;

				case eDirectionOfDiscConnection.UpRight:
					isDistantDiscAvailable = (numOfMatrixColumns > focalColumn + Game.k_LengthOfWinningConnection) && (numOfMatrixRows > focalRow - Game.k_LengthOfWinningConnection);
					break;

				case eDirectionOfDiscConnection.Up:
					isDistantDiscAvailable = numOfMatrixRows > focalRow - Game.k_LengthOfWinningConnection;
					break;

				case eDirectionOfDiscConnection.UpLeft:
					isDistantDiscAvailable = (numOfMatrixColumns > focalColumn - Game.k_LengthOfWinningConnection) && (numOfMatrixRows > focalRow - Game.k_LengthOfWinningConnection);
					break;

				case eDirectionOfDiscConnection.Left:
					isDistantDiscAvailable = numOfMatrixColumns > focalColumn - Game.k_LengthOfWinningConnection;
					break;

				case eDirectionOfDiscConnection.DownLeft:
					isDistantDiscAvailable = (numOfMatrixColumns > focalColumn - Game.k_LengthOfWinningConnection) && (numOfMatrixRows > focalRow + Game.k_LengthOfWinningConnection);
					break;

				case eDirectionOfDiscConnection.Down:
					isDistantDiscAvailable = numOfMatrixRows > focalRow + Game.k_LengthOfWinningConnection;
					break;

				case eDirectionOfDiscConnection.DownRight:
					isDistantDiscAvailable = (numOfMatrixRows > focalRow + Game.k_LengthOfWinningConnection) && (numOfMatrixRows > focalRow + Game.k_LengthOfWinningConnection);
					break;
			}

			return isDistantDiscAvailable;
		}
	}

	// Counterclockwise
	public enum eDirectionOfDiscConnection
	{
		Right = 0,
		UpRight = 1,
		Up = 2,
		UpLeft = 3,
		Left = 4,
		DownLeft = 5,
		Down = 6,
		DownRight = 7
	}
}
