using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Array;

namespace C21_Ex02_Matan_304826811.Extensions
{
	public static class OperateOn2DArrays
	{
		public static int[][] HorizontalConcat(int[][] i_2DArray1, int[][] i_2DArray2)
		{
			int[][] concatenatedArray = new int[i_2DArray1.Length + i_2DArray2.Length][];

			Copy(i_2DArray1, 0, concatenatedArray, 0, i_2DArray1.Length);
			Copy(i_2DArray2, 0, concatenatedArray, i_2DArray1.Length, i_2DArray2.Length);

			return concatenatedArray;
		}
	}
}
