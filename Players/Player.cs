using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using C21_Ex02_Matan_304826811.Views;
using C21_Ex02_Matan_304826811.Toolkit;
using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.Players
{
	public abstract class Player
	{
		public int PlayerID { get; } = 0;

		public Board BoardOfPlayer { get; }

		public int PointsEarned { get; set; } = Game.k_ZeroPoints;

		public eBoardCellType DiscType { get; }

		public eTurnState TurnState { get; set; }

		protected Player(Board i_BoardOfPlayer, eBoardCellType i_DiscType, eTurnState i_TurnState)
		{
			this.DiscType = i_DiscType;
			this.BoardOfPlayer = i_BoardOfPlayer;
			this.TurnState = i_TurnState;
			this.PlayerID++;
		}

		public abstract eBoardState MakeMove(int i_ChosenColumn);
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
