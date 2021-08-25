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

		public eBoardCellType CellType { get; set; }

		public BoardCell(uint i_Column, uint i_Row, eBoardCellType i_CellType)
		{
			this.Column = i_Column;
			this.Row = i_Row;
			this.CellType = i_CellType;
		}

		public BoardCell ShallowCopy()
		{
			return (BoardCell)this.MemberwiseClone();
		}

		public bool HasSameTypeAs(BoardCell i_AnotherBoardCell)
		{
			return this.CellType == i_AnotherBoardCell.CellType;
		}

		public bool HasSameTypeAs(params BoardCell[] i_OtherBoardCell)
		{
			bool hasSameType = true;

			foreach (BoardCell disc in i_OtherBoardCell)
			{
				if (!this.HasSameTypeAs(disc))
				{
					hasSameType = false;
					break;
				}
			}

			return hasSameType;
		}
	}

	public enum eBoardCellType
	{
		Empty = 0,
		XDisc = 1,
		ODisc = 2
	}
}
