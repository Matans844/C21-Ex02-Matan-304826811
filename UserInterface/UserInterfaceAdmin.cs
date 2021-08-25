using System;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;

namespace C21_Ex02_Matan_304826811.UserInterface
{
	public class UserInterfaceAdmin
	{
		public const string k_QuitKey = "Q";

		public DisplayLogic MyGameDisplayLogic { get; set; }

		public InputOutputHandler MyInputOutputHandler { get; set; }

		public ViewOfInitialScreen MyInitialScreenView { get; set; }

		public Game MyGameLogicUnit { get; set; }

		public ePhaseOfUserInterface PhaseOfUserInterface { get; set; } = ePhaseOfUserInterface.Initiated;

		public eQuitFlag QuitFlag { get; set; } = eQuitFlag.DoNotQuit;

		public UserInterfaceAdmin()
		{
			this.MyGameDisplayLogic = new DisplayLogic(this);
			this.MyInitialScreenView = new ViewOfInitialScreen(this);
			this.MyGameLogicUnit = new Game(this.MyGameDisplayLogic.GameMode, this.MyGameDisplayLogic.m_BoardDimensions, this);
		}

		public void InitializeGame()
		{
			this.MyGameLogicUnit.StartGame();
		}

		private bool isUserInterfaceTerminating()
		{
			return this.PhaseOfUserInterface == ePhaseOfUserInterface.Terminated;
		}

		public bool HasPlayerQuitGame()
		{
			return this.isUserInterfaceTerminating();
		}

		internal bool WantsAnotherGame()
		{
			throw new NotImplementedException();
		}
	}

	public enum eQuitFlag
	{
		DoNotQuit = 0,
		Quit = 1
	}

	public enum ePhaseOfUserInterface
	{
		Initiated = 0,
		InitialScreen = 1,
		BoardScreen = 2,
		Terminated = 3
	}
}