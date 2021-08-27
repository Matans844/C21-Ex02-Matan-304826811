using C21_Ex02_Matan_304826811.UserInterface;

namespace C21_Ex02_Matan_304826811.Views
{
	public abstract class View
	{
		public UserInterfaceAdmin GameUserInterfaceAdmin { get; }

		protected View(UserInterfaceAdmin i_MyUserInterfaceAdmin)
		{
			this.GameUserInterfaceAdmin = i_MyUserInterfaceAdmin;
		}
	}
}