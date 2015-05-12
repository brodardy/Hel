/*
 * Author : Yannick R. Brodard
 * File name : PlayScreen.cs
 * Version : 0.1.201504301315
 * Description : Screen for the gameplay
 */

#region USING STATEMENTS
using HelProject.Features;
using HelProject.GameWorld;
using HelProject.GameWorld.Entities;
using HelProject.GameWorld.Map;
using HelProject.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace HelProject.UI
{
    public class PlayScreen : GameScreen
    {
        private HMap _map;
        private HHero _hero;
        private static PlayScreen _instance;
        private Camera _camera;

        public Camera Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }

        public static PlayScreen Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlayScreen();
                return _instance;
            }
        }

        private PlayScreen() { }

        /// <summary>
        /// Playable character
        /// </summary>
        public HHero Hero
        {
            get { return _hero; }
            set { _hero = value; }
        }

        /// <summary>
        /// Map of the game
        /// </summary>
        public HMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        /// <summary>
        /// Loads the content of the window
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            this.Camera = new Camera(new Vector2(10, 10), MainGame.Instance.GraphicsDevice.Viewport.Width, MainGame.Instance.GraphicsDevice.Viewport.Height);

            this.Map = new HMap(200, 200, 1f);
            this.Map.MakeCaverns();
            this.Map.LoadContent();

            FeatureCollection f = new FeatureCollection()
            {
                Strenght = HEntity.DEFAULT_STRENGHT,
                Vitality = HEntity.DEFAULT_VITALITY,
                Agility = HEntity.DEFAULT_AGILITY,
                Magic = HEntity.DEFAULT_MAGIC,
                InitialAttackSpeed = HEntity.DEFAULT_ATTACKSPEED,
                MinimumDamage = HEntity.DEFAULT_MINUMUMDAMAGE,
                MaximumDamage = HEntity.DEFAULT_MAXIMUMDAMAGE,
                InitialManaRegeneration = HEntity.DEFAULT_MANAREGENERATION,
                InitialMovementSpeed = HEntity.DEFAULT_MOVEMENTSPEED,
                InitialLifePoints = HEntity.DEFAULT_LIFEPOINTS
            };
            this.Hero = new HHero(f, new Vector2(10, 10));
            this.Hero.Texture = new Image();
            this.Hero.Texture.ImagePath = "Entities/hero_a";
            this.Hero.LoadContent();
        }

        /// <summary>
        /// Unloads the content of the window
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
            this.Map.UnloadContent();
            this.Hero.UnloadContent();
        }

        /// <summary>
        /// Updates the mechanisms
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.IsKeyboardKeyDown(Keys.Space))
            {
                this.Map.MakeRandomlyFilledMap();
                this.Map.MakeCaverns();
            }

            this.Hero.Update(gameTime);
            //this.Map.FocusPosition = this.Hero.Position;
        }

        /// <summary>
        /// Draws on the window
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Map.Draw(spriteBatch, this.Camera);
            this.Hero.Draw(spriteBatch);
        }
    }
}
