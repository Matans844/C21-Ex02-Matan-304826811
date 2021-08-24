using System;
using System.Text;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.Controller
{
	class InputOutputHandler
	{
		private const string k_QuitKey = "Q";
		private static readonly string sr_InvalidInputMessage = $"Invalid input!{Environment.NewLine}";
		private static readonly string sr_PromptBoardHeightMessage =
			$"Enter board height, between {Constraints.BoardDimensions.HeightLowerLimit} and {Constraints.BoardDimensions.HeightUpperLimit}: ";
		private static readonly string sr_PromptBoardWidthMessage =
			$"Enter board width, between {Constraints.BoardDimensions.WidthLowerLimit} and {Constraints.BoardDimensions.WidthUpperLimit}: ";
		private static readonly string sr_EnterModePromptMessage = @"Choose game mode:
				Enter 0 to play against a computer.
				Enter 1 to play against a human.";

		internal static void GetDimensions()
		{
			getDimensionFromUser(ref DisplayLogic.s_BoardDimensions, eBoardDimension.Height);
			getDimensionFromUser(ref DisplayLogic.s_BoardDimensions, eBoardDimension.Width);
		}

		private static void getDimensionFromUser(ref GameBoardDimensions io_BoardDimension, eBoardDimension i_DimensionToSet)
		{
			string responseFromUser;
			string promptToUser;

			Screen.Clear();
			promptToUser = i_DimensionToSet == eBoardDimension.Height ? sr_PromptBoardHeightMessage : sr_PromptBoardWidthMessage;
			Console.Write(promptToUser);
			responseFromUser = Console.ReadLine();
			int.TryParse(responseFromUser, out int dimensionChosen);
			io_BoardDimension.SetterByChoice(i_DimensionToSet, dimensionChosen);

			while (io_BoardDimension.GetterByChoice(i_DimensionToSet) == (int)eBoardDimension.NotInitiated)
			{
				Screen.Clear();
				Console.WriteLine(sr_InvalidInputMessage);
				Console.Write(promptToUser);
				responseFromUser = Console.ReadLine();
				int.TryParse(responseFromUser, out dimensionChosen);
				io_BoardDimension.SetterByChoice(i_DimensionToSet, dimensionChosen);
			}
		}

		internal static void GetGameMode()
		{
			throw new NotImplementedException();
		}
	}
}
