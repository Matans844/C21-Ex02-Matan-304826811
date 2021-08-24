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
		Default = 0,
		PlayerVsPlayer = 1,
		PlayerVsComputer = 2
	}

	class Game
	{
		private static readonly eGameMode sr_GameMode;
		private static readonly GameBoardDimensions r_GameDimensions;
		private object chosenGameMode;
		private object chosenGameDimensions;

		public Game(object chosenGameMode, object chosenGameDimensions)
		{
			this.chosenGameMode = chosenGameMode;
			this.chosenGameDimensions = chosenGameDimensions;
		}
	}
}
