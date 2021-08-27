using C21_Ex02_Matan_304826811.GameLogic;

namespace C21_Ex02_Matan_304826811.Players
{
	public class PlayerHuman : IPlayer
	{
		private const ePlayerType k_PlayerType = ePlayerType.Human;

		public ePlayerType PlayerType => k_PlayerType;

		public PlayerHuman(Board i_BoardOfPlayer, eBoardCellType i_DiscType, eTurnState i_TurnState)
			: base(i_BoardOfPlayer, i_DiscType, i_TurnState)
		{
		}

		public override BoardCell MakeMove(int i_ChosenBoardColumnAjustedForMatrix)
		{
			BoardCell myLastMove = this.BoardOfPlayer.SlideDiskToBoard(i_ChosenBoardColumnAjustedForMatrix, this.DiscType);
			this.TurnState = eTurnState.NotYourTurn;

			return myLastMove;
		}
	}
}
