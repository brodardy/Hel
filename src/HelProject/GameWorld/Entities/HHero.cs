/*
 * Author : Yannick R. Brodard
 * File name : HHero.cs
 * Version : 0.1.201505110841
 * Description : Hero class, controllable entity by the player
 */

using HelProject.Features;
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
        private Image _texture;

        /// <summary>
        /// Sprite of the hero
        /// </summary>
        public Image Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

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
        public HHero(FeatureCollection initialFeatures, Vector2 position) : base(initialFeatures, position) { /* no code... */ }

        /// <summary>
        /// Loads the content of the entity
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            this.Texture.LoadContent();
            this.Texture.Position = this.Position;
        }

        /// <summary>
        /// Unloads the content of the entity
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
            this.Texture.UnloadContent();
        }

        /// <summary>
        /// Updates the entity
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.Texture.Position = this.Position;

            MouseState ms = InputManager.Instance.MsState;
            if (ms.RightButton == ButtonState.Pressed)
            {
                Vector2 mouseVector = ms.Position.ToVector2();
                Vector2 direction = mouseVector - ScreenManager.Instance.GetCorrectScreenPosition(this.Position, 32);
                direction.Normalize();

                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayScreen.Instance.Map.FocusPosition += this.Direction * elapsedTime * (this.FeatureCalculator.GetTotalMovementSpeed());
                this.Direction = direction;
            }

        }

        /// <summary>
        /// Draws the entity on screen
        /// </summary>
        /// <param name="spriteBatch">Sprite batch used for the drawing</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(this.Texture.Texture, ScreenManager.Instance.GetCorrectScreenPosition(this.Position, 32),
                this.Texture.SourceRect, Color.White, 0.0f, this.Texture.Position, 0.25f, SpriteEffects.None, 0f);
        }
    }
}
