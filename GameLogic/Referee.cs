using System;

using C21_Ex02_Matan_304826811.Extensions;
using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.UserInterface;

namespace C21_Ex02_Matan_304826811.GameLogic
{
	public class Referee
	{
		public const int k_DistanceBetweenWeightsInJaggedArray = 3;
		public const int k_NumberOfWeightPairs = 3;

		public static readonly int[][] sr_IndexWeightsForChangeByDiscIndexOfConnection = new int[4][]
			{
				new int[3] { 1, 2, 3 }, new int[3] { -1, 1, 2 }, new int[3] { -1, 1, -2 }, new int[3] { -1, -2, -3 }
			};

		public static readonly int[][] sr_IndexWeightsForNegativeChangeByDiscIndexOfConnection = new int[4][]
			{
				new int[3] { -1, -2, -3 }, new int[3] { 1, -1, -2 }, new int[3] { 1, -1, 2 }, new int[3] { 1, 2, 3 }
			};

		public static readonly int[][] sr_EmptyIndexWeights = new int[4][]
			{
				new int[3], new int[3], new int[3], new int[3]
			};

		public static readonly int[][] sr_IndexWeightsForHorizontalConnections =
			OperateOn2DArrays.HorizontalConcatInsideJaggedArray(
				sr_EmptyIndexWeights, sr_IndexWeightsForChangeByDiscIndexOfConnection);

		public static readonly int[][] sr_IndexWeightsForVerticalConnections =
			OperateOn2DArrays.HorizontalConcatInsideJaggedArray(
				sr_IndexWeightsForChangeByDiscIndexOfConnection, sr_EmptyIndexWeights);

		public static readonly int[][] sr_IndexWeightsForDiagonalNegativeSlope =
			OperateOn2DArrays.HorizontalConcatInsideJaggedArray(
				sr_IndexWeightsForChangeByDiscIndexOfConnection, sr_IndexWeightsForChangeByDiscIndexOfConnection);

		public static readonly int[][] sr_IndexWeightsForDiagonalPositiveSlope =
			OperateOn2DArrays.HorizontalConcatInsideJaggedArray(
				sr_IndexWeightsForNegativeChangeByDiscIndexOfConnection,
				sr_IndexWeightsForChangeByDiscIndexOfConnection);

		public IPlayer Winner { get; set; }

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
				hasWinnerConnection = this.isWinningConnection(i_LastDiscPlaced, direction);

				if (hasWinnerConnection)
				{
					this.updateWinner(i_LastDiscPlaced);
					MessageCreator.GameResultsMessageForWonGame = this.Winner.PlayerID;

					break;
				}
			}

