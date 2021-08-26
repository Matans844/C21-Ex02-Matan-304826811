using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C21_Ex02_Matan_304826811.Toolkit
{
	public static class EnumerationFor2DArrays<T>
	{
		public static T[] GetColumn(T[,] i_Matrix, int i_ColumnNumber)
		{
			return Enumerable.Range(0, i_Matrix.GetLength(0))
				.Select(x => i_Matrix[x, i_ColumnNumber])
				.ToArray();
		}

		public static T[] GetRow(T[,] i_Matrix, int i_RowNumber)
		{
			return Enumerable.Range(0, i_Matrix.GetLength(1))
				.Select(x => i_Matrix[i_RowNumber, x])
				.ToArray();
		}
    }
}
