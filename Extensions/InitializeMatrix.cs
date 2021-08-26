namespace C21_Ex02_Matan_304826811.Extensions
{
	using C21_Ex02_Matan_304826811.GameLogic;

	public static class InitializeMatrix
	{
		public const int k_RowDimension = 0;
		public const int k_ColumnDimension = 0;

		public static void InitWithEmptyCells<T>(this T[,] io_Matrix)
			where T : new()
		{
			for (int i = 0; i < io_Matrix.GetLength(k_RowDimension); i++)
			{
				for (int j = 0; j < io_Matrix.GetLength(k_ColumnDimension); j++)
				{
					io_Matrix[i, j] = new T();
				}
			}
		}

		public static void InitWithBoardCells(this BoardCell[,] io_Matrix)
		{
			for (int i = 0; i < io_Matrix.GetLength(k_RowDimension); i++)
			{
				for (int j = 0; j < io_Matrix.GetLength(k_ColumnDimension); j++)
				{
					uint rowOfCell = (uint)i;
					uint columnOfCell = (uint)j;
					io_Matrix[i, j] = new BoardCell(rowOfCell, columnOfCell, eBoardCellType.Empty);
				}
			}
		}
	}
}
