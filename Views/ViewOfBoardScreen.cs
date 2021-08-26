using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using C21_Ex02_Matan_304826811.Views;
using C21_Ex02_Matan_304826811.Toolkit;
using Ex02.ConsoleUtils;

namespace C21_Ex02_Matan_304826811.Views
{
	using C21_Ex02_Matan_304826811.UserInterface;

	// The initial screen is the Console
	public class ViewOfBoardScreen : View
	{
		public ViewOfBoardScreen(UserInterfaceAdmin i_MyUserInterfaceAdmin)
			: base(i_MyUserInterfaceAdmin)
		{
		}

		public void DrawBoard()
		{
			ScreenCreator.PrintGameBoard();
		}
	}
}