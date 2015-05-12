/*
 * Author : Yannick R. Brodard
 * File name : PlayScreen.cs
 * Version : 0.3.201505120902
 * Description : Screen for the gameplay
 */

#region USING STATEMENTS
using HelProject.Features;
using HelProject.GameWorld.Entities;
using HelProject.GameWorld.Map;
using HelProject.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace HelProject.UI
{
    /// <summary>
    /// Screen for the gameplay
    /// </summary>
    public class PlayScreen : GameScreen
    {
        private HMap _mapDifficultyEasy;
        private HMap _mapDifficultyMedium;
        private HMap _mapDifficultyHard;
        private HMap _currentMap;
        private HHero _hero;
        private static PlayScreen _instance;
        private Camera _camera;

        /// <summary>
        /// Map of the game with easy difficulty
        /// </summary>
        public HMap MapDifficultyEasy
        {
            get { return _mapDifficultyEasy; }
            set { _mapDifficultyEasy = value; }
        }

        /// <summary>
        /// Map of the game with medium difficulty
        /// </summary>
        public HMap MapDifficultyMedium
        {
            get { return _mapDifficultyMedium; }
            set { _mapDifficultyMedium = value; }
        }

        /// <summary>
        /// Map of the game with hard difficulty
        /// </summary>
        public HMap MapDifficultyHard
        {
            get { return _mapDifficultyHard; }
            set { _mapDifficultyHard = value; }
        }

        /// <summary>
        /// Current map where the hero is
        /// </summary>
        public HMap CurrentMap
        {
            get { return _currentMap; }
            set { _currentMap = value; }
        }

        /// <summary>
        /// Playable character
        /// </summary>
        public HHero PlayableCharacter
        {
            get { return _hero; }
            set { _hero = value; }
        }

        /// <summary>
        /// Instance of the play screen
        /// </summary>
        /// <remarks>
        /// This is a singleton class
        /// </remarks>
        public static PlayScreen Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlayScreen();
                return _instance;
            }
        }

        /// <summary>
        /// Camera of the play screen
        /// </summary>
        public Camera Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }


        /// <summary>
        /// Private constructor
        /// </summary>
        private PlayScreen() { /* no code... */ }

        /// <summary>
        /// Loads the content of the window
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            /* MAP INITIALISATION */
            this.MapDifficultyEasy = new HMap(800, 800, 1f);
            this.MapDifficultyEasy.MakeCaverns();
            this.MapDifficultyEasy.LoadContent();

            this.MapDifficultyMedium = new HMap(800, 800, 1f);
            this.MapDifficultyMedium.MakeCaverns();
            this.MapDifficultyMedium.LoadContent();

            this.MapDifficultyHard = new HMap(800, 800, 1f);
            this.MapDifficultyHard.MakeCaverns();
            this.MapDifficultyHard.LoadContent();

            this.CurrentMap = this.MapDifficultyEasy;

            /* PLAYABLE CHARACTER INITIALISATION */
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
            this.PlayableCharacter = new HHero(f, new Vector2(10, 10));
            this.PlayableCharacter.Texture = new Image();
            this.PlayableCharacter.Texture.ImagePath = "Entities/hero_a";
            this.PlayableCharacter.LoadContent();

            // Camera initialisation, gets the width and height of the window
            // and the position of the hero
            this.Camera = new Camera(this.PlayableCharacter.Position, MainGame.Instance.GraphicsDevice.Viewport.Width, MainGame.Instance.GraphicsDevice.Viewport.Height);
        }

        /// <summary>
        /// Unloads the content of the window
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
            this.MapDifficultyEasy.UnloadContent();
            this.PlayableCharacter.UnloadContent();
        }

        /// <summary>
        /// Updates the mechanisms
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.IsKeyboardKeyDown(Keys.F8))
            {
                this.CurrentMap.MakeRandomlyFilledMap();
                this.CurrentMap.MakeCaverns();
            }

            if (InputManager.Instance.IsKeyboardKeyDown(Keys.F9))
            {
                if (this.CurrentMap == this.MapDifficultyEasy)
                    this.CurrentMap = this.MapDifficultyMedium;
                else if (this.CurrentMap == this.MapDifficultyMedium)
                    this.CurrentMap = this.MapDifficultyHard;
                else if (this.CurrentMap == this.MapDifficultyHard)
                    this.CurrentMap = this.MapDifficultyEasy;
            }

            this.PlayableCharacter.Update(gameTime);
        }

        /// <summary>
        /// Draws on the window
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            this.CurrentMap.Draw(spriteBatch, this.Camera);
            this.PlayableCharacter.Draw(spriteBatch);
        }
    }
}
