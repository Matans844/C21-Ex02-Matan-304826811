using System;

using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;

namespace C21_Ex02_Matan_304826811.UserInterface
{
	public class MessageCreator
	{
		public const string k_GameResultsMessageDrawn = "The game ended in a draw!";

		public static string s_PromptForAnotherGame = string.Format(
			"{0}Do you want to play another game?{0}Press 1 for another game.{0}Press 0 to exit.", Environment.NewLine);

		private static string s_GameResultsMessageForWonGame;
		private static string s_StatusOfPoints;

		public static string GameResultsMessageForWonGame
		{
			get => s_GameResultsMessageForWonGame;
			set => s_GameResultsMessageForWonGame = $"Player {value} won! Congratulations!";
		}

		public static string GoodbyeMessageAfterFirstGameStart { get; } = string.Format(
			"It is no shame to admit defeat. Dust yourself up, and try again.{0}I will be waiting.{0}Goodbye for now!{0}",
			Environment.NewLine);

		public static string GoodbyeMessageBeforeFirstGameStarts { get; } = string.Format(
			"I see You have chosen to postpone your defeat.{0}It is brave to be honest.{0}Goodbye, brave friend!{0}",
			Environment.NewLine);

		public static string PromptForGameMode { get; } = string.Format(
			format:
			"Choose game mode:{0}Enter {1:D} to play against a human.{0}Enter {2:D} to play against a computer.{0}",
			Environment.NewLine, arg1: eGameMode.PlayerVsPlayer, arg2: eGameMode.PlayerVsComputer);

		public static string PromptForBoardWidth { get; } =
			$"Enter board width, between {Constraints.BoardDimensions.WidthLowerLimit} and {Constraints.BoardDimensions.WidthUpperLimit}: ";

		public static string PromptForBoardHeight { get; } =
			$"Enter board height, between {Constraints.BoardDimensions.HeightLowerLimit} and {Constraints.BoardDimensions.HeightUpperLimit}: ";

		public static string StatusOfPoints => s_StatusOfPoints;

		public UserInterfaceAdmin GameUserInterfaceAdmin { get; set; }

		public MessageCreator(UserInterfaceAdmin i_MyUserInterfaceAdmin)
		{
			this.GameUserInterfaceAdmin = i_MyUserInterfaceAdmin;
		}

		public void UpdateStatusOfPointsAndPrepareMessage()
		{
			string player1ID = this.GameUserInterfaceAdmin.MyGameLogicUnit.Player1WithXs.PlayerID;
			int player1Points = this.GameUserInterfaceAdmin.MyGameLogicUnit.Player1WithXs.PointsEarned;
			string player2ID = this.GameUserInterfaceAdmin.MyGameLogicUnit.Player2WithOs.PlayerID;
			int player2Points = this.GameUserInterfaceAdmin.MyGameLogicUnit.Player2WithOs.PointsEarned;

			s_StatusOfPoints = string.Format(
				"Points so far:{0}Player {1} has {2} points.{0}Player {3} has {4} points.{0}Very well! Very very well!", Environment.NewLine,
				player1ID, player1Points, player2ID, player2Points);
		}
	}
}