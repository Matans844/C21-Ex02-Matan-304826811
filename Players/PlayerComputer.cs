using C21_Ex02_Matan_304826811.GameLogic;

namespace C21_Ex02_Matan_304826811.Players
{
	public class PlayerComputer : IPlayer
	{
		private const ePlayerType k_PlayerType = ePlayerType.Computer;

		public ePlayerType PlayerType => k_PlayerType;

		public PlayerComputer(Board i_BoardOfPlayer, eBoardCellType i_DiscType, eTurnState i_TurnState)
			: base(i_BoardOfPlayer, i_DiscType, i_TurnState)
		{
		}

		public override BoardCell MakeMove(int i_ChosenColumn)
		{
			return this.BoardOfPlayer.SlideDiskToBoard(this.ChooseColumnForMove(), this.DiscType);
		}

		public int ChooseColumnForMove()
		{
			// TODO: First choose int randomly, check that it works. Get input. Checks required: validity (does board support?).
			// TODO: Try and write engine. This can be done through translation from Cpp in GitHub.
			throw new System.NotImplementedException();
		}
	}
}
