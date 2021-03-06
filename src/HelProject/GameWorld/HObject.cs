﻿/*
 * Author : Yannick R. Brodard
 * File name : HEntity.cs
 * Version : 0.2.201504240836
 * Description : Base abstact class for 'things' in the game world
 */

#region USING STATEMENTS
using HelProject.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace HelProject.GameWorld
{
    /// <summary>
    /// Abstract class for all entities of the game
    /// </summary>
    public abstract class HObject
    {
        protected const bool DEFAULT_IS_WALKABLE_VALUE = false;
        protected const float DEFAULT_POSITION_X_VALUE = 0.0f;
        protected const float DEFAULT_POSITION_Y_VALUE = 0.0f;

        #region ATTRIBUTES
        private Vector2 _position;
        private bool _isWalkable;             // true if the object can be walked on by enitites
        #endregion

        #region PROPRIETIES
        /// <summary>
        /// Position of the entity
        /// </summary>
        public Vector2 Position
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
        /// <summary>
        /// Creates an object
        /// </summary>
        public HObject() : this(DEFAULT_IS_WALKABLE_VALUE, new Vector2(DEFAULT_POSITION_X_VALUE, DEFAULT_POSITION_Y_VALUE)) { /* no code... */ }

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="position">Position of the object</param>
        public HObject(Vector2 position) : this(DEFAULT_IS_WALKABLE_VALUE, position) { /* no code... */ }

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="isWalkable">Can the object be 'walked' on by entities</param>
        /// <param name="position">Position of the object</param>
        public HObject(bool isWalkable, Vector2 position)
        {
            this.IsWalkable = isWalkable;
            this.Position = position;
        }

        /// <summary>
        /// Override this to load content
        /// </summary>
        public virtual void LoadContent() { /* no code... */ }

        /// <summary>
        /// Override this to unload content
        /// </summary>
        public virtual void UnloadContent() { /* no code... */ }

        /// <summary>
        /// Override this to update object
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime) { /* no code... */ }

        /// <summary>
        /// Override this to draw object
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch) { /* no code... */ }
        #endregion
    }
}
