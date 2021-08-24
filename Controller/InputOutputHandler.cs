using System;
using System.Text;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.Controller
{
	using System.Security.Policy;

	class InputOutputHandler
	{
		private const string k_QuitKey = "Q";
		private static readonly string sr_InvalidInputMessage = $"Invalid input!{Environment.NewLine}";
		private static readonly string sr_PromptBoardHeightMessage =
			$"Enter board height, between {Constraints.BoardDimensions.HeightLowerLimit} and {Constraints.BoardDimensions.HeightUpperLimit}: ";
		private static readonly string sr_PromptBoardWidthMessage =
			$"Enter board width, between {Constraints.BoardDimensions.WidthLowerLimit} and {Constraints.BoardDimensions.WidthUpperLimit}: ";
		private static readonly string sr_EnterModePromptMessage = string.Format(
			"Choose game mode: {0}Enter {1:D} to play against a human. {0}Enter {2:D} to play against a computer. {0}",
			Environment.NewLine, eGameMode.PlayerVsPlayer, eGameMode.PlayerVsComputer);

		// Updates DisplayLogic's GameDimensions struct.
		internal static void GetDimensions()
		{
			getDimensionFromUser(ref DisplayLogic.s_BoardDimensions, eBoardDimension.Height);
			getDimensionFromUser(ref DisplayLogic.s_BoardDimensions, eBoardDimension.Width);
		}

		private static void getDimensionFromUser(ref GameBoardDimensions io_BoardDimensions, eBoardDimension i_DimensionToSet)
		{
			int dimensionChosen;
			string promptToUser = i_DimensionToSet == eBoardDimension.Height ? sr_PromptBoardHeightMessage : sr_PromptBoardWidthMessage;
			string responseFromUser = getFirstNotNullInputFromUser(promptToUser);

			if (int.TryParse(responseFromUser, out dimensionChosen))
			{
				io_BoardDimensions.SetterByChoice(i_DimensionToSet, dimensionChosen);
			}

			while (io_BoardDimensions.GetterByChoice(i_DimensionToSet) == (int)eBoardDimension.NotInitiated)
			{
				responseFromUser = getNotNullInputFromUserAfterError(promptToUser);

				if (int.TryParse(responseFromUser, out dimensionChosen))
				{
					io_BoardDimensions.SetterByChoice(i_DimensionToSet, dimensionChosen);
				}
			}
		}

		private static string getFirstNotNullInputFromUser(string o_PromptToUser)
		{
			Screen.Clear();
			Console.Write(o_PromptToUser);
			return Console.ReadLine() ?? getNotNullInputFromUserAfterError(o_PromptToUser);
		}

		// Updates DisplayLogic's GameMode field.
		internal static void GetGameMode()
		{
			int gameModeChosen;
			string responseFromUser = getFirstNotNullInputFromUser(sr_EnterModePromptMessage);

			Screen.Clear();
			Console.Write(sr_EnterModePromptMessage);

			if (int.TryParse(responseFromUser, out gameModeChosen)
				&& Enum.IsDefined(typeof(eGameMode), gameModeChosen)
				&& gameModeChosen != (int)eGameMode.NotInitiated)
			{
				DisplayLogic.GameMode = (eGameMode)gameModeChosen;
			}
			else
			{
				do
				{
					responseFromUser = getNotNullInputFromUserAfterError(sr_EnterModePromptMessage);
				}
				while (!(int.TryParse(responseFromUser, out gameModeChosen)
						&& Enum.IsDefined(typeof(eGameMode), gameModeChosen)
						&& gameModeChosen != (int)eGameMode.NotInitiated));

				DisplayLogic.GameMode = (eGameMode)gameModeChosen;
			}
		}

		private static string getNotNullInputFromUserAfterError(string o_PromptToUser)
		{
			return getNewInputAfterError(o_PromptToUser) ?? getNotNullInputFromUserAfterError(o_PromptToUser);
		}

		private static string getNewInputAfterError(string o_PromptToUser)
		{
			Screen.Clear();
			Console.WriteLine(sr_InvalidInputMessage);
			Console.Write(o_PromptToUser);
			return Console.ReadLine();
		}
	}
}
