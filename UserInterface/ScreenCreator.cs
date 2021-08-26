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
	public class ScreenCreator
	{
		private const int k_SpaceColumnHeaderPrefix = 2;
		private const int k_SpaceBetweenColumnHeaders = 3;
		private const int k_HeaderSpaceWidth = 4;
		private const int k_SpaceBetweenCellCenterAndSeparatingChar = 1;
		private const char k_HorizontalLineFillerChar = '=';
		private const string k_VerticalSeparatingChar = "|";
		private const string k_EmptyDiscChar = " ";
		private const string k_XDiscChar = "X";
		private const string k_ODiscChar = "O";

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

		private static readonly string sr_GoodbyeMessageBeforeFirstGame = string.Format(
			"I see You have chosen to postpone your defeat.{0}It is brave to be honest.{0}Goodbye, brave friend!{0}{0}",
			Environment.NewLine);

		private static readonly string sr_GoodbyeMessageAfterFirstGameStart = string.Format(
			"It is no shame to admit defeat. Dust yourself up, and try again.{0}I will be waiting.{0}Goodbye for now!{0}{0}",
			Environment.NewLine);

		private static int s_ScreenBoardWidthInChar;

		public static StringBuilder ScreenBoardBuilder { get; set; }

		public static int ScreenBoardRowWidthInChar
		{
			get => s_ScreenBoardWidthInChar;
			set => s_ScreenBoardWidthInChar = (k_HeaderSpaceWidth * value) + k_SpaceBetweenCellCenterAndSeparatingChar;
		}

		public static UserInterfaceAdmin GameUserInterfaceAdmin { get; set; }

		public static string GoodbyeMessageAfterFirstGameStart => sr_GoodbyeMessageAfterFirstGameStart;

		public static string GoodbyeMessageBeforeFirstGame => sr_GoodbyeMessageBeforeFirstGame;

		public static string PromptForGameMode => sr_EnterModePromptMessage;

		public static string PromptForBoardWidth => sr_PromptBoardWidthMessage;

		public static string PromptForBoardHeight => sr_PromptBoardHeightMessage;

		public static string StatusOfPoints { get; set; }

		public ScreenCreator(UserInterfaceAdmin i_MyUserInterfaceAdmin)
		{
			GameUserInterfaceAdmin = i_MyUserInterfaceAdmin;
		}

		private static string buildColumnHeaderRow()
		{
			StringBuilder columnHeaderRow = new StringBuilder(k_SpaceColumnHeaderPrefix);
			int numberOfColumnsInBoard = GameUserInterfaceAdmin.MyGameDisplayLogic.m_BoardDimensions.Width;
			string nextColumnHeader;

			for (int i = 0; i < numberOfColumnsInBoard; i++)
			{
				nextColumnHeader = $"{(char)i,k_SpaceBetweenColumnHeaders}";
				columnHeaderRow.Append(nextColumnHeader);
			}

			columnHeaderRow.Append($"{Environment.NewLine}");

			return columnHeaderRow.ToString();
		}

		private static string buildSeparatingFillerRow()
		{
			return new string(k_HorizontalLineFillerChar, ScreenBoardRowWidthInChar);
		}

		private static string getCellStringIdentity(eBoardCellType i_CellType)
		{
			string cellString = k_EmptyDiscChar;

			switch (i_CellType)
			{
				case eBoardCellType.Empty:
					break;

				case eBoardCellType.XDisc:
					cellString = k_XDiscChar;
					break;

				case eBoardCellType.ODisc:
					cellString = k_ODiscChar;
					break;
			}

			return cellString;
		}

		private static string buildDiscRow(int i_RowOfBoard)
		{
			StringBuilder discRow = new StringBuilder(k_VerticalSeparatingChar);
			string nextColumnDisk;

			BoardCell[] cellRowFromGameBoard = EnumerationFor2DArrays<BoardCell>.GetRow(
				GameUserInterfaceAdmin.MyGameLogicUnit.GameBoard.BoardCellMatrix, i_RowOfBoard);

			foreach (BoardCell cell in cellRowFromGameBoard)
			{
				nextColumnDisk =
					$"{getCellStringIdentity(cell.CellType),k_SpaceBetweenCellCenterAndSeparatingChar}{k_VerticalSeparatingChar,k_SpaceBetweenCellCenterAndSeparatingChar}";

				discRow.Append(nextColumnDisk);
			}

			discRow.Append($"{Environment.NewLine}");

			return discRow.ToString();
		}

		private static string buildBoard()
		{
			ScreenBoardBuilder = new StringBuilder(buildColumnHeaderRow());
			int numberOfRowsInBoard = GameUserInterfaceAdmin.MyGameDisplayLogic.m_BoardDimensions.Height;

			for (int i = 0; i < numberOfRowsInBoard; i++)
			{
				ScreenBoardBuilder.Append(buildDiscRow(i));
				ScreenBoardBuilder.Append(buildSeparatingFillerRow());
			}

			return ScreenBoardBuilder.ToString();
		}

		public static void PrintGameBoard()
		{
			Console.WriteLine(buildBoard());
		}

		private static void setPointsStatus()
		{
			int player1Points = GameUserInterfaceAdmin.MyGameLogicUnit.Player1WithXs.PointsEarned;
			int player2Points = GameUserInterfaceAdmin.MyGameLogicUnit.Player2WithOs.PointsEarned;
			StatusOfPoints = string.Format("Points so far:{0}Player 1 has {1} points.{0}Player 2 has {2} points.", Environment.NewLine, player1Points, player2Points);
		}

		public static void UpdateStatusOfPoints()
		{
			setPointsStatus();
		}

	}
}
