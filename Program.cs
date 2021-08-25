using System;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;

namespace C21_Ex02_Matan_304826811
{
	public class Program
	{
		public static void Main()
		{
			UserInterfaceAdmin userInterfaceAdmin = new UserInterfaceAdmin();
			userInterfaceAdmin.InitializeGame();
			Console.WriteLine("Press Enter to exit...");
			Console.ReadLine();
		}
	}
}
