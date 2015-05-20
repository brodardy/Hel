/*
 * Author : Yannick R. Brodard
 * File name : HHostile.cs
 * Version : 0.1.201505182014
 * Description : Base class for hostile entities
 */

using HelProject.Features;
using HelProject.GameWorld.Map;
using HelProject.Tools;
using HelProject.UI;
using HelProject.UI.HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.GameWorld.Entities
{
    public class HHostile : HEntity
    {
        private const float ALERT_FOV_MUTLIPLIER = 1.85f;

        private FRectangle _fieldOfView;
        private FRectangle _alertedFieldOfView;
        private bool _isAlerted;
        private FillingBar _healthBar;

        /// <summary>
        /// Health bar of the hostile
        /// </summary>
        public FillingBar HealthBar
        {
            get { return _healthBar; }
            set { _healthBar = value; }
        }

        /// <summary>
        /// The unit is alerted
        /// </summary>
        public bool IsAlerted
        {
            get { return _isAlerted; }
            set { _isAlerted = value; }
        }

        /// <summary>
        /// Field of view of the hostile
        /// </summary>
        public FRectangle FieldOfView
        {
            get { return _fieldOfView; }
            set { _fieldOfView = value; }
        }

        /// <summary>
        /// Field of view of the hositle when this one is alerted
        /// </summary>
        public FRectangle AlertedFieldOfView
        {
            get { return _alertedFieldOfView; }
            set { _alertedFieldOfView = value; }
        }

        /// <summary>
        /// Creates a hostile creature
        /// </summary>
        /// <param name="initialFeatures">The initial features</param>
        /// <param name="position">Position</param>
        /// <param name="width">Width (in-game unit)</param>
        /// <param name="height">Height (in-game unit)</param>
        /// <param name="textureName">Name of the texture</param>
        public HHostile(FeatureCollection initialFeatures, Vector2 position, float width, float height, string textureName, float fieldOfView = 8.125f)
            : base(initialFeatures, position, width, height, textureName)
        {
            this.IsAlerted = false;
            this.FieldOfView = new FRectangle(fieldOfView, fieldOfView);
            this.AlertedFieldOfView = new FRectangle(fieldOfView * ALERT_FOV_MUTLIPLIER, fieldOfView * ALERT_FOV_MUTLIPLIER);
            this.HealthBar = new FillingBar(FillingBar.FillingDirection.LeftToRight, new FRectangle(30, 5), Color.Black, Color.Red, Color.Black,
                this.FeatureCalculator.GetTotalLifePoints(), this.ActualFeatures.LifePoints);
        }

        /// <summary>
        /// Loads the content of the hostile
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            this.IsAlerted = false;
        }

        /// <summary>
        /// Unloads the content of the hostile
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
            this.IsAlerted = false;
        }

        /// <summary>
        /// Updates the mechanismes of the hostile
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.CenterFieldOfView();

            if (this.FieldOfView.Intersects(PlayScreen.Instance.PlayableCharacter.Bounds) ||
                (this.IsAlerted && this.AlertedFieldOfView.Intersects(PlayScreen.Instance.PlayableCharacter.Bounds)))
            {
                this.State = EntityState.Running;
                this.IsAlerted = true;
                Vector2 newPosition = this.Position;
                Vector2 heroPosition = new Vector2(PlayScreen.Instance.PlayableCharacter.Position.X, PlayScreen.Instance.PlayableCharacter.Position.Y);
                Vector2 direction = heroPosition - newPosition; // new position is still actual position
                direction.Normalize();

                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                newPosition += direction * elapsedTime * this.FeatureCalculator.GetTotalMovementSpeed();
                FRectangle newBounds = new FRectangle(this.Bounds.Width, this.Bounds.Height); // ready the new bounds of the character
                newBounds.SetBounds(newPosition, this.Texture.Width, this.Texture.Height);

                this.ApplyFluidMovement(direction, newPosition, newBounds, elapsedTime);
                this.Direction = direction;
            }
            else
            {
                this.State = EntityState.Idle;
                this.IsAlerted = false;
            }

            this.HealthBar.ActualValue = this.ActualFeatures.LifePoints;
            Vector2 hbPos = new Vector2((this.Position.X - this.HealthBar.Container.Width / 2 / HCell.TILE_SIZE) + 1f / HCell.TILE_SIZE, this.Position.Y - this.Texture.Height / HCell.TILE_SIZE);
            this.HealthBar.Container.Position = ScreenManager.Instance.GetCorrectScreenPosition(hbPos, PlayScreen.Instance.Camera.Position);
        }

        /// <summary>
        /// Draws the hostile
        /// </summary>
        /// <param name="spriteBatch">Sprite batch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            this.HealthBar.Draw(spriteBatch);
        }

        /// <summary>
        /// Centers the field of view to the position
        /// </summary>
        private void CenterFieldOfView()
        {
            float x = 0, y = 0;
            x = this.Position.X - this.FieldOfView.Width / 2f;
            y = this.Position.Y - this.FieldOfView.Height / 2f;
            this.FieldOfView.Position = new Vector2(x, y);
            x = this.Position.X - this.AlertedFieldOfView.Width / 2f;
            y = this.Position.Y - this.AlertedFieldOfView.Height / 2f;
            this.AlertedFieldOfView.Position = new Vector2(x, y);
        }
    }
}
