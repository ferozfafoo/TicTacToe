using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Private Members 

		private MarkType[] mResults;

		/// <summary>
		/// True if it is player 1's turn X player 2's turn 0
		/// </summary>
		private bool mPlayer1Turn;

		/// <summary>
		/// The game has ended 
		/// </summary>
		private bool mGameEnded;

		#endregion
		#region Constructor
		/// <summary>
		/// Default Constructor 
		/// </summary>

		public MainWindow()
		{
			InitializeComponent();

			NewGame();
		}

		#endregion


		/// <summary>
		/// Starts a new game and clears all values back to the start 
		/// </summary>
		private void NewGame()
		{
			// Create a new blank array of free cells.
			mResults = new MarkType[9];

			for (var i = 0; i < mResults.Length; i++)
				mResults[i] = MarkType.Free;

			// Make sure player 1 starts the game.
			mPlayer1Turn = true;

			// Iterate every button on the grid.
			//Linq below, inumarabe => fetching button.
			Container.Children.Cast<Button>().ToList().ForEach(button =>
			{
				button.Content = string.Empty;
				button.Background = Brushes.White;
				button.Foreground = Brushes.Blue;
			});

			mGameEnded = false;
		}

		/// <summary>
		/// Handles a button click event 
		/// </summary>
		/// <param name="sender"> The button was clicked </param>
		/// <param name="e">The events of the click </param>
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			// Start a new game on the click after it finished 
			if (mGameEnded)
			{
				NewGame();
				return;
			}
			
			// Casted button to a Button.
			var button = (Button)sender;

			// Find the buttons position in the array
			var column = Grid.GetColumn(button);
			var row = Grid.GetRow(button);


			var index = column + (row * 3);

			// Don't do anything if the cell already has a value in it
			if (mResults[index] != MarkType.Free)
				return;

			// Set the cell value based on which players turn it is
			// Terneray operator 
			mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

			// Set button text to the result 
			button.Content = mPlayer1Turn ? "X" : "0";

			// Change noughts to green.
			if (!mPlayer1Turn)
				button.Foreground = Brushes.Red;

			// Toggle the players turns/ duckface/ bit wise operator 
			mPlayer1Turn ^= true;

			//Check for winner
			CheckForWinner();

		}
		#region Horizontal wins
		/// <summary>
		/// Checks if there is a winner of a 3 line striaght Horizontally. 
		/// </summary> Row 1
		private void CheckForWinner()
		{
			// Check for horizontal wins
			if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
			{
				// Game ends
				mGameEnded = true;

				//Highlight winning cells in green 
				Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
			}


			// Row 2
			if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
			{
				// Game ends
				mGameEnded = true;

				//Highlight winning cells in green 
				Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
			}


			//Row 3.
			if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
			{
				// Game ends
				mGameEnded = true;

				//Highlight winning cells in green 
				Button0_2.Background = Button1_2.Background = Button2_1.Background = Brushes.Green;
			}
			#endregion
			#region Vertical Wins 
			//Check for Vertical wins
			// col 0
			//
			if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
			{
				// Game ends
				mGameEnded = true;

				//Highlight winning cells in green 
				Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
			}

			//Check for Vertical wins
			// col 1
			//
			if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
			{
				// Game ends
				mGameEnded = true;

				//Highlight winning cells in green 
				Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
			}

			//Check for Vertical wins
			// col 2
			//
			if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
			{
				// Game ends
				mGameEnded = true;

				//Highlight winning cells in green 
				Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
			}
			#endregion
			#region Diagonal Wins

			//Check for Diagonal wins

			//Check for Vertical wins
			// Top left- Bottom Right
			//
			if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
			{
				// Game ends
				mGameEnded = true;

				//Highlight winning cells in green 
				Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
			}
			//Check for Vertical wins
			// Top Right- Bottom Left
			//

			if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
			{
				// Game ends
				mGameEnded = true;

				//Highlight winning cells in green 
				Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
			}
			#endregion
			#region No Winners

			// Linq expression 
			if (!mResults.Any(f => f == MarkType.Free))
			{
				// Game ended
				mGameEnded = true;

				//Turn all cells orange

				Container.Children.Cast<Button>().ToList().ForEach(button =>
				{
					button.Background = Brushes.Orange;
				});
			}
			#endregion
		}
	}
}
