using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    class SafeCell : Cell
    {
        internal int cellValue;

        /// <summary>
        /// The value of the cell.
        /// </summary>
        /// <remarks>
        /// This should be used to store the number of neighbouring mines.
        /// The value must not be set to a negative number.
        /// </remarks>
        /// <value>
        /// Gets or sets the value of the int field, cellValue.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when a negative value is set.
        /// </exception>
        public int CellValue
        {
            get
            {
                return cellValue;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Cell values must not be negative.");
                cellValue = value;
            }
        }
    }
}
