/*
 * Author : Yannick R. Brodard
 * File name : HSpell.cs
 * Version : 0.1.201505070906
 * Description : Abstract class and base of all spells
 */

#region USING STATEMENTS
using HelProject.Features;
using HelProject.GameWorld.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace HelProject.GameWorld.Spells
{
    /// <summary>
    /// Spell of the game
    /// </summary>
    public abstract class HSpell
    {
        #region ATTRIBUTES
        private HHero _hero;
        private FeatureCollection _features;
        private string _name;
        private float _timeOfEffect;
        #endregion

        #region PROPRIETIES
        /// <summary>
        /// Hero that the spell is attached to
        /// </summary>
        public HHero Hero
        {
            get { return _hero; }
            set { _hero = value; }
        }

        /// <summary>
        /// Features of the spell
        /// </summary>
        public FeatureCollection Features
        {
            get { return _features; }
            set { _features = value; }
        }

        /// <summary>
        /// Name of the spell
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Time of effect once the spell is active
        /// </summary>
        public float TimeOfEffect
        {
            get { return _timeOfEffect; }
            set { _timeOfEffect = value; }
        }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Creates a spell
        /// </summary>
        /// <param name="hero">Hero that the spell is attached to</param>
        /// <param name="features">Features of the spell</param>
        /// <param name="timeOfEffect">Time of effect once the spell is active</param>
        /// <param name="name">Name of the spell</param>
        public HSpell(HHero hero, FeatureCollection features, float timeOfEffect, string name)
        {
            this.Hero = hero;
            this.Features = features;
            this.TimeOfEffect = timeOfEffect;
            this.Name = name;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Loads the content of the spell
        /// </summary>
        public virtual void LoadContent() { /* no code... */ }

        /// <summary>
        /// Unloads the content of the spell
        /// </summary>
        public virtual void UnloadContent() { /* no code... */ }

        /// <summary>
        /// Updates the spell in the game loop
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime) { /* no code... */ }

        /// <summary>
        /// Draws the spell
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch) { /* no code... */ }
        #endregion
    }
}
