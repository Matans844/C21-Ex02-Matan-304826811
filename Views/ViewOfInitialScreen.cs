namespace C21_Ex02_Matan_304826811.Views
{
	using C21_Ex02_Matan_304826811.UserInterface;

	// The initial screen is the Console
	public class ViewOfInitialScreen : View
	{
		public ViewOfInitialScreen(UserInterfaceAdmin i_MyUserInterfaceAdmin)
			: base(i_MyUserInterfaceAdmin)
		{
		}

		// Unlike other views, this view is not under the responsibility of the ScreenCreator.
		// This is because this is the first screen, and it contains only taking inputs.
		public void GetInitialInputsFromUser()
		{
			this.GameUserInterfaceAdmin.MyInputOutputHandler.GetAndSetValidDimensionsFromUser();
			this.GameUserInterfaceAdmin.MyInputOutputHandler.GetAndSetValidGameModeFromUser();
		}
	}
}
