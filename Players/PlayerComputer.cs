using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using Ex02.ConsoleUtils;


namespace C21_Ex02_Matan_304826811.Players
{
	public class PlayerComputer : Player
	{
		private const ePlayerType r_PlayerType = ePlayerType.Computer;

		public ePlayerType PlayerType => r_PlayerType;

		public PlayerComputer(ePlayerType i_Type)
			: base(i_Type)
		{
		}

		public BoardCell PickBoardCell(Board i_Board)
		{
			throw new System.NotImplementedException();
		}

		public override BoardCell PickBoardColumnForDisc(Board i_Board, int i_Column)
		{
			throw new System.NotImplementedException();
		}
	}
}
