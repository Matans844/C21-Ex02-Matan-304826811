using System;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.GameLogic;

using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.Controller
{
	public class InputOutputHandler
	{
		public const string k_QuitKey = "Q";
		public const bool k_LoopUntilAllInputConditionsAreMetAndBreakByReturn = true;

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

		private void getAndSetValidDimensionsFromUser(ref GameBoardDimensions io_BoardDimensions, eBoardDimension i_DimensionToSet)
		{
			string promptToUser = i_DimensionToSet == eBoardDimension.Height
									? MessageCreator.PromptForBoardHeight
									: MessageCreator.PromptForBoardWidth;

			// Checking for null. We still have to validate type and value.
			string responseFromUser = this.getNewInputAndCheckForExit(promptToUser, eErrorInFirstInput.No);

			// We are updating a struct that guards our dimension constraints.
			// Validation of type and value goes through the struct guards.
			if (int.TryParse(responseFromUser, out var dimensionChosen))
			{
				io_BoardDimensions.SetterByChoice(i_DimensionToSet, dimensionChosen);
			}

			while (io_BoardDimensions.GetterByChoice(i_DimensionToSet) == (int)eBoardDimension.NotInitiated)
			{
				responseFromUser = this.getNewInputAndCheckForExit(promptToUser, eErrorInFirstInput.No);

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
			int returnedMove;
			bool inputIsValidByNullTypeValue = false;
			bool isInputParsedToInt;
			bool isInputInRange;
			eAttemptedOutOfRange moveOutOfRange = eAttemptedOutOfRange.No;
			int columnChosen;

			Screen.Clear();
			this.GameUserInterfaceAdmin.MyBoardScreenView.DrawBoard();

			// Exit key validation
			responseFromUser = this.getNewInputAndCheckForExit(sr_PromptForNextMove, eErrorInFirstInput.No);

			// Type validation
			isInputParsedToInt = int.TryParse(responseFromUser, out columnChosen);

			// Value validation
			isInputInRange = this.isInputInRange(columnChosen, out moveOutOfRange);
			// TODO debuggin this
			if(isInputParsedToInt && isInputInRange)
			{
				// First input succeeded
				returnedMove = columnChosen;
			}
			else if (!isInputParsedToInt)
			{
				// First input did not succeed because input had error.
				while (!inputIsValidByNullTypeValue)
				{
					// Checking for exit key.
					responseFromUser = this.getNewInputAndCheckForExit(sr_PromptForNextMove, eErrorInFirstInput.Yes);

					if (int.TryParse(responseFromUser, out columnChosen)
						&& this.GameUserInterfaceAdmin.MyGameLogicUnit.GameBoard.IsColumnAvailableForDisc(columnChosen, out moveOutOfRange))
					{
						returnedMove = columnChosen;
						inputIsValidByNullTypeValue = true;
					}
				}
			}
			else
			{
				// First input did not succeed because move was out of range.
				Console.WriteLine(sr_ColumnIsFull);

				while (!inputIsValidByNullTypeValue)
				{
					// Checking for exit key.
					responseFromUser = this.getNewInputAndCheckForExit(sr_PromptForNextMove, eErrorInFirstInput.No);

					if (int.TryParse(responseFromUser, out columnChosen)
						&& this.GameUserInterfaceAdmin.MyGameLogicUnit.GameBoard.IsColumnAvailableForDisc(columnChosen, out moveOutOfRange))
					{
						returnedMove = columnChosen;
						inputIsValidByNullTypeValue = true;
					}
				}
			}
		}

		private bool isInputInRange(int i_ColumnChosen, out eAttemptedOutOfRange o_ChosenMoveOutOfRange)
		{
			return this.GameUserInterfaceAdmin.MyGameLogicUnit.GameBoard.IsColumnAvailableForDisc(
				i_ColumnChosen, out o_ChosenMoveOutOfRange);
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

		private bool askForAnotherGame()
		{
			string responseFromUser;
			bool inputIsValidByNullTypeValue = false;

			Console.Write(MessageCreator.k_PromptForAnotherGame);

			// Not empty validation.
			responseFromUser = this.getNewInputAndCheckForExit(sr_PromptForNextMove, eErrorInFirstInput.No);

			// Type and value validation.
			if (!int.TryParse(responseFromUser, out var answerChosenAsInt)
				|| (answerChosenAsInt != (int)eBooleanByInt.No && answerChosenAsInt != (int)eBooleanByInt.Yes))
			{
				while (!inputIsValidByNullTypeValue)
				{
					responseFromUser = this.getNewInputAndCheckForExit(sr_PromptForNextMove, eErrorInFirstInput.Yes);

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
			string responseFromUser = this.getNewInputAndCheckForExit(MessageCreator.PromptForGameMode, eErrorInFirstInput.No);

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
					responseFromUser = this.getNewInputAndCheckForExit(MessageCreator.PromptForGameMode, eErrorInFirstInput.Yes);
				}
				while (!(int.TryParse(responseFromUser, out gameModeChosenByUser)
						&& Enum.IsDefined(typeof(eGameMode), gameModeChosenByUser)
						&& gameModeChosenByUser != (int)eGameMode.NotInitiated));

				this.GameUserInterfaceAdmin.MyGameDisplayLogic.GameMode = (eGameMode)gameModeChosenByUser;
			}
		}

		private string getNewInputAndCheckForExit(string i_PromptToUser, eErrorInFirstInput i_ErrorInInput)
		{
			string responseFromUser;

			if (this.GameUserInterfaceAdmin.PhaseOfUserInterface == ePhaseOfUserInterface.InitialScreen)
			{
				Screen.Clear();
			}

			if (i_ErrorInInput == eErrorInFirstInput.Yes)
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

		public enum eBooleanByInt
		{
			No = 0,
			Yes = 1
		}

		public enum eErrorInFirstInput
		{
			No = 0,
			Yes = 1
		}
	}
}
