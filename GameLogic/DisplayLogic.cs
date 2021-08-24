using System;
using System.Text;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;

namespace C21_Ex02_Matan_304826811.GameLogic
{
	public class DisplayLogic
	{
		// Had to use access modifier public because:
		// 1. We are trying to set values in a struct with more than 1 value.
		// 2. The property set method accepts at most 1 argument.
		public static GameBoardDimensions s_BoardDimensions;
		private static eGameMode s_GameMode;

		public static eGameMode GameMode
		{
			get => s_GameMode;
			set => s_GameMode = value;
		}
	}
}

public struct GameBoardDimensions
{
	private int m_Height;
	private int m_Width;

	public int Height
	{
		get => m_Height;
		set
		{
			if (Constraints.BoardDimensions.HeightLowerLimit <= value
				&& value <= Constraints.BoardDimensions.HeightUpperLimit)
			{
				m_Height = value;
			}
		}
	}

	public int Width
	{
		get => m_Width;
		set
		{
			if (Constraints.BoardDimensions.WidthLowerLimit <= value
				&& value <= Constraints.BoardDimensions.WidthUpperLimit)
			{
				m_Width = value;
			}
		}
	}

	public void SetterByChoice(eBoardDimension i_Dimension, int i_Value)
	{
		if (i_Dimension == eBoardDimension.Height)
		{
			 this.Height = i_Value;
		}
		else
		{
			this.Width = i_Value;
		}
	}

	public int GetterByChoice(eBoardDimension i_Dimension)
	{
		return (i_Dimension == eBoardDimension.Height) ? this.Height : this.Width;
	}

	public GameBoardDimensions(int i_HeightToSet, int i_WidthToSet)
	{
		this.m_Height = i_HeightToSet;
		this.m_Width = i_WidthToSet;
	}
}

public enum eBoardDimension
{
	NotInitiated = 0,
	Height = 1,
	Width = 2
}

public enum eInputsToGame
{
	NotInitiated = 0,
	Dimensions = 1,
	GameMode = 2
}