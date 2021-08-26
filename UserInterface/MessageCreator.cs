using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using C21_Ex02_Matan_304826811.Views;
using C21_Ex02_Matan_304826811.Toolkit;
using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.UserInterface
{
	public class MessageCreator
	{
		public const string k_PromptForAnotherGame = "Do you want to play another game?";

		public static UserInterfaceAdmin GameUserInterfaceAdmin { get; set; }

		public static string GameResultsMessage { get; } =
			$"Player {GameUserInterfaceAdmin.MyGameLogicUnit.GameBoard.BoardReferee.Winner.PlayerID} won";

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

		private static void setPointsStatus()
		{
			int player1ID = GameUserInterfaceAdmin.MyGameLogicUnit.Player1WithXs.PlayerID;
			int player1Points = GameUserInterfaceAdmin.MyGameLogicUnit.Player1WithXs.PointsEarned;
			int player2ID = GameUserInterfaceAdmin.MyGameLogicUnit.Player2WithOs.PlayerID;
			int player2Points = GameUserInterfaceAdmin.MyGameLogicUnit.Player2WithOs.PointsEarned;

			StatusOfPoints =
				$"Points so far:{Environment.NewLine}Player {player1ID} has {player1Points} points.{Environment.NewLine}Player {player2ID} has {player2Points} points.";
		}

		public static void UpdateStatusOfPoints()
		{
			setPointsStatus();
		}
	}
}
