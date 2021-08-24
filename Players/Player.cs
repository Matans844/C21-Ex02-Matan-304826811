using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.Players
{
	using C21_Ex02_Matan_304826811.GameLogic;

	public abstract class Player
	{
		private const int k_ZeroPoints = 0;
		private readonly ePlayerType r_PlayerType;
		private readonly eBoardCellType r_DiscType;
		private int m_Score;

		public int Points
		{
			get => this.m_Score;
			set => this.m_Score = value;
		}

		public ePlayerType PlayerType => this.r_PlayerType;

		public eBoardCellType DiscType => this.r_DiscType;

		protected Player(ePlayerType i_PlayerType, eBoardCellType i_DiscType)
		{
			this.Points = k_ZeroPoints;
			this.r_PlayerType = i_PlayerType;
			this.r_DiscType = i_DiscType;
		}

		public abstract BoardCell PickBoardColumnForDisc(Board i_Board, int i_Column);
	}

	public enum ePlayerType
	{
		NotInit = 0,
		Human = 1,
		Computer = 2
	}
}
