using System.Linq;

namespace C21_Ex02_Matan_304826811.Toolkit
{
	public static class EnumerationFor2DArrays<T>
	{
		private const int k_RowDimension = 0;
		private const int k_ColumnDimension = 0;

		public static T[] GetColumn(T[,] i_Matrix, int i_ColumnNumber)
		{
			return Enumerable.Range(0, i_Matrix.GetLength(k_RowDimension))
				.Select(i_IndexForEnumeration => i_Matrix[i_IndexForEnumeration, i_ColumnNumber])
				.ToArray();
		}

		public static T[] GetRow(T[,] i_Matrix, int i_RowNumber)
		{
			return Enumerable.Range(0, i_Matrix.GetLength(k_ColumnDimension))
				.Select(i_IndexForEnumeration => i_Matrix[i_RowNumber, i_IndexForEnumeration])
				.ToArray();
		}
    }
}
