/*
 * Author : Yannick R. Brodard
 * File name : HHero.cs
 * Version : 0.1.201505110841
 * Description : Hero class, controllable entity by the player
 */

using HelProject.Features;
using HelProject.GameWorld.Map;
using HelProject.GameWorld.Spells;
using HelProject.Tools;
using HelProject.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.GameWorld.Entities
{
    /// <summary>
    /// Controllable entity
    /// </summary>
    public class HHero : HEntity
    {
        private HSpell _spellSlot1;
        private HSpell _spellSlot2;
        private HSpell _spellSlot3;
        private HSpell _spellSlot4;

        /// <summary>
        /// Spell present in slot 1
        /// </summary>
        public HSpell SpellSlot1
        {
            get { return _spellSlot1; }
            set { _spellSlot1 = value; }
        }

        /// <summary>
        /// Spell present in slot 2
        /// </summary>
        public HSpell SpellSlot2
        {
            get { return _spellSlot2; }
            set { _spellSlot2 = value; }
        }

        /// <summary>
        /// Spell present in slot 3
        /// </summary>
        public HSpell SpellSlot3
        {
            get { return _spellSlot3; }
            set { _spellSlot3 = value; }
        }

        /// <summary>
        /// Spell present in slot 4
        /// </summary>
        public HSpell SpellSlot4
        {
            get { return _spellSlot4; }
            set { _spellSlot4 = value; }
        }

        /// <summary>
        /// Creates a controlable entity
        /// </summary>
        /// <param name="initialFeatures">Initial features of the entity</param>
        /// <param name="position">Position of the enitity</param>
        public HHero(FeatureCollection initialFeatures, Vector2 position, float width, float height, Texture2D texture) : base(initialFeatures, position, width, height, texture) { /* no code... */ }

        /// <summary>
        /// Loads the content of the entity
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
        }

        /// <summary>
        /// Unloads the content of the entity
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Updates the entity
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 newPosition = this.Position; // gets the current position
            MouseState ms = InputManager.Instance.MsState; // gets the current state of the mouse

            // is the right button of the mouse clicked ?
            if (ms.LeftButton == ButtonState.Pressed)
            {
                // Update hero state to running
                this.State = EntityState.Running;

                Vector2 mouseVector = ms.Position.ToVector2(); // Gets mouse position
                Vector2 direction = mouseVector - ScreenManager.Instance.GetCorrectScreenPosition(this.Position, PlayScreen.Instance.Camera.Position, 32); // Gets the direction of the mouse from the player
                direction.Normalize(); // Normalize the direction vector

                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds; // gets the elapsed time in seconds from the last update
                newPosition += direction * elapsedTime * (this.FeatureCalculator.GetTotalMovementSpeed()); // Calculates the new position
                FRectangle newBounds = new FRectangle(this.Bounds.Width, this.Bounds.Height); // ready the new bounds of the character
                newBounds.SetBounds(newPosition, this.Texture.Width, this.Texture.Height);

                this.ApplyFluidMovement(direction, newPosition, newBounds, elapsedTime);

                this.Direction = direction; // Update the direction the hero is facing
            }
            else
            {
                this.State = EntityState.Idle;
            }
        }

        /// <summary>
        /// Draws the entity on screen
        /// </summary>
        /// <param name="spriteBatch">Sprite batch used for the drawing</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
