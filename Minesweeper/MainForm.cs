using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MainForm : Form
    {
        Cell[,] grid;

        public MainForm()
        {
            InitializeComponent();

            grid = new Cell[9,9];
            Random rand = new Random();

            List<Point> potentialMineLocations = new List<Point>(grid.Length);

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    potentialMineLocations.Add(new Point(i, j));
                }
            }

            for (int i = 0; i < 10; i++)
            {
                Point x = potentialMineLocations[rand.Next(potentialMineLocations.Count)];
                grid[x.X, x.Y] = new MineCell();
            }

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] is MineCell)
                        continue;

                    grid[i, j] = new SafeCell();
                    ((SafeCell)grid[i, j]).CellValue = GetNeighbouringCellCount(i, j);
                }
            }


        }

        private int GetNeighbouringCellCount(int x, int y)
        {
            int count = 0;

            if (x != grid.GetLowerBound(0))
            {
                if (y != grid.GetLowerBound(1))
                {
                    if (grid[x - 1, y - 1] is MineCell)
                        count++;
                }
                if (y != grid.GetUpperBound(1))
                {
                    if (grid[x - 1, y + 1] is MineCell)
                        count++;
                }

                if (grid[x - 1, y] is MineCell)
                    count++;
            }
            if (x != grid.GetUpperBound(0))
            {
                if (y != grid.GetLowerBound(1))
                {
                    if (grid[x + 1, y - 1] is MineCell)
                        count++;
                }
                if (y != grid.GetUpperBound(1))
                {
                    if (grid[x + 1, y + 1] is MineCell)
                        count++;
                }

                if (grid[x + 1, y]is MineCell)
                    count++;
            }

            if (y != grid.GetLowerBound(1))
            {
                if (grid[x, y - 1] is MineCell)
                    count++;
            }
            if (y != grid.GetUpperBound(1))
            {
                if (grid[x, y + 1] is MineCell)
                    count++;
            }

            return count;
        }

        private void drawingBoard_Paint(object sender, PaintEventArgs e)
        {
            int cellWidth = ((Control)sender).Size.Width / grid.GetLength(0);
            int cellHeight = ((Control)sender).Size.Height / grid.GetLength(1);

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    RectangleF cellRectangle = new RectangleF(i * cellWidth, j * cellHeight, cellWidth, cellHeight);
                    if (grid[i, j].Exposed)
                    {
                        if (grid[i, j] is SafeCell)
                        {
                            using (Font font = new Font("Microsoft Sans Serif", (float)(cellHeight * 0.75 * 72 / e.Graphics.DpiY)))
                            {
                                using (StringFormat stringFormat = new StringFormat())
                                {
                                    stringFormat.LineAlignment = StringAlignment.Center;
                                    stringFormat.Alignment = StringAlignment.Center;

                                    e.Graphics.DrawString(((SafeCell)grid[i, j]).CellValue.ToString(), font, Brushes.Black, cellRectangle, stringFormat);
                                }
                            }
                        }
                        else
                        {
                            e.Graphics.FillRectangle(Brushes.Red, cellRectangle);
                        }
                    }
                    else if (grid[i, j].Marked)
                    {
                        e.Graphics.FillRectangle(Brushes.Orange, cellRectangle);
                    }
                }
            }
        }

        private void drawingBoard_Click(object sender, EventArgs e)
        {
            int cellWidth = ((Control)sender).Size.Width / grid.GetLength(0);
            int cellHeight = ((Control)sender).Size.Height / grid.GetLength(1);

            
        }

        private void drawingBoard_MouseClick(object sender, MouseEventArgs e)
        {
            int cellWidth = ((Control)sender).Size.Width / grid.GetLength(0);
            int cellHeight = ((Control)sender).Size.Height / grid.GetLength(1);

            Cell clickedCell = grid[e.X / cellWidth, e.Y / cellHeight];
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                ExposeBlock(e.X / cellWidth, e.Y / cellHeight);
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                clickedCell.Marked = !clickedCell.Marked;

            drawingBoard.Invalidate();
        }

        private void ExposeBlock(int x, int y)
        {
            if (x < grid.GetLowerBound(0) || y < grid.GetLowerBound(1) || x > grid.GetUpperBound(0) || y > grid.GetUpperBound(1) || grid[x, y].Exposed)
                return;

            grid[x, y].Exposed = true;

            if (grid[x, y] is SafeCell && ((SafeCell)grid[x, y]).CellValue == 0)
            {
                ExposeBlock(x - 1, y - 1);
                ExposeBlock(x - 1, y);
                ExposeBlock(x - 1, y + 1);

                ExposeBlock(x, y - 1);
                //ExposeBlock(x, y);
                ExposeBlock(x, y + 1);

                ExposeBlock(x + 1, y - 1);
                ExposeBlock(x + 1, y);
                ExposeBlock(x + 1, y + 1);
            }
        }
    }
}
