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
		public Board BoardOfPlayer { get; }

		public int Point { get; set; } = Game.k_ZeroPoints;

		public eBoardCellType DiscType { get; }

		public eTurnState TurnState { get; set; }

		protected Player(Board i_BoardOfPlayer, eBoardCellType i_DiscType, eTurnState i_TurnState)
		{
			this.DiscType = i_DiscType;
			this.BoardOfPlayer = i_BoardOfPlayer;
			this.TurnState = i_TurnState;
		}

		public abstract eBoardState MakeMove(int i_ChosenColumn);

		protected abstract int ChooseColumnForMove();
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
