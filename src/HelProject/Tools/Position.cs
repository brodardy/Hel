/*
 * Author : Yannick R. Brodard
 * File name : Position.cs
 * Version : 0.1.201504221450
 * Description : Class used to determine a position in int.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.Tools
{
    public class Position
    {
        private const int DEFAULT_X_VALUE = 0;
        private const int DEFAULT_Y_VALUE = 0;

        #region ATTRIBUTES
        private int _x;
        private int _y;
        #endregion

        #region PROPRIETIES
        /// <summary>
        /// X position (Horizontal)
        /// </summary>
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        /// <summary>
        /// Y position (Vertical)
        /// </summary>
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Creates a position with the x and y coordinates (Integer)
        /// </summary>
        public Position() : this(DEFAULT_X_VALUE, DEFAULT_Y_VALUE) { /* no code... */ }

        /// <summary>
        /// Creates a position with the x and y coordinates (Integer)
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        #endregion

        #region FUNCTIONS
        /// <summary>
        /// Returns the position in form of text
        /// </summary>
        /// <returns>String of the position</returns>
        public override string ToString()
        {
            StringBuilder outputSB = new StringBuilder();
            outputSB.Append(this.X);
            outputSB.Append(", ");
            outputSB.Append(this.Y);
            return outputSB.ToString();
        }

        /// <summary>
        /// Converts the integer position to a float position
        /// </summary>
        /// <returns>Float position</returns>
        public FPosition ToFloatPosition()
        {
            return new FPosition(this.X, this.Y);
        }
        #endregion
    }
}
