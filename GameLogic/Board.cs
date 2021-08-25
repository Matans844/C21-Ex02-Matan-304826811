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
		// Auto properties are used. They contain class fields.
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
			this.BoardReferee = new Referee(this);
			this.NumOfCellVacanciesInBoard = i_ChosenGameDimensions.Height * i_ChosenGameDimensions.Width;

			this.NumOfCellVacanciesInColumn = Enumerable.Repeat(
				i_ChosenGameDimensions.Width, i_ChosenGameDimensions.Height).ToArray();
		}

		public eBoardState SlideDisk(int i_Column, eBoardCellType i_PlayerDiscType)
		{
			return insertToBoard(i_Column, ref i_PlayerDiscType);
		}

		private eBoardState insertToBoard(int i_Column, ref eBoardCellType i_PlayerDiscType)
		{
			var lastVacantCellInColumn = this.Dimensions.Height - this.NumOfCellVacanciesInColumn[i_Column];

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
	}

	public enum eBoardState
	{
		NotFinished = 0,
		FinishedInDraw = 1,
		FinishedInWin = 2
	}
}
