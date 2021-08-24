using System;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;

namespace C21_Ex02_Matan_304826811.UserInterface
{
	public class UserInterfaceAdmin
	{
		public UserInterfaceAdmin()
		{
			ViewInit initialView = new ViewInit();
			Game game = new Game(DisplayLogic.ChosenGameMode, DisplayLogic.ChosenGameDimensions);
		}

		public void StartGame()
		{

		}
	}
}