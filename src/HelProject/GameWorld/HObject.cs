/*
 * Author : Yannick R. Brodard
 * File name : HEntity.cs
 * Version : 0.1.201504221304
 * Description : Base abstact class for all entities
 */

#region USING STATEMENTS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelProject.Tools;
#endregion

namespace HelProject.GameWorld
{
    /// <summary>
    /// Abstract class for all entities of the game
    /// </summary>
    public class HObject
    {
        private const bool DEFAULT_IS_WALKABLE_VALUE = false;
        private const float DEFAULT_POSITION_X_VALUE = 0.0f;
        private const float DEFAULT_POSITION_Y_VALUE = 0.0f;

        #region ATTRIBUTES
        private FPosition _position;
        private bool _isWalkable;             // true if the object can be walked on by enitites
        #endregion

        #region PROPRIETIES
        /// <summary>
        /// Position of the entity
        /// </summary>
        public FPosition Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Can be walked by entities
        /// </summary>
        public bool IsWalkable
        {
            get { return _isWalkable; }
            set { _isWalkable = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public HObject() : this(DEFAULT_IS_WALKABLE_VALUE, new FPosition(DEFAULT_POSITION_X_VALUE, DEFAULT_POSITION_Y_VALUE)) { /* no code... */ }
        public HObject(FPosition position) : this(DEFAULT_IS_WALKABLE_VALUE, position) { /* no code... */ }
        public HObject(bool isWalkable, FPosition position)
        {
            this.IsWalkable = isWalkable;
            this.Position = position;
        }
        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region ENUMS
        #endregion
    }
}
