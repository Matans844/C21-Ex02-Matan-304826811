using System;
using System.Linq;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Extensions;

namespace C21_Ex02_Matan_304826811.GameLogic
{
	public class Board
	{
		public const int k_TransformBoardToMatrixIndicesWith1 = 1;

		public Game GameForBoard { get; }

		public eBoardState BoardState { get; set; } = eBoardState.NotFinished;

		public int NumOfCellVacanciesInBoard { get; set; }

		public int[] NumOfCellVacanciesInColumn { get; }

		public GameBoardDimensions Dimensions { get; }

		public BoardCell[,] BoardCellMatrix { get; }

		public Referee BoardReferee { get; }

		public BoardCell LastCellOccupied { get; set; }

		public Board(GameBoardDimensions i_ChosenGameDimensions, Game i_GameForBoard)
		{
			this.GameForBoard = i_GameForBoard;
			this.Dimensions = i_ChosenGameDimensions;
			this.BoardCellMatrix = new BoardCell[i_ChosenGameDimensions.Height, i_ChosenGameDimensions.Width];
			this.BoardCellMatrix.InitWithBoardCells();
			this.BoardReferee = new Referee(this);
			this.NumOfCellVacanciesInBoard = i_ChosenGameDimensions.Height * i_ChosenGameDimensions.Width;

			this.NumOfCellVacanciesInColumn = Enumerable.Repeat(
				i_ChosenGameDimensions.Width, i_ChosenGameDimensions.Height).ToArray();
		}

		private static bool isChosenColumnInRange(int i_ChosenColumn, int i_NumOfColumnsInBoard)
		{
			// TODO: Change this to fit dimensions
			return (i_ChosenColumn >= 0)
					&& (i_ChosenColumn <= i_NumOfColumnsInBoard - k_TransformBoardToMatrixIndicesWith1);
		}

		public eBoardState SlideDisk(int i_ChosenBoardColumnAdjustedForMatrix, eBoardCellType i_PlayerDiscType)
		{
			return insertToBoard(i_ChosenBoardColumnAdjustedForMatrix, ref i_PlayerDiscType);
		}

		private eBoardState insertToBoard(int i_ColumnInMatrixToInsert, ref eBoardCellType i_PlayerDiscType)
		{
			// TODO: Check
			// int lastVacantCellInColumn = this.Dimensions.Height - this.NumOfCellVacanciesInColumn[i_ColumnInMatrixToInsert];
			int rowIndexOfLastVacantCellInChosenColumn = this.NumOfCellVacanciesInColumn[i_ColumnInMatrixToInsert] - k_TransformBoardToMatrixIndicesWith1;

			this.NumOfCellVacanciesInColumn[i_ColumnInMatrixToInsert]--;
			this.NumOfCellVacanciesInBoard--;
			this.BoardCellMatrix[rowIndexOfLastVacantCellInChosenColumn, i_ColumnInMatrixToInsert].CellType = i_PlayerDiscType;
			this.LastCellOccupied = this.BoardCellMatrix[rowIndexOfLastVacantCellInChosenColumn, i_ColumnInMatrixToInsert].ShallowCopy();
			return this.calculateBoardState(LastCellOccupied);
		}

		private eBoardState calculateBoardState(BoardCell i_LastDiscPlayed)
		{
			if (this.BoardReferee.IsGameFinished(i_LastDiscPlayed))
			{
				this.BoardState = this.BoardReferee.IsGameDrawn(i_LastDiscPlayed)
									? eBoardState.FinishedInDraw
									: eBoardState.FinishedInWin;
			}

			return this.BoardState;
		}

		private bool columnAvailabilityForDisc(int i_ChosenColumn, out bool o_IsOutOfRange)
		{
			int chosenColumnTranslatedToMatrixIndices = i_ChosenColumn;
			o_IsOutOfRange = true;
			int numOfColumns = this.Dimensions.Width;
			bool isValidChoice = false;

			if (isChosenColumnInRange(i_ChosenColumn, numOfColumns))
			{
				o_IsOutOfRange = false;
				isValidChoice = this.NumOfCellVacanciesInColumn[chosenColumnTranslatedToMatrixIndices] != 0;
			}

			return isValidChoice;
		}

		public bool IsColumnAvailableForDisc(int i_ChosenColumn, out bool o_IsOutOfRange)
		{
			return this.columnAvailabilityForDisc(i_ChosenColumn, out o_IsOutOfRange);
		}
	}

	public enum eBoardState
	{
		NotFinished = 0,
		FinishedInDraw = 1,
		FinishedInWin = 2
	}

	public enum eAttemptedOutOfRange
	{
		No = 0,
		Yes = 1
	}
}
