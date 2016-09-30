using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConwaysGameOfLife
{
	public partial class Board : Form
	{
		public static int BoardSize = 16;
		public static Cell[,] Cells = new Cell[16, 16];
		
		public Board() {
			InitializeComponent();
			for(int x=0; x<16; x++ ) {
				for ( int y = 0; y < 16; y++ ) {
					Cells[x, y] = new Cell(x,y,(Button)tableLayoutPanel1.GetControlFromPosition(x, y));
				}
			}
			foreach (Cell c in Cells[7, 7].GetNeighbors() ) {
				c.Fill();
			}
			foreach(Cell c in Cells ) {

				c.ShowNeighbors();
			}
		}
		
		

		private void button1_Click(object sender, EventArgs e) {
			Button clicked = (Button)sender;
			TableLayoutPanelCellPosition pos = tableLayoutPanel1.GetPositionFromControl(clicked);
			Cell clickedcell = Cells[pos.Column, pos.Row];
			clickedcell.Toggle();
		//	clicked.BackColor = clicked.BackColor == Color.FromArgb(64,64,64) ? Color.Yellow : Color.FromArgb(64,64,64);
			
		}

		private void Board_Load(object sender, EventArgs e) {

		}

		private void runoncebutton_Click(object sender, EventArgs e) {
			ProcessCells();
		}

		public static void TickCells() {
			foreach (Cell c in Cells ) {
				c.Tick();
			}
			foreach ( Cell c in Cells ) {
				c.ShowNeighbors();
			}
		}
		

		private void Board_KeyDown(object sender, KeyEventArgs e) {
			MessageBox.Show(e.KeyCode.ToString());
			if ( e.KeyCode == Keys.Right ) {
				runoncebutton_Click(sender, e);
				return;
			}
		}


		protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
			if ( keyData == Keys.Right ) {
				ProcessCells();
				return true;
			}
			if(keyData == Keys.R ) {
				Random rnd = new Random();
				int xx = rnd.Next(16);
				int yy = rnd.Next(16);
				Cells[xx, yy].Toggle();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		public void ProcessCells() {
			foreach ( Cell c in Cells ) {
				Cell[] neighbors = c.GetNeighbors(true);
				if ( c.Status.Equals(Cell.State.Alive) ) {
					if ( neighbors.Length < 2 || neighbors.Length > 3 )
						c.NextTick = Cell.State.Dead;
					if ( neighbors.Length == 2 || neighbors.Length == 3 )
						c.NextTick = Cell.State.Alive;
				}
				else if ( c.Status.Equals(Cell.State.Dead) ) {
					if ( neighbors.Length == 3 )
						c.NextTick = Cell.State.Alive;
				}
			}
			TickCells();
		}

		private void clearbutton_Click(object sender, EventArgs e) {
			foreach (Cell c in Cells ) {
				if ( c.Status.Equals(Cell.State.Alive) ) {
					c.Toggle();
					c.NextTick = Cell.State.Dead;
				}
			}
		}
	}
}
