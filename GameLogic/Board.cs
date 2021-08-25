﻿using System;
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
		public eBoardState BoardState { get; set; } = eBoardState.NotFinished;

		public int NumOfCellVacanciesInBoard { get; set; }

		public int[] NumOfCellVacanciesInColumn { get; }

		public GameBoardDimensions Dimensions { get; }

		public BoardCell[,] BoardCellMatrix { get; }

		public Referee BoardReferee { get; }

		public BoardCell LastCellOccupied { get; set; }

		public Board(GameBoardDimensions i_ChosenGameDimensions)
		{
			this.Dimensions = i_ChosenGameDimensions;
			this.BoardCellMatrix = new BoardCell[i_ChosenGameDimensions.Height, i_ChosenGameDimensions.Width];
			this.BoardReferee = new Referee(this);
			this.NumOfCellVacanciesInBoard = i_ChosenGameDimensions.Height * i_ChosenGameDimensions.Width;

			this.NumOfCellVacanciesInColumn = Enumerable.Repeat(
				i_ChosenGameDimensions.Width, i_ChosenGameDimensions.Height).ToArray();
		}

		public BoardCell SlideDisk(int i_Column, eBoardCellType i_PlayerDiscType)
		{
			return insertToBoard(i_Column, ref i_PlayerDiscType);
		}

		private BoardCell insertToBoard(int i_Column, ref eBoardCellType i_PlayerDiscType)
		{
			var lastVacantCellInColumn = this.Dimensions.Height - this.NumOfCellVacanciesInColumn[i_Column];

			this.NumOfCellVacanciesInColumn[i_Column]--;
			this.NumOfCellVacanciesInBoard--;
			this.BoardCellMatrix[lastVacantCellInColumn, i_Column].CellType = i_PlayerDiscType;
			this.LastCellOccupied = this.BoardCellMatrix[lastVacantCellInColumn, i_Column].ShallowCopy();
			this.BoardState = this.calculateBoardState(LastCellOccupied);

			return this.LastCellOccupied;
		}

		private eBoardState calculateBoardState(BoardCell i_LastDiscPlayed)
		{
			return this.BoardReferee.IsGameFinished(i_LastDiscPlayed)
						? this.BoardReferee.IsGameDrawn(i_LastDiscPlayed) ? eBoardState.FinishedInDraw : eBoardState.FinishedInWin
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
