using System;
using System.Text;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using C21_Ex02_Matan_304826811.Views;
using C21_Ex02_Matan_304826811.Toolkit;
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
									? ScreenCreator.PromptForBoardHeight
									: ScreenCreator.PromptForBoardWidth;

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
			responseFromUser = getFirstNotNullInputFromUser(sr_PromptForNextMove);

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
			Console.WriteLine(ScreenCreator.StatusOfPoints);
		}

		internal void DeclareGameResult()
		{
			// TODO: Who won?
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
			string responseFromUser = getFirstNotNullInputFromUser(ScreenCreator.PromptForGameMode);

			Screen.Clear();
			Console.Write(ScreenCreator.PromptForGameMode);

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
					responseFromUser = this.getNotNullInputFromUserAfterError(ScreenCreator.PromptForGameMode);
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
					Console.Write(ScreenCreator.GoodbyeMessageBeforeFirstGame);
					break;

				case ePhaseOfUserInterface.BoardScreen:
					Console.Write(ScreenCreator.GoodbyeMessageAfterFirstGameStart);
					break;

				case ePhaseOfUserInterface.Terminated:
					break;
			}
		}
	}
}
