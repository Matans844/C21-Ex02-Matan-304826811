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
		private readonly object[] r_BoxingPlayersInGame = new object[2];

		public object[] BoxignPlayersInGame => r_BoxingPlayersInGame;

		public Board GameBoard { get; set; }

		public Player Player1WithXs { get; }

		public Player Player2WithOs { get; }

		public eGameMode Mode { get; }

		public uint GameNumber { get; set; } = 0;

		public Game(eGameMode i_ChosenGameMode, GameBoardDimensions i_ChosenGameDimensions)
		{
			this.Mode = i_ChosenGameMode;
			this.GameBoard = new Board(i_ChosenGameDimensions, this);
			this.Player1WithXs = new PlayerHuman(this.GameBoard, eBoardCellType.XDisc, eTurnState.YourTurn);
			this.GameNumber++;

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

			this.BoxignPlayersInGame[0] = (object)this.Player1WithXs;
			this.BoxignPlayersInGame[1] = (object)this.Player2WithOs;
		}

		private void continueWithAnotherGame()
		{
			this.GameBoard = new Board(this.GameBoard.Dimensions, this);
			this.StartGame();
		}

		private eBoardState getGameState(Board i_BoardOfGame)
		{
			return i_BoardOfGame.BoardState;
			}

		private bool hasGameEnded()
		{
			return this.getGameState(this.GameBoard) != eBoardState.NotFinished;
		}

		private bool hasGameEndedInDraw()
		{
			return this.getGameState(this.GameBoard) == eBoardState.FinishedInDraw;
		}

		// Not used.
		private bool hasGameEndedInWin()
		{
			return this.hasGameEnded() && !this.hasGameEndedInDraw();
		}

		public void StartGame()
		{
			while (!UserInterfaceAdmin.HasPlayerQuitGame())
			{
				foreach (Player playerOfGame in this.BoxignPlayersInGame)
				{
					playerOfGame.MakeMove(InputOutputHandler.PromptForMove());

					if (this.hasGameEnded())
					{
						break;
					}
				}
			}

			if (!this.hasGameEndedInDraw())
			{
				this.GameBoard.BoardReferee.Winner.Point++;
			}

			if (UserInterfaceAdmin.WantsAnotherGame())
			{
				this.continueWithAnotherGame();
			}
		}
	}

	public enum eGameMode
	{
		NotInitiated = 0,
		PlayerVsPlayer = 1,
		PlayerVsComputer = 2
	}


}
