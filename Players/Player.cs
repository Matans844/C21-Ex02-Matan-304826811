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
	public abstract class Player
	{
		private const int k_ZeroPoints = 0;

		public int Points { get; set; }

		public eBoardCellType DiscType { get; }

		public eTurnState TurnState { get; set; }

		protected Player(eBoardCellType i_DiscType, eTurnState i_TurnState)
		{
			this.Points = k_ZeroPoints;
			this.DiscType = i_DiscType;
			this.TurnState = i_TurnState;
		}

		public abstract BoardCell PickBoardColumnForDisc(Board i_Board, int i_Column);
	}

	public enum ePlayerType
	{
		NotInit = 0,
		Human = 1,
		Computer = 2
	}

	public enum eTurnState
	{
		NotInit = 0,
		YourTurn = 1,
		NotYourTurn = 2
	}
}
