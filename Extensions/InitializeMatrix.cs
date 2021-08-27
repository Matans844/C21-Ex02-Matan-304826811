using System;
using C21_Ex02_Matan_304826811.GameLogic;

namespace C21_Ex02_Matan_304826811.Extensions
{
	public static class InitializeMatrix
	{
		public const int k_RowDimension = 0;
		public const int k_ColumnDimension = 1;

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
			int heightOfMatrix = io_Matrix.GetLength(k_RowDimension);
			int widthOfMatrix = io_Matrix.GetLength(k_ColumnDimension);

			for (int i = 0; i < heightOfMatrix; i++)
			{
				for (int j = 0; j < widthOfMatrix; j++)
				{
					uint rowOfCell = (uint)i;
					uint columnOfCell = (uint)j;
					io_Matrix[i, j] = new BoardCell(rowOfCell, columnOfCell, eBoardCellType.Empty);
				}
			}
		}
	}
}
