using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Players;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;

namespace C21_Ex02_Matan_304826811.UserInterface
{
	// The initial screen is the Console
	public class ViewOfInitialScreen
	{
		public UserInterfaceAdmin GameUserInterfaceAdmin { get; }

		public ViewOfInitialScreen(UserInterfaceAdmin i_MyUserInterfaceAdmin)
		{
			this.GameUserInterfaceAdmin = i_MyUserInterfaceAdmin;
		}

		public void GetInitialInputsFromUser()
		{
			this.GameUserInterfaceAdmin.MyInputOutputHandler.GetAndSetValidDimensionsFromUser();
			this.GameUserInterfaceAdmin.MyInputOutputHandler.GetAndSetValidGameModeFromUser();
		}
	}
}
