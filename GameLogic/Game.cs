using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.UserInterface;

namespace C21_Ex02_Matan_304826811.GameLogic
{
	public class Game
	{
		public const int k_ZeroPoints = 0;

		// This field determines the length of the winning connection.
		// For generality, we need to make sure this field does not exceed the length (or height) of the board.
		public const int k_LengthOfWinningConnection = 4;

		public UserInterfaceAdmin GameUserInterfaceAdmin { get; }

		public object[] BoxingPlayersInGame { get; } = new object[2];

		public Board GameBoard { get; set; }

		public IPlayer Player1WithXs { get; }

		public IPlayer Player2WithOs { get; }

		public eGameMode Mode { get; }

		public uint GameNumber { get; set; }

		public Game(
			eGameMode i_ChosenGameMode,
			GameBoardDimensions i_ChosenGameDimensions,
			UserInterfaceAdmin i_MyUserInterfaceAdmin)
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

			this.Player1WithXs.TurnState = eTurnState.YourTurn;
			this.Player2WithOs.TurnState = eTurnState.NotYourTurn;
			IPlayer.BoardOfPlayer = this.GameBoard;

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

		public void StartGame()
		{
			int chosenValidBoardMoveAdjustedToMatrix;
			BoardCell lastDiscPlayed;
			this.GameUserInterfaceAdmin.PhaseOfUserInterface = ePhaseOfUserInterface.BoardScreen;

			while (!this.GameUserInterfaceAdmin.IsPlayerQuittingGame())
			{
				foreach (IPlayer playerOfGame in this.BoxingPlayersInGame)
				{
					playerOfGame.TurnState = eTurnState.YourTurn;

					chosenValidBoardMoveAdjustedToMatrix = this.GameUserInterfaceAdmin.MyInputOutputHandler
						.PromptForValidMoveOnDisplayedBoard();
					lastDiscPlayed = playerOfGame.MakeMove(chosenValidBoardMoveAdjustedToMatrix);
					this.GameBoard.BoardReferee.CalculateBoardState(lastDiscPlayed);

					if (this.hasGameEndedAfterMove())
					{
						goto GameEnded;
					}
				}
			}

			GameEnded:

			if (!this.hasGameEndedInDraw())
			{
				this.GameBoard.BoardReferee.Winner.PointsEarned++;
			}

			// Declare game results, show general results and prompt for another game
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