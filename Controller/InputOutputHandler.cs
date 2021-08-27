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
		private static readonly string sr_ChooseInRange = $"{Environment.NewLine}Please choose a valid value for column";

		private static readonly string sr_PromptForNextMove =
			$"{Environment.NewLine}Choose column to slide disk in, or press '{k_QuitKey}' to quit current game: ";

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

		public int PromptForMoveOnDisplayedBoard()
		{
			return this.getMove();
		}

		private void getIntegerAndCheck(out int io_ColumnChosenFromConsoleBoard, out bool o_IsInputParsedToInt, out bool o_IsInputValid, out bool o_IsOutOfRange, eErrorInPreviousInput i_PreviousInputError)
		{
			bool hasLeadingZero = false;

			// Validation: Exit key. We proceed only if we got a valid string.
			string responseFromUser = this.getNewInputAndCheckForExit(sr_PromptForNextMove, i_PreviousInputError);

			hasLeadingZero = responseFromUser[0] == (char)0;

			// Validation: Type.
			o_IsInputParsedToInt = int.TryParse(responseFromUser, out io_ColumnChosenFromConsoleBoard);

			// Checking type and leading zeros (00 is not accepted)
			if ((!o_IsInputParsedToInt)
				|| (hasLeadingZero && io_ColumnChosenFromConsoleBoard != 0)
				|| (hasLeadingZero && io_ColumnChosenFromConsoleBoard == 0 && responseFromUser.ToCharArray().Length > 1))
			{
				// No need to proceed if type is does not match.
				o_IsInputValid = false;
				o_IsOutOfRange = false;
			}
			else
			{
				// The player was shown a board that starts from 1, not 0.
				io_ColumnChosenFromConsoleBoard--;

				// Validation: Value is in range
				o_IsInputValid = this.GameUserInterfaceAdmin.MyGameLogicUnit.GameBoard.IsColumnAvailableForDisc(
					io_ColumnChosenFromConsoleBoard, out o_IsOutOfRange);
			}
		}

		public void DeclarePointStatus()
		{
			Console.WriteLine();
			Console.WriteLine(MessageCreator.StatusOfPoints);
		}

		public void DeclareGameResult()
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

			if (this.GameUserInterfaceAdmin.IsEscapeKeyOn == false)
			{
				Console.Write(i_PromptToUser);
			}

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

			this.getIntegerAndCheck(out int columnChosen, out bool isInputParsedToInt, out bool isInputValid, out bool isOutOfRange, eErrorInPreviousInput.No);

			while (k_LoopUntilAllInputRequirementsAreMet)
			{
				switch (isInputParsedToInt)
				{
					// TODO debugging this
					case true when isInputValid:
						// Move is valid
						return columnChosen;
					case true:
						// Move did not succeed because move was to a full column or out of range
						Console.WriteLine(isOutOfRange ? sr_ChooseInRange : sr_ColumnIsFull);

						this.getIntegerAndCheck(out columnChosen, out isInputParsedToInt, out isInputValid, out isOutOfRange, eErrorInPreviousInput.No);
						break;
					case false:
						// Input did not succeed because input had error.
						this.getIntegerAndCheck(out columnChosen, out isInputParsedToInt, out isInputValid, out isOutOfRange, eErrorInPreviousInput.Yes);
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
			if (int.TryParse(responseFromUser, out int dimensionChosen))
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

			if (int.TryParse(responseFromUser, out int gameModeChosenByUser)
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
			if (!int.TryParse(responseFromUser, out int answerChosenAsInt)
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
