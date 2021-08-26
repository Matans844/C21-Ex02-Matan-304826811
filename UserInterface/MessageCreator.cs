using System;

using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;

namespace C21_Ex02_Matan_304826811.UserInterface
{
	public class MessageCreator
	{
		public const string k_PromptForAnotherGame = "Do you want to play another game?";

		public static string GameResultsMessage { get; set; }

		public static string GoodbyeMessageAfterFirstGameStart { get; } = string.Format(
			"It is no shame to admit defeat. Dust yourself up, and try again.{0}I will be waiting.{0}Goodbye for now!{0}",
			Environment.NewLine);

		public static string GoodbyeMessageBeforeFirstGameStarts { get; } = string.Format(
			"I see You have chosen to postpone your defeat.{0}It is brave to be honest.{0}Goodbye, brave friend!{0}",
			Environment.NewLine);

		public static string PromptForGameMode { get; } = string.Format(
			format:
			"Choose game mode:{0}Enter {1:D} to play against a human.{0}Enter {2:D} to play against a computer.{0}",
			Environment.NewLine,
			arg1: eGameMode.PlayerVsPlayer,
			arg2: eGameMode.PlayerVsComputer);

		public static string PromptForBoardWidth { get; } =
			$"Enter board width, between {Constraints.BoardDimensions.WidthLowerLimit} and {Constraints.BoardDimensions.WidthUpperLimit}: ";

		public static string PromptForBoardHeight { get; } =
			$"Enter board height, between {Constraints.BoardDimensions.HeightLowerLimit} and {Constraints.BoardDimensions.HeightUpperLimit}: ";

		public static string StatusOfPoints { get; set; }

		public UserInterfaceAdmin GameUserInterfaceAdmin { get; set; }

		public MessageCreator(UserInterfaceAdmin i_MyUserInterfaceAdmin)
		{
			this.GameUserInterfaceAdmin = i_MyUserInterfaceAdmin;
		}

		private void setPointsStatus()
		{
			int player1ID = this.GameUserInterfaceAdmin.MyGameLogicUnit.Player1WithXs.PlayerID;
			int player1Points = this.GameUserInterfaceAdmin.MyGameLogicUnit.Player1WithXs.PointsEarned;
			int player2ID = this.GameUserInterfaceAdmin.MyGameLogicUnit.Player2WithOs.PlayerID;
			int player2Points = this.GameUserInterfaceAdmin.MyGameLogicUnit.Player2WithOs.PointsEarned;

			StatusOfPoints =
				$"Points so far:{Environment.NewLine}Player {player1ID} has {player1Points} points.{Environment.NewLine}Player {player2ID} has {player2Points} points.";
		}

		private void setResultsMessage()
		{
			GameResultsMessage = $"Player {this.GameUserInterfaceAdmin.MyGameLogicUnit.GameBoard.BoardReferee.Winner.PlayerID} won";
		}

		public void UpdateResultsMessage()
		{
			this.setResultsMessage();
		}

		public void UpdateStatusOfPoints()
		{
			this.setPointsStatus();
		}
	}
}
