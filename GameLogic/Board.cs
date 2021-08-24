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

	public class Board
	{
		private readonly GameBoardDimensions r_BoardDimensions;
		private int[] m_NumOfCellVacanciesInColumn;
		private BoardCell[,] m_BoardCellMatrix;

		public int[] NumOfCellVacanciesInColumn => this.m_NumOfCellVacanciesInColumn;

		public GameBoardDimensions Dimensions => this.r_BoardDimensions;

		public BoardCell[,] BoardCellMatrix => this.m_BoardCellMatrix;

		public Board(GameBoardDimensions i_ChosenGameDimensions)
		{
			this.r_BoardDimensions = i_ChosenGameDimensions;
			this.m_BoardCellMatrix = new BoardCell[i_ChosenGameDimensions.Height, i_ChosenGameDimensions.Width];

			this.m_NumOfCellVacanciesInColumn = Enumerable.Repeat(
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
			BoardCellMatrix[i_RowToUpdate, i_ColumnToUpdate] = io_ChosenCell;
			return io_ChosenCell;
		}
	}

	public enum eColumnVacancy
	{
		Empty = 0,
		NotFull = 1,
		Full = 2
	}
}
