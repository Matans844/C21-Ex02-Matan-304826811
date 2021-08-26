using System.Linq;
using C21_Ex02_Matan_304826811.Extensions;

namespace C21_Ex02_Matan_304826811.Extensions
{
	public static class SlicingMatrices<T>
	{
		public const int k_RowDimension = 0;
		public const int k_ColumnDimension = 0;

		public static T[] GetColumn(T[,] i_Matrix, int i_ColumnNumber)
		{
			return Enumerable.Range(0, i_Matrix.GetLength(k_RowDimension))
				.Select(indexForEnumeration => i_Matrix[indexForEnumeration, i_ColumnNumber])
				.ToArray();
		}

		public static T[] GetRow(T[,] i_Matrix, int i_RowNumber)
		{
			return Enumerable.Range(0, i_Matrix.GetLength(k_ColumnDimension))
				.Select(indexForEnumeration => i_Matrix[i_RowNumber, indexForEnumeration])
				.ToArray();
		}
	}
}
