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
		public const int k_ZeroPoints = 0;
		public const int k_LengthOfWinningConnection = 4;

		public Board GameBoard { get; }

		public Player Player1WithXs { get; }

		public Player Player2WithOs { get; }

		public eGameMode Mode { get; }

		public Game(eGameMode i_ChosenGameMode, GameBoardDimensions i_ChosenGameDimensions)
		{
			this.Mode = i_ChosenGameMode;
			this.GameBoard = new Board(i_ChosenGameDimensions);
			this.Player1WithXs = new PlayerHuman(this.GameBoard, eBoardCellType.XDisc, eTurnState.YourTurn);

			// In this case, there is no choice for 1-line condition expression.
			// Fore more details: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/target-typed-conditional-expression
			if (i_ChosenGameMode == eGameMode.PlayerVsPlayer)
			{
				this.Player2WithOs = new PlayerHuman(this.GameBoard, eBoardCellType.ODisc, eTurnState.NotYourTurn);
			}
			else
			{
				this.Player2WithOs = new PlayerComputer(this.GameBoard, eBoardCellType.ODisc, eTurnState.NotYourTurn);
			}
		}

		private eBoardState getGameState(Board i_BoardOfGame)
		{
			return i_BoardOfGame.BoardState;
		}

		private bool hasGameEnded()
		{
			return getGameState(this.GameBoard) != eBoardState.NotFinished;
		}

		public void StartGame()
		{
			while (!this.hasGameEnded())
			{
				this.Player1WithXs.MakeMove();

				if (this.hasGameEnded())
				{

				}
			}
		}

		private eBoardState singleMove(Player io_PlayerToMove)
		{
			return Player.MakeMove();
		}
	}

	public enum eGameMode
	{
		NotInitiated = 0,
		PlayerVsPlayer = 1,
		PlayerVsComputer = 2
	}


}
