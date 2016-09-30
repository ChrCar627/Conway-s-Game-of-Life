using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ConwaysGameOfLife
{
	public class Cell
	{
		public static Color livecolor = Color.Yellow;
		public static Color deadcolor = Color.FromArgb(64, 64, 64);
		
		public enum State { Alive,Dead};
		private State _status = State.Dead;
		private State _nexttick = State.Dead;
		private int _x;
		private int _y;

		public int X { get { return _x; } set { _x = value; } }
		public int Y { get { return _y; } set { _y = value; } }
		public State Status { get { return _status; } set { _status = value; } }
		public State NextTick { get { return _nexttick; } set { _nexttick = value; } }

		public Button button;
		public Cell(int x, int y, Button b) {
			X = x;
			Y = y;
			button = b;
		}

		public void Fill() {
			button.BackColor = livecolor;
			Status = State.Alive;
		}
		public void Empty() {
			button.BackColor = deadcolor;
			Status = State.Dead;
		}
		public void Tick() {
			Status = NextTick;
			if ( Status.Equals(State.Alive) )
				Fill();
			else
				Empty();
		}

		public void ShowNeighbors() {
			button.Text = GetNeighbors(true).Length.ToString();
		}

		public void Toggle() {
			if ( Status == State.Alive ) {
				Status = State.Dead;
				Empty();
			}
			else {
				Status = State.Alive;
				Fill();
			}
			foreach(Cell c in GetNeighbors() ) {
				c.ShowNeighbors();
			}
			ShowNeighbors();
		}
		public Cell[] GetNeighbors(bool live = false) {
			bool lbound = X - 1 >= 0 ? true:false;
			bool rbound = X + 1 < Board.BoardSize ? true:false;
			bool tbound = Y + 1 < Board.BoardSize ? true:false;
			bool bbound = Y - 1 >= 0 ? true:false;
			Cell tl = tbound && lbound ? Board.Cells[X - 1, Y + 1] : null;
			Cell t = tbound ? Board.Cells[X, Y + 1] : null;
			Cell tr = tbound && rbound ? Board.Cells[X + 1, Y + 1] : null;
			Cell r = rbound ? Board.Cells[X + 1, Y] : null;
			Cell br = bbound && rbound ? Board.Cells[X + 1, Y - 1] : null;
			Cell b = bbound ? Board.Cells[X, Y - 1] : null;
			Cell bl = bbound && lbound ? Board.Cells[X - 1, Y - 1] : null;
			Cell l = lbound ? Board.Cells[X - 1, Y] : null;
			List<Cell> neighbors = new List<Cell> { tl, t, tr, r, br, b, bl, l };
			neighbors.RemoveAll(cell => cell == null);
			if(live==true)
				neighbors.RemoveAll(cell => cell.Status == State.Dead);
			return neighbors.ToArray();
		}
	}
}
