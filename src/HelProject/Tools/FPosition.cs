/*
 * Author : Yannick R. Brodard
 * File name : FPosition.cs
 * Version : 0.1.201504221306
 * Description : Class used to determine a position in float.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.Tools
{
    public class FPosition
    {
        private const int DEFAULT_X_VALUE = 0;
        private const int DEFAULT_Y_VALUE = 0;

        #region ATTRIBUTES
        private float _x;
        private float _y;
        #endregion

        #region PROPRIETIES
        /// <summary>
        /// X position (Horizontal)
        /// </summary>
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        /// <summary>
        /// Y position (Vertical)
        /// </summary>
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Creates a position with the x and y coordinates (Integer)
        /// </summary>
        public FPosition() : this(DEFAULT_X_VALUE, DEFAULT_Y_VALUE) { /* no code... */ }

        /// <summary>
        /// Creates a position with the x and y coordinates (Integer)
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public FPosition(float x, float y)
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
        /// Converts the float position to an integer position
        /// </summary>
        /// <returns>Integer position</returns>
        public Position ToInt32Position()
        {
            int x = (int)Math.Round(this.X, 0);
            int y = (int)Math.Round(this.Y, 0);
            return new Position(x, y);
        }
        #endregion
    }
}
