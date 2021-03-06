namespace C21_Ex02_Matan_304826811.GameLogic
{
	public class BoardCell
	{
		public uint Column { get; }

		public uint Row { get; }

		public eBoardCellType CellType { get; set; }

		public BoardCell(uint i_Row, uint i_Column, eBoardCellType i_CellType)
		{
			this.Row = i_Row;
			this.Column = i_Column;
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
				bool flagSameType = this.HasSameTypeAs(disc);

				if (!flagSameType)
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