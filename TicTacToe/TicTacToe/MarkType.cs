using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The type of value cell in the game is current at 
/// </summary>
namespace TicTacToe
{
	public enum MarkType
	{
		/// <summary>
		/// The cell hasnt been clicked yet 
		/// </summary>
		Free,

		/// <summary>
		/// The cell is O
		/// </summary>
		Nought,

		/// <summary>
		/// The cell is X
		/// </summary>
		Cross

	}
}
