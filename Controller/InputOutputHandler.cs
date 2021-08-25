using System;
using System.Text;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.Controller
{
	public class InputOutputHandler
	{
		public UserInterfaceAdmin GameUserInterfaceAdmin { get; }

		private static readonly string sr_InvalidInputMessage = $"Invalid input!{Environment.NewLine}";

		private static readonly string sr_PromptBoardHeightMessage =
			$"Enter board height, between {Constraints.BoardDimensions.HeightLowerLimit} and {Constraints.BoardDimensions.HeightUpperLimit}: ";

		private static readonly string sr_PromptBoardWidthMessage =
			$"Enter board width, between {Constraints.BoardDimensions.WidthLowerLimit} and {Constraints.BoardDimensions.WidthUpperLimit}: ";

		private static readonly string sr_EnterModePromptMessage = string.Format(
			format:
			"Choose game mode: {0}Enter {1:D} to play against a human. {0}Enter {2:D} to play against a computer. {0}",
			Environment.NewLine,
			arg1: eGameMode.PlayerVsPlayer,
			arg2: eGameMode.PlayerVsComputer);

		public InputOutputHandler(UserInterfaceAdmin i_GameUserInterfaceAdmin)
		{
			this.GameUserInterfaceAdmin = i_GameUserInterfaceAdmin;
		}

		// Updates DisplayLogic's GameDimensions struct.
		public void GetAndSetValidDimensionsFromUser()
		{
			this.getAndSetValidDimensionsFromUser(ref this.GameUserInterfaceAdmin.MyGameDisplayLogic.m_BoardDimensions, eBoardDimension.Height);
			this.getAndSetValidDimensionsFromUser(ref this.GameUserInterfaceAdmin.MyGameDisplayLogic.m_BoardDimensions, eBoardDimension.Width);
		}

		private void getAndSetValidDimensionsFromUser(ref GameBoardDimensions io_BoardDimensions, eBoardDimension i_DimensionToSet)
		{
			string promptToUser = i_DimensionToSet == eBoardDimension.Height ? sr_PromptBoardHeightMessage : sr_PromptBoardWidthMessage;
			string responseFromUser = getFirstNotNullInputFromUser(promptToUser);

			if (this.GameUserInterfaceAdmin.HasPlayerQuitGame())
			{
				return;
			}

			if (int.TryParse(responseFromUser, out var dimensionChosen))
			{
				io_BoardDimensions.SetterByChoice(i_DimensionToSet, dimensionChosen);
			}

			while (io_BoardDimensions.GetterByChoice(i_DimensionToSet) == (int)eBoardDimension.NotInitiated)
			{
				responseFromUser = getNotNullInputFromUserAfterError(promptToUser);

				if (this.GameUserInterfaceAdmin.HasPlayerQuitGame())
				{
					return;
				}

				if (int.TryParse(responseFromUser, out dimensionChosen))
				{
					io_BoardDimensions.SetterByChoice(i_DimensionToSet, dimensionChosen);
				}
			}
		}

		public int PromptForMove()
		{
			return this.promptForMove();
		}

		private int promptForMove()
		{
			throw new NotImplementedException();
		}

		private ePhaseOfUserInterface identifyExitKey(string i_ResponseFromUser)
		{
			if (i_ResponseFromUser == UserInterfaceAdmin.k_QuitKey)
			{
				this.GameUserInterfaceAdmin.PhaseOfUserInterface = ePhaseOfUserInterface.Terminated;
			}

			return this.GameUserInterfaceAdmin.PhaseOfUserInterface;
		}

		private string getFirstNotNullInputFromUser(string i_PromptToUser)
		{
			Screen.Clear();
			Console.Write(i_PromptToUser);
			return Console.ReadLine() ?? getNotNullInputFromUserAfterError(i_PromptToUser);
		}

		public void GetAndSetValidGameModeFromUser()
		{
			this.getAndSetValidGameModeFromUser();
		}

		// Updates DisplayLogic's GameMode field.
		private void getAndSetValidGameModeFromUser()
		{
			string responseFromUser = getFirstNotNullInputFromUser(sr_EnterModePromptMessage);

			if (this.GameUserInterfaceAdmin.HasPlayerQuitGame())
			{
				return;
			}

			Screen.Clear();
			Console.Write(sr_EnterModePromptMessage);

			if (int.TryParse(responseFromUser, out var gameModeChosenByUser)
				&& Enum.IsDefined(typeof(eGameMode), gameModeChosenByUser)
				&& gameModeChosenByUser != (int)eGameMode.NotInitiated)
			{
				this.GameUserInterfaceAdmin.MyGameDisplayLogic.GameMode = (eGameMode)gameModeChosenByUser;
			}
			else
			{
				do
				{
					responseFromUser = this.getNotNullInputFromUserAfterError(sr_EnterModePromptMessage);

					if (this.GameUserInterfaceAdmin.HasPlayerQuitGame())
					{
						return;
					}
				}
				while (!(int.TryParse(responseFromUser, out gameModeChosenByUser)
						&& Enum.IsDefined(typeof(eGameMode), gameModeChosenByUser)
						&& gameModeChosenByUser != (int)eGameMode.NotInitiated));

				this.GameUserInterfaceAdmin.MyGameDisplayLogic.GameMode = (eGameMode)gameModeChosenByUser;
			}
		}

		private string getNotNullInputFromUserAfterError(string i_PromptToUser)
		{
			return this.getNewInputAfterError(i_PromptToUser) ?? getNotNullInputFromUserAfterError(i_PromptToUser);
		}

		private string getNewInputAfterError(string i_PromptToUser)
		{
			Screen.Clear();
			Console.WriteLine(sr_InvalidInputMessage);
			Console.Write(i_PromptToUser);
			return Console.ReadLine();
		}
	}
}
