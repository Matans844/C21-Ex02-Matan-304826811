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

		public static readonly string sr_GoodbyeMessageBeforeFirstGame = string.Format(
			"I see You have chosen to postpone your defeat.{0}It is brave to be honest.{0}Goodbye, brave friend!{0}{0}",
			Environment.NewLine);

		public static readonly string sr_GoodbyeMessageAfterFirstGameStart = string.Format(
			"It is no shame to admit defeat. Dust yourself up, and try again.{0}I will be waiting.{0}Goodbye for now!{0}{0}",
			Environment.NewLine);

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

			if (int.TryParse(responseFromUser, out var dimensionChosen))
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

		public int PromptForMove()
		{
			return this.askForAnotherMove();
		}

		private int askForAnotherMove()
		{
			// TODO: Before each move prompt, create draw (implement ScreenCreator) board screen (ViewOfBoardScreen). Get input. Checks required: null, int, validity (does board support this int?).
			throw new NotImplementedException();
		}

		public bool PromptForAnotherGame()
		{
			return this.askForAnotherGame();
		}

		private bool askForAnotherGame()
		{
			// TODO: Prompt a message. Get Input. Checks required: null, bool. This was done with ints for dimensions.
			throw new NotImplementedException();
		}

		private string identifyExitKey(string i_ResponseFromUser)
		{
			if (i_ResponseFromUser.ToUpper() == UserInterfaceAdmin.k_QuitKey)
			{
				this.GameUserInterfaceAdmin.IsEscapeKeyOn = true;

				if (this.GameUserInterfaceAdmin.IsPlayerQuittingGame())
				{
					Console.WriteLine("Press Enter to exit...");
					Console.ReadLine();
					Environment.Exit(0);
				}
			}

			return i_ResponseFromUser;
		}

		public void GetAndSetValidGameModeFromUser()
		{
			this.getAndSetValidGameModeFromUser();
		}

		// Updates DisplayLogic's GameMode field.
		private void getAndSetValidGameModeFromUser()
		{
			string responseFromUser = getFirstNotNullInputFromUser(sr_EnterModePromptMessage);

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
			return this.identifyExitKey(Console.ReadLine());
		}

		private string getFirstNotNullInputFromUser(string i_PromptToUser)
		{
			Screen.Clear();
			Console.Write(i_PromptToUser);
			return this.identifyExitKey(Console.ReadLine()) ?? getNotNullInputFromUserAfterError(i_PromptToUser);
		}

		public void SayGoodbye(ePhaseOfUserInterface i_PhaseOfUserInterface)
		{
			Screen.Clear();

			switch (i_PhaseOfUserInterface)
			{
				case ePhaseOfUserInterface.InitialScreen:
					Console.Write(sr_GoodbyeMessageBeforeFirstGame);
					break;

				case ePhaseOfUserInterface.BoardScreen:
					Console.Write(sr_GoodbyeMessageAfterFirstGameStart);
					break;

				case ePhaseOfUserInterface.Terminated:
					break;
			}
		}
	}
}
