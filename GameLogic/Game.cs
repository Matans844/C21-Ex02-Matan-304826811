using System;
using System.Collections.Generic;
using System.Text;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;

namespace C21_Ex02_Matan_304826811.GameLogic
{
	public enum eGameMode
	{
		NotInitiated = 0,
		PlayerVsPlayer = 1,
		PlayerVsComputer = 2
	}

	public class Game
	{
		private readonly eGameMode r_GameMode;
		private readonly GameBoardDimensions r_GameDimensions;

		public eGameMode Mode => r_GameMode;

		public GameBoardDimensions BoardDimensions => r_GameDimensions;

		public Game(eGameMode o_ChosenGameMode, GameBoardDimensions o_ChosenGameDimensions)
		{
			this.r_GameMode = o_ChosenGameMode;
			this.r_GameDimensions = o_ChosenGameDimensions;
		}
	}
}
