using C21_Ex02_Matan_304826811.GameLogic;

namespace C21_Ex02_Matan_304826811.Players
{
	public abstract class Player
	{
		private static int s_InstanceCounter = 1;

		public static Board BoardOfPlayer { get; set; }

		public string PlayerID { get; }

		public int PointsEarned { get; set; } = Game.k_ZeroPoints;

		public eBoardCellType DiscType { get; }

		public eTurnState TurnState { get; set; }

		protected Player(Board i_BoardOfPlayer, eBoardCellType i_DiscType, eTurnState i_TurnState)
		{
			this.DiscType = i_DiscType;
			BoardOfPlayer = i_BoardOfPlayer;
			this.TurnState = i_TurnState;
			this.PlayerID = string.Format("John {0} of Streadonlitica", s_InstanceCounter.ToString());
			s_InstanceCounter++;
		}

		public abstract BoardCell MakeMove(int i_ChosenColumnAfterIndexAdjustment);
	}

	public enum ePlayerType
	{
		NotInit = 0,
		Human = 1,
		Computer = 2
	}

	public enum eTurnState
	{
		NotInit = 0,
		YourTurn = 1,
		NotYourTurn = 2
	}
}