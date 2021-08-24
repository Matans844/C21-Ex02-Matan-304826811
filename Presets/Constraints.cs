using System;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;

namespace C21_Ex02_Matan_304826811.Presets
{
	public static class Constraints
	{
		public struct BoardDimensions
		{
			private const int k_HeightLowerLimit = (int)eBoardLimit.Limit1;
			private const int k_HeightUpperLimit = (int)eBoardLimit.Limit2;
			private const int k_WidthLowerLimit = (int)eBoardLimit.Limit1;
			private const int k_WidthUpperLimit = (int)eBoardLimit.Limit2;

			public static int HeightLowerLimit => k_HeightLowerLimit;

			public static int HeightUpperLimit => k_HeightUpperLimit;

			public static int WidthLowerLimit => k_WidthLowerLimit;

			public static int WidthUpperLimit => k_WidthUpperLimit;
		}
	}

	public enum eBoardLimit
	{
		Default = 0,
		Limit1 = 4,
		Limit2 = 8
	}
}



//{
//private static BoardDimensionsConfiguration s_BoardDimensionsConfiguration =
//	new BoardDimensionsConfiguration();

//public static BoardDimensionsConfiguration BoardDimensions => s_BoardDimensionsConfiguration;
//}