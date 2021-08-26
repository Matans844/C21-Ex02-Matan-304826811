using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Views;

namespace C21_Ex02_Matan_304826811.UserInterface
{
	public class UserInterfaceAdmin
	{
		public const int k_NumberOfGamesOnFirstGame = 1;

		public DisplayLogic MyGameDisplayLogic { get; set; }

		public MessageCreator MyMessageCreator { get; set; }

		public ScreenCreator MyScreenCreator { get; set; }

		public InputOutputHandler MyInputOutputHandler { get; set; }

		public ViewOfInitialScreen MyInitialScreenView { get; set; }

		public ViewOfBoardScreen MyBoardScreenView { get; set; }

		public Game MyGameLogicUnit { get; set; }

		public ePhaseOfUserInterface PhaseOfUserInterface { get; set; } = ePhaseOfUserInterface.InitialScreen;

		public eQuitProcess QuitProcess { get; set; } = eQuitProcess.DoNotQuit;

		public bool IsEscapeKeyOn { get; set; }

		public UserInterfaceAdmin()
		{
			this.MyGameDisplayLogic = new DisplayLogic(this);
			this.MyInputOutputHandler = new InputOutputHandler(this);
			this.MyInitialScreenView = new ViewOfInitialScreen(this);
			this.MyMessageCreator = new MessageCreator(this);
			this.MyScreenCreator = new ScreenCreator(this);
		}

		public void InitializeGame()
		{
			this.initializeGame();
		}

		private void initializeGame()
		{
			this.MyInitialScreenView.GetInitialInputsFromUser();
			MyScreenCreator.ScreenBoardRowWidthInChar = this.MyGameDisplayLogic.m_BoardDimensions.Width;
			this.MyBoardScreenView = new ViewOfBoardScreen(this);
			this.PhaseOfUserInterface = ePhaseOfUserInterface.InitialScreen;
			this.MyGameLogicUnit = new Game(this.MyGameDisplayLogic.GameMode, this.MyGameDisplayLogic.m_BoardDimensions, this);
			this.MyMessageCreator.UpdateStatusOfPoints();
			this.MyGameLogicUnit.StartGame();
		}

		private bool isUserAbandoningGame()
		{
			return this.QuitProcess == eQuitProcess.Quit;
		}

		private bool hasPlayerQuitGame()
		{
			if (this.IsEscapeKeyOn)
			{
				// TODO
				if (!this.isUserAbandoningGame())
				{
					this.checkWhyPlayerQuitGame(this.PhaseOfUserInterface);
				}

				this.MyInputOutputHandler.SayGoodbye(this.PhaseOfUserInterface);
				this.PhaseOfUserInterface = ePhaseOfUserInterface.Terminated;
			}

			return this.IsEscapeKeyOn;
		}

		private void checkWhyPlayerQuitGame(ePhaseOfUserInterface i_PhaseOfUserInterface)
		{
			switch (i_PhaseOfUserInterface)
			{
				case ePhaseOfUserInterface.BoardScreen:
					this.QuitProcess = eQuitProcess.Quit;

					if (this.concludingSingleGameAndOfferAnother())
					{
						this.QuitProcess = eQuitProcess.DoNotQuit;
						this.IsEscapeKeyOn = false;
					}

					break;

				case ePhaseOfUserInterface.InitialScreen:
					this.QuitProcess = eQuitProcess.Quit;

					break;

				case ePhaseOfUserInterface.Terminated:

					break;
			}
		}

		public bool IsPlayerQuittingGame()
		{
			return this.hasPlayerQuitGame();
		}

		public bool ConcludeSingleGameAndOfferAnotherGame()
		{
			return this.concludingSingleGameAndOfferAnother();
		}

		private bool concludingSingleGameAndOfferAnother()
		{
			if (this.MyGameLogicUnit.GameNumber > k_NumberOfGamesOnFirstGame)
			{
				this.MyInputOutputHandler.DeclarePointStatus();
			}

			if (this.MyGameLogicUnit.GameBoard.BoardState == eBoardState.FinishedInWin)
			{
				this.MyInputOutputHandler.DeclareGameResult();
			}

			return this.askPlayerForAnotherGame();
		}

		private bool askPlayerForAnotherGame()
		{
			return this.MyInputOutputHandler.PromptForAnotherGame();
		}
	}

	public enum eQuitProcess
	{
		DoNotQuit = 0,
		Quit = 1
	}

	public enum ePhaseOfUserInterface
	{
		InitialScreen = 0,
		BoardScreen = 1,
		Terminated = 2
	}
}