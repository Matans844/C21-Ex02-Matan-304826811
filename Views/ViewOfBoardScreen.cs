using C21_Ex02_Matan_304826811.UserInterface;

namespace C21_Ex02_Matan_304826811.Views
{
	// The initial screen is the Console
	public class ViewOfBoardScreen : View
	{
		public ViewOfBoardScreen(UserInterfaceAdmin i_MyUserInterfaceAdmin)
			: base(i_MyUserInterfaceAdmin)
		{
		}

		public void DrawBoard()
		{
			this.GameUserInterfaceAdmin.MyScreenCreator.PrintGameBoard();
		}
	}
}