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

        #region PUBLIC METHODS
        
        #endregion

        #region PRIVATE METHODS

        #endregion

        #region ENUMS
        #endregion
    }
}
