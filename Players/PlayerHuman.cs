using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using Ex02.ConsoleUtils;


namespace C21_Ex02_Matan_304826811.Players
{
	public class PlayerHuman : Player
	{
		public PlayerHuman(ePlayerType i_PlayerType, eBoardCellType i_DiscType)
			: base(i_PlayerType, i_DiscType)
		{
		}

		public override BoardCell PickBoardColumnForDisc(Board i_Board, int i_Column)
		{
			return i_Board.SlideDisk(i_Column);
		}
	}
}
