using System;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.GameLogic;

using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.Controller
{
	public class InputOutputHandler
	{
		public UserInterfaceAdmin GameUserInterfaceAdmin { get; }

		public const string k_QuitKey = "Q";

		private static readonly string sr_InvalidInputMessage = $"Invalid input!{Environment.NewLine}";

		private static readonly string sr_PromptForNextMove =
			$"Choose column to slide disk in, or press {k_QuitKey} to quit current game: ";

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
			string promptToUser = i_DimensionToSet == eBoardDimension.Height
									? MessageCreator.PromptForBoardHeight
									: MessageCreator.PromptForBoardWidth;

			// Checking for null. We still have to validate type and value.
			string responseFromUser = getFirstNotNullInputFromUser(promptToUser);

			// We are updating a struct that guards our dimension constraints.
			// Validation of type and value goes through the struct guards.
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
			return this.getMove();
		}

		private int getMove()
		{
			string responseFromUser;
			bool inputIsValidByNullTypeValue = false;

			Screen.Clear();
			this.GameUserInterfaceAdmin.MyBoardScreenView.DrawBoard();

			// Null validation.
			responseFromUser = this.getFirstNotNullInputFromUser(sr_PromptForNextMove);

			// Type and value validation.
			if (!int.TryParse(responseFromUser, out var columnChosen)
				|| !this.GameUserInterfaceAdmin.MyGameLogicUnit.GameBoard.IsColumnAvailableForDisc(columnChosen))
			{
				while (!inputIsValidByNullTypeValue)
				{
					responseFromUser = this.getNotNullInputFromUserAfterError(sr_PromptForNextMove);

					if (int.TryParse(responseFromUser, out columnChosen)
						&& this.GameUserInterfaceAdmin.MyGameLogicUnit.GameBoard.IsColumnAvailableForDisc(columnChosen))
					{
						inputIsValidByNullTypeValue = true;
					}
				}
			}

			return columnChosen;
		}

		internal void DeclarePointStatus()
		{
			Console.WriteLine(MessageCreator.StatusOfPoints);
		}

		internal void DeclareGameResult()
		{
			Console.WriteLine(MessageCreator.GameResultsMessage);
		}

		public bool PromptForAnotherGame()
		{
			return this.askForAnotherGame();
		}

		private bool askForAnotherGame()
		{
			string responseFromUser;
			bool inputIsValidByNullTypeValue = false;

			Console.Write(MessageCreator.k_PromptForAnotherGame);

			// Null validation.
			responseFromUser = this.getFirstNotNullInputFromUser(sr_PromptForNextMove);

			// Type and value validation.
			if (!int.TryParse(responseFromUser, out var answerChosenAsInt)
				|| (answerChosenAsInt != (int)eBooleanByInt.No && answerChosenAsInt != (int)eBooleanByInt.Yes))
			{
				while (!inputIsValidByNullTypeValue)
				{
					responseFromUser = this.getNotNullInputFromUserAfterError(sr_PromptForNextMove);

					if (int.TryParse(responseFromUser, out answerChosenAsInt)
						&& (answerChosenAsInt == (int)eBooleanByInt.No || answerChosenAsInt == (int)eBooleanByInt.Yes))
					{
						inputIsValidByNullTypeValue = true;
					}
				}
			}

			return answerChosenAsInt == (int)eBooleanByInt.Yes;
		}

		private string identifyExitKey(string i_ResponseFromUser)
		{
			if (i_ResponseFromUser.ToUpper() == k_QuitKey)
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
			string responseFromUser = getFirstNotNullInputFromUser(MessageCreator.PromptForGameMode);

			Screen.Clear();
			Console.Write(MessageCreator.PromptForGameMode);

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
					responseFromUser = this.getNotNullInputFromUserAfterError(MessageCreator.PromptForGameMode);
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
					Console.WriteLine(MessageCreator.GoodbyeMessageBeforeFirstGameStarts);
					break;

				case ePhaseOfUserInterface.BoardScreen:
					Console.WriteLine(MessageCreator.GoodbyeMessageAfterFirstGameStart);
					break;

				case ePhaseOfUserInterface.Terminated:
					break;
			}
		}
	}

	public enum eBooleanByInt
	{
		No = 0,
		Yes = 1
	}
}
