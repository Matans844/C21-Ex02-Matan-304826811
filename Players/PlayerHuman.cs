using C21_Ex02_Matan_304826811.GameLogic;

namespace C21_Ex02_Matan_304826811.Players
{
	public class PlayerHuman : Player
	{
		private const ePlayerType k_PlayerType = ePlayerType.Human;

		public ePlayerType PlayerType => k_PlayerType;

		public PlayerHuman(Board i_BoardOfPlayer, eBoardCellType i_DiscType, eTurnState i_TurnState)
			: base(i_BoardOfPlayer, i_DiscType, i_TurnState)
		{
		}

		public override eBoardState MakeMove(int i_ChosenColumn)
		{
			return this.BoardOfPlayer.SlideDisk(i_ChosenColumn, this.DiscType);
		}
	}
}
