using System;

using C21_Ex02_Matan_304826811.UserInterface;

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
