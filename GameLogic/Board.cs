using System.Linq;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Extensions;

namespace C21_Ex02_Matan_304826811.GameLogic
{
	public class Board
	{
		public const int k_TransformBoardToMatrixIndicesWith1 = 1;
		public const int k_ZeroIndex = 0;

		public static int NumberOfRowIndices { get; set; }

		public static int NumberOfColumnIndices { get; set; }

		public Game GameForBoard { get; }

		public eBoardState BoardState { get; set; } = eBoardState.NotFinished;

		public int NumOfCellVacanciesInBoard { get; set; }

		public int[] NumOfCellVacanciesInColumn { get; }

		public GameBoardDimensions Dimensions { get; }

		public BoardCell[,] BoardCellMatrix { get; }

		public Referee BoardReferee { get; }

		public Board(GameBoardDimensions i_ChosenGameDimensions, Game i_GameForBoard)
		{
			this.GameForBoard = i_GameForBoard;
			this.Dimensions = i_ChosenGameDimensions;
			this.BoardCellMatrix = new BoardCell[i_ChosenGameDimensions.Height, i_ChosenGameDimensions.Width];
			this.BoardCellMatrix.InitWithBoardCells();
			NumberOfRowIndices = BoardCellMatrix.GetLength(0) - Board.k_TransformBoardToMatrixIndicesWith1;
			NumberOfColumnIndices = BoardCellMatrix.GetLength(1) - Board.k_TransformBoardToMatrixIndicesWith1;
			this.BoardReferee = new Referee(this);

			this.NumOfCellVacanciesInBoard = i_ChosenGameDimensions.Height * i_ChosenGameDimensions.Width;

			this.NumOfCellVacanciesInColumn = Enumerable.Repeat(
				i_ChosenGameDimensions.Width, i_ChosenGameDimensions.Height).ToArray();
		}

		public static bool IsNumberInInclusiveRange(
			int i_NumberToFindInRange,
			int i_LowerRangeLimit,
			int i_UpperRangeLimit)
		{
			return (i_NumberToFindInRange >= i_LowerRangeLimit) && (i_NumberToFindInRange <= i_UpperRangeLimit);
		}

		public BoardCell SlideDiskToBoard(int i_ChosenBoardColumnAdjustedForMatrix, eBoardCellType i_PlayerDiscType)
		{
			int rowIndexOfLastVacantCellInChosenColumn =
				this.NumOfCellVacanciesInColumn[i_ChosenBoardColumnAdjustedForMatrix]
				- k_TransformBoardToMatrixIndicesWith1;

			this.NumOfCellVacanciesInColumn[i_ChosenBoardColumnAdjustedForMatrix]--;
			this.NumOfCellVacanciesInBoard--;

			this.BoardCellMatrix[rowIndexOfLastVacantCellInChosenColumn, i_ChosenBoardColumnAdjustedForMatrix]
				.CellType = i_PlayerDiscType;

			return this.BoardCellMatrix[rowIndexOfLastVacantCellInChosenColumn, i_ChosenBoardColumnAdjustedForMatrix]
				.ShallowCopy();
		}

		public bool IsColumnIndexAvailableForDisc(int i_ChosenColumnIndex, out bool o_IsOutOfRange)
		{
			int chosenColumnTranslatedToMatrixIndices = i_ChosenColumnIndex;
			o_IsOutOfRange = true;
			int numOfColumnIndices = this.Dimensions.Width - k_TransformBoardToMatrixIndicesWith1;
			bool isValidChoice = false;

			if (IsNumberInInclusiveRange(i_ChosenColumnIndex, k_ZeroIndex, numOfColumnIndices))
			{
				o_IsOutOfRange = false;
				isValidChoice = this.NumOfCellVacanciesInColumn[chosenColumnTranslatedToMatrixIndices] != 0;
			}

			return isValidChoice;
		}
	}

	public enum eBoardState
	{
		NotFinished = 0,
		FinishedInDraw = 1,
		FinishedInWinByBoard = 2,
		FinishedInWinByQuit = 3
	}

	public enum eAttemptedOutOfRange
	{
		No = 0,
		Yes = 1
	}
}