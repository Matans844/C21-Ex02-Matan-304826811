using System;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.GameLogic;

using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.Controller
{
	public class InputOutputHandler
	{
		public const string k_QuitKey = "Q";
		public const bool k_LoopUntilAllInputRequirementsAreMet = true;

		private static readonly string sr_InvalidInputMessage = $"Invalid input!{Environment.NewLine}";
		private static readonly string sr_ColumnIsFull = $"{Environment.NewLine}Chosen column is already full. Choose again.";

		private static readonly string sr_PromptForNextMove =
			$"Choose column to slide disk in, or press {k_QuitKey} to quit current game: ";

		public UserInterfaceAdmin GameUserInterfaceAdmin { get; }

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

		public int PromptForMove()
		{
			return this.getMove();
		}

		private void getIntegerAndCheck(out int io_ColumnChosen, out bool o_IsInputParsedToInt, out bool o_IsInputInRange, eErrorInPreviousInput i_PreviousInputError)
		{
			// Validation: Exit key
			string responseFromUser = this.getNewInputAndCheckForExit(sr_PromptForNextMove, i_PreviousInputError);

			// Validation: Type
			o_IsInputParsedToInt = int.TryParse(responseFromUser, out io_ColumnChosen);

			// Validation: Value
			o_IsInputInRange = this.GameUserInterfaceAdmin.MyGameLogicUnit.GameBoard.IsColumnAvailableForDisc(io_ColumnChosen);
		}

		internal void DeclarePointStatus()
		{
			Console.WriteLine();
			Console.WriteLine(MessageCreator.StatusOfPoints);
		}

		internal void DeclareGameResult()
		{
			Console.WriteLine();
			Console.WriteLine(MessageCreator.GameResultsMessage);
		}

		public bool PromptForAnotherGame()
		{
			return this.askForAnotherGame();
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

		private string getNewInputAndCheckForExit(string i_PromptToUser, eErrorInPreviousInput i_ErrorInInput)
		{
			string responseFromUser;

			if (this.GameUserInterfaceAdmin.PhaseOfUserInterface == ePhaseOfUserInterface.InitialScreen)
			{
				Screen.Clear();
			}

			if (i_ErrorInInput == eErrorInPreviousInput.Yes)
			{
				Console.WriteLine(sr_InvalidInputMessage);
			}

			Console.Write(i_PromptToUser);
			responseFromUser = Console.ReadLine();

			return this.identifyExitKey(responseFromUser);
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

		private int getMove()
		{
			Screen.Clear();
			this.GameUserInterfaceAdmin.MyBoardScreenView.DrawBoard();

			this.getIntegerAndCheck(out int columnChosen, out bool isInputParsedToInt, out bool isInputInRange, eErrorInPreviousInput.No);

			while (k_LoopUntilAllInputRequirementsAreMet)
			{
				switch (isInputParsedToInt)
				{
					// TODO debugging this
					case true when isInputInRange:
						// Move is valid
						return columnChosen;
					case true:
						// Move did not succeed because move was out of range.
						Console.WriteLine(sr_ColumnIsFull);

						this.getIntegerAndCheck(out columnChosen, out isInputParsedToInt, out isInputInRange, eErrorInPreviousInput.No);
						break;

					case false:
						// Input did not succeed because input had error.
						this.getIntegerAndCheck(out columnChosen, out isInputParsedToInt, out isInputInRange, eErrorInPreviousInput.Yes);
						break;
				}
			}
		}

		private void getAndSetValidDimensionsFromUser(ref GameBoardDimensions io_BoardDimensions, eBoardDimension i_DimensionToSet)
		{
			string promptToUser = i_DimensionToSet == eBoardDimension.Height
									? MessageCreator.PromptForBoardHeight
									: MessageCreator.PromptForBoardWidth;

			// Checking for exit key. We still have to validate type and value.
			string responseFromUser = this.getNewInputAndCheckForExit(promptToUser, eErrorInPreviousInput.No);

			// We are updating a struct that guards our dimension constraints.
			// Validation of type and value goes through the struct guards.
			if (int.TryParse(responseFromUser, out var dimensionChosen))
			{
				io_BoardDimensions.SetterByChoice(i_DimensionToSet, dimensionChosen);
			}

			while (io_BoardDimensions.GetterByChoice(i_DimensionToSet) == (int)eBoardDimension.NotInitiated)
			{
				responseFromUser = this.getNewInputAndCheckForExit(promptToUser, eErrorInPreviousInput.Yes);

				if (int.TryParse(responseFromUser, out dimensionChosen))
				{
					io_BoardDimensions.SetterByChoice(i_DimensionToSet, dimensionChosen);
				}
			}
		}

		// Updates DisplayLogic's GameMode field.
		private void getAndSetValidGameModeFromUser()
		{
			string responseFromUser = this.getNewInputAndCheckForExit(MessageCreator.PromptForGameMode, eErrorInPreviousInput.No);

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
					responseFromUser = this.getNewInputAndCheckForExit(MessageCreator.PromptForGameMode, eErrorInPreviousInput.Yes);
				}
				while (!(int.TryParse(responseFromUser, out gameModeChosenByUser)
						&& Enum.IsDefined(typeof(eGameMode), gameModeChosenByUser)
						&& gameModeChosenByUser != (int)eGameMode.NotInitiated));

				this.GameUserInterfaceAdmin.MyGameDisplayLogic.GameMode = (eGameMode)gameModeChosenByUser;
			}
		}

		private bool askForAnotherGame()
		{
			string responseFromUser;
			bool inputIsValidByNullTypeValue = false;

			Console.Write(MessageCreator.k_PromptForAnotherGame);

			// Not empty validation.
			responseFromUser = this.getNewInputAndCheckForExit(sr_PromptForNextMove, eErrorInPreviousInput.No);

			// Type and value validation.
			if (!int.TryParse(responseFromUser, out var answerChosenAsInt)
				|| (answerChosenAsInt != (int)eBooleanByInt.No && answerChosenAsInt != (int)eBooleanByInt.Yes))
			{
				while (!inputIsValidByNullTypeValue)
				{
					responseFromUser = this.getNewInputAndCheckForExit(sr_PromptForNextMove, eErrorInPreviousInput.Yes);

					if (int.TryParse(responseFromUser, out answerChosenAsInt)
						&& (answerChosenAsInt == (int)eBooleanByInt.No || answerChosenAsInt == (int)eBooleanByInt.Yes))
					{
						inputIsValidByNullTypeValue = true;
					}
				}
			}

			return answerChosenAsInt == (int)eBooleanByInt.Yes;
		}

		public enum eBooleanByInt
		{
			No = 0,
			Yes = 1
		}

		public enum eErrorInPreviousInput
		{
			No = 0,
			Yes = 1
		}
	}
}
