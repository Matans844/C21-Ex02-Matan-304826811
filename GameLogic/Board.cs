using System;
using System.Linq;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Extensions;

namespace C21_Ex02_Matan_304826811.GameLogic
{
	public class Board
	{
		private const int k_TransformToIndicesWith1 = 1;

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
			return i_ChosenColumn < k_TransformToIndicesWith1
					|| i_ChosenColumn > i_NumOfColumnsInBoard - k_TransformToIndicesWith1;
		}

		public eBoardState SlideDisk(int i_Column, eBoardCellType i_PlayerDiscType)
		{
			return insertToBoard(i_Column, ref i_PlayerDiscType);
		}

		private eBoardState insertToBoard(int i_Column, ref eBoardCellType i_PlayerDiscType)
		{
			int lastVacantCellInColumn = this.Dimensions.Height - this.NumOfCellVacanciesInColumn[i_Column];

			this.NumOfCellVacanciesInColumn[i_Column]--;
			this.NumOfCellVacanciesInBoard--;
			this.BoardCellMatrix[lastVacantCellInColumn, i_Column].CellType = i_PlayerDiscType;
			this.LastCellOccupied = this.BoardCellMatrix[lastVacantCellInColumn, i_Column].ShallowCopy();
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

		private bool columnAvailabilityForDisc(int i_ChosenColumn, out eAttemptedOutOfRange o_LastMoveOutOfRange)
		{
			int chosenColumnTranslatedToMatrixIndices = i_ChosenColumn - 1;
			int numOfColumns = this.Dimensions.Width;
			bool isValidChoice = false;

			if (isChosenColumnInRange(i_ChosenColumn, numOfColumns))
			{
				o_LastMoveOutOfRange = eAttemptedOutOfRange.No;
				isValidChoice = this.NumOfCellVacanciesInColumn[chosenColumnTranslatedToMatrixIndices] == 0;
			}
			else
			{
				o_LastMoveOutOfRange = eAttemptedOutOfRange.Yes;
			}

			return isValidChoice;
		}

		public bool IsColumnAvailableForDisc(int i_ChosenColumn, out eAttemptedOutOfRange io_MoveOutOfRange)
		{
			return this.columnAvailabilityForDisc(i_ChosenColumn, out io_MoveOutOfRange);
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
