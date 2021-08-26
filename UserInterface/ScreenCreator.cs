﻿using System;
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

		private static int s_ScreenBoardWidthInChar;

		public static StringBuilder ScreenBoardBuilder { get; set; }

		public static int ScreenBoardRowWidthInChar
		{
			get => s_ScreenBoardWidthInChar;
			set => s_ScreenBoardWidthInChar = (k_HeaderSpaceWidth * value) + k_SpaceBetweenCellCenterAndSeparatingChar;
		}

		public static UserInterfaceAdmin GameUserInterfaceAdmin { get; set; }

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
	}
}
