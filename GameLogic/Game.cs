using System;
using System.Collections.Generic;
using System.Text;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using C21_Ex02_Matan_304826811.Players;
using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.GameLogic
{
	public class Game
	{
		private readonly eGameMode r_GameMode;
		private readonly Board r_GameBoard;
		private readonly Player r_Player1;
		private readonly Player r_Player2;

		public eGameMode Mode => r_GameMode;

		public Game(eGameMode i_ChosenGameMode, GameBoardDimensions i_ChosenGameDimensions)
		{
			this.r_GameMode = i_ChosenGameMode;
			this.r_GameBoard = new Board(i_ChosenGameDimensions);
			this.r_Player1 = new PlayerHuman(ePlayerType.Human, eBoardCellType.XDisc);

			this.r_Player2 = i_ChosenGameMode == eGameMode.PlayerVsPlayer
								? new PlayerHuman(ePlayerType.Human, eBoardCellType.ODisc)
								: new PlayerHuman(ePlayerType.Computer, eBoardCellType.ODisc);
		}

		public void StartGame()
		{
			generatePlayers();
			startPlaying();
		}
	}

	public enum eGameMode
	{
		NotInitiated = 0,
		PlayerVsPlayer = 1,
		PlayerVsComputer = 2
	}


}
