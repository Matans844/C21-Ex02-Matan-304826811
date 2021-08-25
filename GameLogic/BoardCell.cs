using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.GameLogic
{
	public class BoardCell
	{
		public uint Column { get; }

		public uint Row { get; }

		public eBoardCellType CellType { get; }

		public BoardCell(uint i_Column, uint i_Row, eBoardCellType i_CellType)
		{
			this.Column = i_Column;
			this.Row = i_Row;
			this.CellType = i_CellType;
		}

		//public BoardCell(eBoardCellType i_CellType)
		//{
		//	this.r_CellType = i_CellType;
		//}
	}

	public enum eBoardCellType
	{
		Empty = 0,
		XDisc = 1,
		ODisc = 2
	}
}
