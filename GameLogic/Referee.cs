using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C21_Ex02_Matan_304826811.GameLogic
{
	public class Referee
	{
		public Board BoardToReferee { get; }

		public Referee(Board i_Board)
		{
			this.BoardToReferee = i_Board;
		}

		public bool IsGameDrawn()
		{
			return isBoardExhausted() && !hasGameWinnerForBoard();
		}

		public bool IsGameFinished()
		{
			return hasGameWinnerForBoard() || isBoardExhausted();
		}

		private bool isBoardExhausted()
		{
			// This was a foreach loop that was upgraded to a LINQ statement
			return this.BoardToReferee.NumOfCellVacanciesInColumn.All(i_ColumnVacancy => i_ColumnVacancy == 0);
		}

		private bool hasGameWinnerForBoard()
		{

		}
	}
}
