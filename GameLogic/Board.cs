using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.GameLogic
{
	using System.Runtime.CompilerServices;
	using System.Runtime.InteropServices;

	public class Board
	{
		public eBoardState BoardState { get; set; } = eBoardState.NotFinished;

		public int NumOfCellVacanciesInBoard { get; set; }

		public int[] NumOfCellVacanciesInColumn { get; }

		public GameBoardDimensions Dimensions { get; }

		public BoardCell[,] BoardCellMatrix { get; }

		public Referee BoardReferee { get; }

		public Board(GameBoardDimensions i_ChosenGameDimensions)
		{
			this.Dimensions = i_ChosenGameDimensions;
			this.BoardCellMatrix = new BoardCell[i_ChosenGameDimensions.Height, i_ChosenGameDimensions.Width];
			this.BoardReferee = new Referee(this);
			this.NumOfCellVacanciesInBoard = i_ChosenGameDimensions.Height * i_ChosenGameDimensions.Width;

			this.NumOfCellVacanciesInColumn = Enumerable.Repeat(
				i_ChosenGameDimensions.Width, i_ChosenGameDimensions.Height).ToArray();
		}

		public BoardCell SlideDisk(int i_Column, eBoardCellType i_CellType)
		{
			int lastVacantCell = this.Dimensions.Height - NumOfCellVacanciesInColumn[i_Column];
			BoardCell chosenCell = new BoardCell((uint)i_Column, (uint)lastVacantCell, i_CellType);
			return updateBoardWithDisc(lastVacantCell, i_Column, chosenCell);
		}

		private BoardCell updateBoardWithDisc(int i_RowToUpdate, int i_ColumnToUpdate, BoardCell io_ChosenCell)
		{
			this.NumOfCellVacanciesInColumn[i_ColumnToUpdate]--;
			this.NumOfCellVacanciesInBoard--;
			BoardCellMatrix[i_RowToUpdate, i_ColumnToUpdate] = io_ChosenCell;
			return io_ChosenCell;
		}

		public eBoardState CalculateBoardState()
		{
			return BoardReferee.IsGameFinished()
						? BoardReferee.IsGameDrawn() ? eBoardState.FinishedInDraw : eBoardState.FinishedInWin
						: this.BoardState;
		}
	}

	public enum eBoardState
	{
		NotFinished = 0,
		FinishedInDraw = 1,
		FinishedInWin = 2
	}
}