			return hasWinnerConnection;
		}

		public eBoardState CalculateBoardState(BoardCell i_LastDiscPlayed)
		{
			if (this.IsGameFinished(i_LastDiscPlayed))
			{
				this.BoardToReferee.BoardState = this.IsGameDrawn(i_LastDiscPlayed)
													? eBoardState.FinishedInDraw
													: eBoardState.FinishedInWin;
			}

			return this.BoardToReferee.BoardState;
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
			uint focalRowIndex = i_FocalBoardCell.Row;
			uint focalColumnIndex = i_FocalBoardCell.Column;

			this.setWeights(
				i_DirectionOfConnection, out int[] weightsForIndex0, out int[] weightsForIndex1,
				out int[] weightsForIndex2, out int[] weightsForIndex3);

			return checkDirectionByIndex(i_FocalBoardCell, focalRowIndex, focalColumnIndex, weightsForIndex0)
					|| checkDirectionByIndex(i_FocalBoardCell, focalRowIndex, focalColumnIndex, weightsForIndex1)
					|| checkDirectionByIndex(i_FocalBoardCell, focalRowIndex, focalColumnIndex, weightsForIndex2)
					|| checkDirectionByIndex(i_FocalBoardCell, focalRowIndex, focalColumnIndex, weightsForIndex3);
		}

		private void setWeights(
			eDirectionOfDiscConnection i_DirectionOfConnection,
			out int[] o_WeightsForIndex0,
			out int[] o_WeightsForIndex1,
			out int[] o_WeightsForIndex2,
			out int[] o_WeightsForIndex3)
		{
			switch (i_DirectionOfConnection)
			{
				case eDirectionOfDiscConnection.Horizontal:
					o_WeightsForIndex0 = sr_IndexWeightsForHorizontalConnections[0];
					o_WeightsForIndex1 = sr_IndexWeightsForHorizontalConnections[1];
					o_WeightsForIndex2 = sr_IndexWeightsForHorizontalConnections[2];
					o_WeightsForIndex3 = sr_IndexWeightsForHorizontalConnections[3];
					break;
				case eDirectionOfDiscConnection.Vertical:
					o_WeightsForIndex0 = sr_IndexWeightsForVerticalConnections[0];
					o_WeightsForIndex1 = sr_IndexWeightsForVerticalConnections[1];
					o_WeightsForIndex2 = sr_IndexWeightsForVerticalConnections[2];
					o_WeightsForIndex3 = sr_IndexWeightsForVerticalConnections[3];
					break;
				case eDirectionOfDiscConnection.DiagonalPositiveSlope:
					o_WeightsForIndex0 = sr_IndexWeightsForDiagonalPositiveSlope[0];
					o_WeightsForIndex1 = sr_IndexWeightsForDiagonalPositiveSlope[1];
					o_WeightsForIndex2 = sr_IndexWeightsForDiagonalPositiveSlope[2];
					o_WeightsForIndex3 = sr_IndexWeightsForDiagonalPositiveSlope[3];
					break;
				case eDirectionOfDiscConnection.DiagonalNegativeSlope:
					o_WeightsForIndex0 = sr_IndexWeightsForDiagonalNegativeSlope[0];
					o_WeightsForIndex1 = sr_IndexWeightsForDiagonalNegativeSlope[1];
					o_WeightsForIndex2 = sr_IndexWeightsForDiagonalNegativeSlope[2];
					o_WeightsForIndex3 = sr_IndexWeightsForDiagonalNegativeSlope[3];
					break;
				default:
					throw new ArgumentOutOfRangeException(
						nameof(i_DirectionOfConnection), i_DirectionOfConnection, null);
			}
		}

		private bool checkDirectionByIndex(
			BoardCell i_FocalBoardCell,
			uint i_FocalRow,
			uint i_FocalColumn,
			params int[] io_WeightsByDirection)
		{
			bool isConnectionWinning = false;
			int lastDiscFocalRowIndex = (int)i_FocalRow;
			int lastDiscFocalColumnIndex = (int)i_FocalColumn;
			bool connectionIsInMatrixRange = true;
			bool rowCoordinateInRange;
			bool columnCoordinateInRange;
			int weightForRow;
			int weightForColumn;

			// Checking that all coordinates for the 4 discs in connection are in range
			for (int i = 0; i < k_NumberOfWeightPairs; i++)
			{
				weightForRow = io_WeightsByDirection[i];
				weightForColumn = io_WeightsByDirection[i + k_DistanceBetweenWeightsInJaggedArray];

				rowCoordinateInRange = Board.IsNumberInInclusiveRange(
					lastDiscFocalRowIndex + weightForRow, Board.k_ZeroIndex, Board.NumberOfRowIndices);

				columnCoordinateInRange = Board.IsNumberInInclusiveRange(
					lastDiscFocalColumnIndex + weightForColumn, Board.k_ZeroIndex, Board.NumberOfColumnIndices);

				if (!rowCoordinateInRange || !columnCoordinateInRange)
				{
					connectionIsInMatrixRange = false;

					break;
				}
			}

			// All conditions are in range. We can safely check for connection inside Matrix without stepping out of bounds.
			if (connectionIsInMatrixRange)
			{
				isConnectionWinning = i_FocalBoardCell.HasSameTypeAs(
					this.BoardToReferee.BoardCellMatrix[lastDiscFocalRowIndex + io_WeightsByDirection[0],
						lastDiscFocalColumnIndex + io_WeightsByDirection[3]],
					this.BoardToReferee.BoardCellMatrix[lastDiscFocalRowIndex + io_WeightsByDirection[1],
						lastDiscFocalColumnIndex + io_WeightsByDirection[4]],
					this.BoardToReferee.BoardCellMatrix[lastDiscFocalRowIndex + io_WeightsByDirection[2],
						lastDiscFocalColumnIndex + io_WeightsByDirection[5]]);
			}

			return isConnectionWinning;
		}
	}

	public enum eDirectionOfDiscConnection
	{
		Horizontal = 0,
		Vertical = 1,
		DiagonalPositiveSlope = 2,
		DiagonalNegativeSlope = 3
	}
}