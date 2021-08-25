using System;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;

namespace C21_Ex02_Matan_304826811.UserInterface
{
	public static class UserInterfaceAdmin
	{
		public static Game Init()
		{
			ViewOfInitialScreen.Build();
			return new Game(DisplayLogic.GameMode, DisplayLogic.s_BoardDimensions);
		}
	}
}