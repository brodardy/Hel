/*
 * Author : Yannick R. Brodard
 * File name : HCell.cs
 * Version : 0.1.201504240835
 * Description : Cell class, for the map
 */

#region USING STATEMENTS
using HelProject.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace HelProject.GameWorld
{
    /// <summary>
    /// Cell of a map
    /// </summary>
    public class HCell : HObject
    {
        #region CONSTRUCTORS
        /// <summary>
        /// Cell that represents a part of the map
        /// </summary>
        public HCell() : this(DEFAULT_IS_WALKABLE_VALUE, new FPosition(DEFAULT_POSITION_X_VALUE, DEFAULT_POSITION_Y_VALUE)) { /* no code... */ }

        /// <summary>
        /// Cell that represents a part of the map
        /// </summary>
        /// <param name="position">The position of the cell</param>
        /// <remarks>
        /// The cell position is rounded to the base digit.
        /// </remarks>
        public HCell(FPosition position) : this(DEFAULT_IS_WALKABLE_VALUE, position) { /* no code... */ }

        /// <summary>
        /// Cell that represents a part of the map
        /// </summary>
        /// <param name="isWalkable">The cell can be 'walked' on by entities</param>
        /// <param name="position">The position of the cell</param>
        /// <remarks>
        /// The cell position is rounded to the base digit.
        /// </remarks>
        public HCell(bool isWalkable, FPosition position)
        {
            this.IsWalkable = isWalkable;
            this.Position = new FPosition((int)position.X, (int)position.Y); // casted to only have round numbers for cells
        }
        #endregion
    }
}
