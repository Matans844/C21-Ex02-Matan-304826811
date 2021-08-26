using System;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using C21_Ex02_Matan_304826811.Views;
using C21_Ex02_Matan_304826811.Toolkit;
using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.UserInterface
{
	using C21_Ex02_Matan_304826811.Views;

	public class UserInterfaceAdmin
	{
		public const string k_QuitKey = "Q";

		public DisplayLogic MyGameDisplayLogic { get; set; }

		public InputOutputHandler MyInputOutputHandler { get; set; }

		public ViewOfInitialScreen MyInitialScreenView { get; set; }

		public ViewOfBoardScreen MyBoardScreenView { get; set; }

		public Game MyGameLogicUnit { get; set; }

		public ePhaseOfUserInterface PhaseOfUserInterface { get; set; } = ePhaseOfUserInterface.InitialScreen;

		public eQuitProcess QuitProcess { get; set; } = eQuitProcess.DoNotQuit;

		public bool IsEscapeKeyOn { get; set; } = false;

		public UserInterfaceAdmin()
		{
			this.MyGameDisplayLogic = new DisplayLogic(this);
			this.MyInputOutputHandler = new InputOutputHandler(this);
			this.MyInitialScreenView = new ViewOfInitialScreen(this);
		}

		public void InitializeGame()
		{
			this.initializeGame();

			if (this.IsPlayerQuittingGame())
			{
				return;
			}
		}

		private void initializeGame()
		{
			this.MyInitialScreenView.GetInitialInputsFromUser();

			if (this.IsPlayerQuittingGame())
			{
				return;
			}

			ScreenCreator.ScreenBoardRowWidthInChar = this.MyGameDisplayLogic.m_BoardDimensions.Width;
			this.MyBoardScreenView = new ViewOfBoardScreen(this);
			this.PhaseOfUserInterface = ePhaseOfUserInterface.InitialScreen;
			this.MyGameLogicUnit = new Game(this.MyGameDisplayLogic.GameMode, this.MyGameDisplayLogic.m_BoardDimensions, this);

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
					if (this.DoesPlayerWantAnotherGame())
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

		public bool DoesPlayerWantAnotherGame()
		{
			// TODO: Show point status
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