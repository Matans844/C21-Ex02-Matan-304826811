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

	public abstract class View
	{
		public UserInterfaceAdmin GameUserInterfaceAdmin { get; }

		protected View(UserInterfaceAdmin i_MyUserInterfaceAdmin)
		{
			this.GameUserInterfaceAdmin = i_MyUserInterfaceAdmin;
		}

	}
}
