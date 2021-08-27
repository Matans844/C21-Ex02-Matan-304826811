using System;

using C21_Ex02_Matan_304826811.Players;

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
				// if (isLastDiscInConnectionValid(i_LastDiscPlaced, direction))
				// {
				// }
				hasWinnerConnection = isWinningConnection(i_LastDiscPlaced, direction);

				if (hasWinnerConnection)
				{
					this.updateWinner(i_LastDiscPlaced);
					this.BoardToReferee.GameForBoard.GameUserInterfaceAdmin.MyMessageCreator.UpdateResultsMessage();
				}
			}

			return hasWinnerConnection;
		}

		private void updateWinner(BoardCell i_WinningDiscPlaced)
		{
			this.BoardToReferee.BoardState = eBoardState.FinishedInWin;

			this.Winner = (i_WinningDiscPlaced.CellType == eBoardCellType.XDisc)
							? this.BoardToReferee.GameForBoard.Player1WithXs
							: this.BoardToReferee.GameForBoard.Player2WithOs;
		}

		private bool isWinningConnection(BoardCell i_FocalBoardCell, eDirectionOfDiscConnection i_DirectionOfConnection)
		{
			// The Console board is a rotation of the CellBoard Matrix:
			// 1. The matrix has cell 0,0 in the upper left.
			// 2. The console board has cell 0,0 in the bottom left.
			// Also consider that:
			// 1. A player receives indices from 1 to the width of the board.
			// 2. The matrix has column indices from 0 to with of the board minus 1.
			bool hasWinningConnection = false;
			uint focalRow = i_FocalBoardCell.Row;
			uint focalColumn = i_FocalBoardCell.Column;

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

		/*private bool isLastDiscInConnectionValid(BoardCell i_FocalBoardCell, eDirectionOfDiscConnection i_DirectionOfConnection)
		{

		// This method is wrong.
		// I can insert a winning disc in the middle of a combination.
		// Checking what happens in radius 4 is not good enough

			bool isDistantDiscAvailable = false;
			uint focalRow = i_FocalBoardCell.Row;
			uint focalColumn = i_FocalBoardCell.Column;
			int indexOfLastMatrixRow = BoardToReferee.BoardCellMatrix.GetLength(0) - Board.k_TransformBoardToMatrixIndicesWith1;
			int indexOfLastMatrixColumn = BoardToReferee.BoardCellMatrix.GetLength(1) - Board.k_TransformBoardToMatrixIndicesWith1;

			switch (i_DirectionOfConnection)
			{
				case eDirectionOfDiscConnection.Right:
					isDistantDiscAvailable = indexOfLastMatrixColumn > focalColumn + Game.k_LengthOfWinningConnectionFromFirstDisk;
					break;

				case eDirectionOfDiscConnection.UpRight:
					isDistantDiscAvailable =
						(indexOfLastMatrixColumn > focalColumn + Game.k_LengthOfWinningConnectionFromFirstDisk)
						&& (indexOfLastMatrixRow > focalRow - Game.k_LengthOfWinningConnectionFromFirstDisk);
					break;

				case eDirectionOfDiscConnection.Up:
					isDistantDiscAvailable = indexOfLastMatrixRow > focalRow - Game.k_LengthOfWinningConnectionFromFirstDisk;
					break;

				case eDirectionOfDiscConnection.UpLeft:
					isDistantDiscAvailable =
						(indexOfLastMatrixColumn > focalColumn - Game.k_LengthOfWinningConnectionFromFirstDisk)
						&& (indexOfLastMatrixRow > focalRow - Game.k_LengthOfWinningConnectionFromFirstDisk);
					break;

				case eDirectionOfDiscConnection.Left:
					isDistantDiscAvailable = indexOfLastMatrixColumn > focalColumn - Game.k_LengthOfWinningConnectionFromFirstDisk;
					break;

				case eDirectionOfDiscConnection.DownLeft:
					isDistantDiscAvailable =
						(indexOfLastMatrixColumn > focalColumn - Game.k_LengthOfWinningConnectionFromFirstDisk)
						&& (indexOfLastMatrixRow > focalRow + Game.k_LengthOfWinningConnectionFromFirstDisk);
					break;

				case eDirectionOfDiscConnection.Down:
					isDistantDiscAvailable = indexOfLastMatrixRow > focalRow + Game.k_LengthOfWinningConnectionFromFirstDisk;
					break;

				case eDirectionOfDiscConnection.DownRight:
					isDistantDiscAvailable =
						(indexOfLastMatrixRow > focalRow + Game.k_LengthOfWinningConnectionFromFirstDisk)
						&& (indexOfLastMatrixRow > focalRow + Game.k_LengthOfWinningConnectionFromFirstDisk);
					break;
			}

			return isDistantDiscAvailable;
		}*/
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
