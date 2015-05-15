/*
 * Author : Yannick R. Brodard
 * File name : HCell.cs
 * Version : 0.1.201504240835
 * Description : Cell class, for the map
 */

#region USING STATEMENTS
using HelProject.Tools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace HelProject.GameWorld.Map
{
    /// <summary>
    /// Cell of a map
    /// </summary>
    public class HCell : HObject
    {
        public const int TILE_SIZE = 32;

        private FRectangle _bounds;
        private string _type;

        /// <summary>
        /// Bounds of the cells
        /// </summary>
        public FRectangle Bounds
        {
            get { return _bounds; }
            set { _bounds = value; }
        }

        /// <summary>
        /// Type of the cell
        /// </summary>
        /// <remarks>
        /// Often corresponds with a texture
        /// </remarks>
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        #region CONSTRUCTORS
        /// <summary>
        /// Cell that represents a part of the map
        /// </summary>
        public HCell() : this(new Vector2(DEFAULT_POSITION_X_VALUE, DEFAULT_POSITION_Y_VALUE)) { /* no code... */ }

        /// <summary>
        /// Cell that represents a part of the map
        /// </summary>
        /// <param name="position">The position of the cell</param>
        /// <remarks>
        /// The cell position is rounded to the base digit.
        /// </remarks>
        public HCell(Vector2 position) : this(DEFAULT_IS_WALKABLE_VALUE, position) { /* no code... */ }

        /// <summary>
        /// Cell that represents a part of the map
        /// </summary>
        /// <param name="isWalkable">The cell can be 'walked' on by entities</param>
        /// <param name="position">The position of the cell</param>
        /// <remarks>
        /// The cell position is rounded to the base digit.
        /// </remarks>
        public HCell(bool isWalkable, Vector2 position) : this(isWalkable, position, String.Empty) { /* no code... */ }

        /// <summary>
        /// Cell that represents a part of the map
        /// </summary>
        /// <param name="isWalkable">The cell can be 'walked' on by entities</param>
        /// <param name="position">The position of the cell</param>
        /// <param name="type">Type of the cell</param>
        /// <remarks>
        /// The cell position is rounded to the base digit.
        /// The type of the often corresponds with a texture
        /// </remarks>
        public HCell(bool isWalkable, Vector2 position, string type)
        {
            this.IsWalkable = isWalkable;
            this.Position = new Vector2((int)position.X, (int)position.Y); // casted to integer, to only have round numbers for cells
            this.Bounds = new FRectangle(position.X, position.Y, 1f, 1f);

            if (type == String.Empty)
                this.Type = (isWalkable) ? "floor" : "wall";
            else
                this.Type = type;
        }
        #endregion
    }
}
