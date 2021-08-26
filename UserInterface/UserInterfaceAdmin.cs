using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;

namespace C21_Ex02_Matan_304826811.UserInterface
{
	using C21_Ex02_Matan_304826811.Views;

	public class UserInterfaceAdmin
	{
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

			if (this.IsPlayerQuittingGame())
			{
				return;
			}

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
				this.checkWhyPlayerQuitGame(this.PhaseOfUserInterface);

				if (this.isUserAbandoningGame())
				{
					this.MyInputOutputHandler.SayGoodbye(this.PhaseOfUserInterface);
					this.PhaseOfUserInterface = ePhaseOfUserInterface.Terminated;
				}
			}

			return this.IsEscapeKeyOn;
		}

		private void checkWhyPlayerQuitGame(ePhaseOfUserInterface i_PhaseOfUserInterface)
		{
			switch (i_PhaseOfUserInterface)
			{
				case ePhaseOfUserInterface.BoardScreen:
					if (this.concludingSingleGame())
					{
						this.IsEscapeKeyOn = false;
						}
					else
					{
						this.QuitProcess = eQuitProcess.Quit;
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

		public bool ConcludeSingleGame()
		{
			return this.concludingSingleGame();
		}

		private bool concludingSingleGame()
		{
			if (this.MyGameLogicUnit.GameNumber > 0)
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