using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;

namespace C21_Ex02_Matan_304826811.UserInterface
{
	// This object works with the constraints object.
	public class DisplayLogic
	{
		public UserInterfaceAdmin GameUserInterfaceAdmin { get; }

		// Had to use access modifier public because:
		// 1. We are trying to set values in a struct with more than 1 value.
		// 2. The property set method accepts at most 1 argument.
		public GameBoardDimensions m_BoardDimensions;

		public eGameMode GameMode { get; set; }

		public DisplayLogic(UserInterfaceAdmin i_MyUserInterfaceAdmin)
		{
			this.GameUserInterfaceAdmin = i_MyUserInterfaceAdmin;
		}
	}

	public struct GameBoardDimensions
	{
		private int m_Height;
		private int m_Width;

		public int Height
		{
			get => this.m_Height;
			set
			{
				if (Constraints.BoardDimensions.HeightLowerLimit <= value
					&& value <= Constraints.BoardDimensions.HeightUpperLimit)
				{
					this.m_Height = value;
				}
			}
		}

		public int Width
		{
			get => this.m_Width;
			set
			{
				if (Constraints.BoardDimensions.WidthLowerLimit <= value
					&& value <= Constraints.BoardDimensions.WidthUpperLimit)
				{
					this.m_Width = value;
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
}