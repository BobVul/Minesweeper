using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    class Cell
    {
        internal bool marked;
        internal bool exposed;

        /// <summary>
        /// Whether the cell has been marked as a mine by the player.
        /// </summary>
        /// <value>
        /// Gets or sets the value of the bool field, marked.
        /// </value>
        public bool Marked
        {
            get
            {
                return marked;
            }
            set
            {
                marked = value;
            }
        }

        /// <summary>
        /// Whether the cell has been exposed by the player yet.
        /// </summary>
        /// <value>
        /// Gets or sets the value of the bool field, exposed.
        /// </value>
        public bool Exposed
        {
            get
            {
                return exposed;
            }
            set
            {
                exposed = value;
            }
        }
    }
}
