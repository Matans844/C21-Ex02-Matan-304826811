using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Players;

namespace C21_Ex02_Matan_304826811.GameLogic
{
	public class Game
	{
		public const int k_ZeroPoints = 0;

		// This field determines the length of the winning connection.
		// For generality, we need to make sure this field does not exceed the length (or height) of the board.
		public const int k_LengthOfWinningConnection = 4;
		public const int k_LengthOfWinningConnectionFromFirstDisk = k_LengthOfWinningConnection - 1;

		public UserInterfaceAdmin GameUserInterfaceAdmin { get; }

		public object[] BoxingPlayersInGame { get; } = new object[2];

		public Board GameBoard { get; set; }

		public Player Player1WithXs { get; }

		public Player Player2WithOs { get; }

		public eGameMode Mode { get; }

		public uint GameNumber { get; set; }

		public Game(eGameMode i_ChosenGameMode, GameBoardDimensions i_ChosenGameDimensions, UserInterfaceAdmin i_MyUserInterfaceAdmin)
		{
			this.GameUserInterfaceAdmin = i_MyUserInterfaceAdmin;
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

			this.BoxingPlayersInGame[0] = this.Player1WithXs;
			this.BoxingPlayersInGame[1] = this.Player2WithOs;
		}

		private void continueWithAnotherGame()
		{
			this.GameBoard = new Board(this.GameBoard.Dimensions, this);
			this.GameNumber++;

			this.GameUserInterfaceAdmin.MyMessageCreator.UpdateStatusOfPoints();
			this.StartGame();
		}

		private bool hasGameEndedAfterMove()
		{
			return this.GameBoard.BoardState != eBoardState.NotFinished;
		}

		private bool hasGameEndedInDraw()
		{
			return this.GameBoard.BoardState == eBoardState.FinishedInDraw;
		}

		// Not used.
		//private bool hasGameEndedInWin()
		//{
		//	return this.hasGameEndedAfterMove() && !this.hasGameEndedInDraw();
		//}

		public void StartGame()
		{
			int chosenBoardMoveAdjustedToMatrix; 
			this.GameUserInterfaceAdmin.PhaseOfUserInterface = ePhaseOfUserInterface.BoardScreen;

			while (!this.GameUserInterfaceAdmin.IsPlayerQuittingGame())
			{
				foreach (Player playerOfGame in this.BoxingPlayersInGame)
				{
					chosenBoardMoveAdjustedToMatrix = this.GameUserInterfaceAdmin.MyInputOutputHandler.PromptForMoveOnDisplayedBoard();
					playerOfGame.MakeMove(chosenBoardMoveAdjustedToMatrix);

					if (this.hasGameEndedAfterMove())
					{
						break;
					}
				}
			}

			if (!this.hasGameEndedInDraw())
			{
				this.GameBoard.BoardReferee.Winner.PointsEarned++;
			}

			if (this.GameUserInterfaceAdmin.ConcludeSingleGameAndOfferAnotherGame())
			{
				this.continueWithAnotherGame();
			}

			this.GameUserInterfaceAdmin.PhaseOfUserInterface = ePhaseOfUserInterface.Terminated;
		}
	}

	public enum eGameMode
	{
		NotInitiated = 0,
		PlayerVsPlayer = 1,
		PlayerVsComputer = 2
	}
}
