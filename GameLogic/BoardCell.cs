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
		private readonly uint r_Column;
		private readonly uint r_Row;
		private readonly eBoardCellType r_CellType;

		public uint Column => this.r_Column;

		public uint Row => this.r_Row;

		public eBoardCellType CellType => this.r_CellType;

		public BoardCell(uint i_Column, uint i_Row, eBoardCellType i_CellType)
		{
			this.r_Column = i_Column;
			this.r_Row = i_Row;
			this.r_CellType = i_CellType;
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
