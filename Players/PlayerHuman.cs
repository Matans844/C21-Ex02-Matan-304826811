using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using Ex02.ConsoleUtils;


namespace C21_Ex02_Matan_304826811.Players
{
	public class PlayerHuman : Player
	{
		private const ePlayerType r_PlayerType = ePlayerType.Human;

		public ePlayerType PlayerType => r_PlayerType;

		public PlayerHuman(ePlayerType i_PlayerType, eBoardCellType i_DiscType, eTurnState i_TurnState)
			: base(i_PlayerType, i_DiscType, i_TurnState)
		{
		}

		public override BoardCell PickBoardColumnForDisc(Board i_Board, int i_Column)
		{
			return i_Board.SlideDisk(i_Column);
		}
	}
}
