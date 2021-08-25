using C21_Ex02_Matan_304826811.UserInterface;
using C21_Ex02_Matan_304826811.Controller;
using C21_Ex02_Matan_304826811.GameLogic;
using C21_Ex02_Matan_304826811.Presets;
using Ex02.ConsoleUtils;


namespace C21_Ex02_Matan_304826811.Players
{
	public class PlayerComputer : Player
	{
		private const ePlayerType k_PlayerType = ePlayerType.Computer;

		public ePlayerType PlayerType => k_PlayerType;

		public PlayerComputer(Board i_BoardOfPlayer, eBoardCellType i_DiscType, eTurnState i_TurnState)
			: base(i_BoardOfPlayer, i_DiscType, i_TurnState)
		{
		}

		public override BoardCell MakeMove(int i_ChosenColumn)
		{
			return this.BoardOfPlayer.SlideDisk(this.chooseColumnForMove(), this.DiscType);
		}

		public override int ChooseColumnForMove()
		{
			throw new System.NotImplementedException();
		}
	}
}
