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
using System;
using System.Collections.Generic;
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
        private HMap _mapTown;
        private HMap _currentMap;
        private HHero _hero;
        private static PlayScreen _instance;
        private Camera _camera;

        /// <summary>
        /// Starting point of the game : The town
        /// </summary>
        public HMap MapTown
        {
            get { return _mapTown; }
            set { _mapTown = value; }
        }

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

            this.LoadMaps();
            this.LoadPlayableCharacter();
            this.LoadHostiles();

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

            this.UnloadMaps();
            this.PlayableCharacter.UnloadContent();
        }

        /// <summary>
        /// Updates the mechanisms
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            this.UpdateMapControl();
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
            this.DrawHostiles(spriteBatch);
        }

        /// <summary>
        /// Map initialization
        /// </summary>
        private void LoadMaps()
        {
            this.MapTown = new HMap(HMap.LoadFromXml("Load/MapTown.xml"));
            this.MapTown.LoadContent();
            this.MapTown.DecorateMap();

            this.MapDifficultyEasy = new HMap(125, 125, 1f);
            this.MapDifficultyEasy.MakeCaverns();
            this.MapDifficultyEasy.LoadContent();
            this.MapDifficultyEasy.DecorateMap();

            this.MapDifficultyMedium = new HMap(125, 125, 1f);
            this.MapDifficultyMedium.MakeCaverns();
            this.MapDifficultyMedium.LoadContent();
            this.MapDifficultyMedium.DecorateMap();

            this.MapDifficultyHard = new HMap(125, 125, 1f);
            this.MapDifficultyHard.MakeCaverns();
            this.MapDifficultyHard.LoadContent();
            this.MapDifficultyHard.DecorateMap();

            this.CurrentMap = this.MapTown;
        }

        private void LoadHostiles()
        {
            for (int i = 0; i < 150; i++)
            {
                this.MapDifficultyEasy.Hostiles.Add(new HHostile(new FeatureCollection(), this.MapDifficultyEasy.GetRandomFloorPoint(), 1f, 1.5f, "draugr"));
            }
            for (int i = 0; i < 250; i++)
            {
                this.MapDifficultyMedium.Hostiles.Add(new HHostile(new FeatureCollection(), this.MapDifficultyMedium.GetRandomFloorPoint(), 1f, 1.5f, "draugr"));
            }
            for (int i = 0; i < 500; i++)
            {
                this.MapDifficultyHard.Hostiles.Add(new HHostile(new FeatureCollection(), this.MapDifficultyHard.GetRandomFloorPoint(), 1f, 1.5f, "draugr"));
            }
        }

        private void DrawHostiles(SpriteBatch sb)
        {
            List<HHostile> hostiles = this.CurrentMap.Hostiles;
            int nbrHostiles = hostiles.Count;

            for (int i = 0; i < nbrHostiles; i++)
            {
                hostiles[i].Draw(sb);
            }
        }

        /// <summary>
        /// Unloads the maps
        /// </summary>
        private void UnloadMaps()
        {
            this.CurrentMap = null;
            this.MapDifficultyEasy.UnloadContent();
            this.MapDifficultyMedium.UnloadContent();
            this.MapDifficultyHard.UnloadContent();
            this.MapTown.UnloadContent();
        }

        /// <summary>
        /// Loads the playable character
        /// </summary>
        private void LoadPlayableCharacter()
        {
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
            Vector2 position = this.CurrentMap.GetRandomFloorPoint();
            this.PlayableCharacter = new HHero(f, position, 1f, 1.5f, "hero");
            this.PlayableCharacter.LoadContent();
        }

        /// <summary>
        /// Update method to update the map control mechanisms
        /// </summary>
        private void UpdateMapControl()
        {
            if (InputManager.Instance.IsKeyboardKeyDown(Keys.F8))
            {
                this.CurrentMap.MakeRandomlyFilledMap();
                this.CurrentMap.MakeCaverns();
                this.CurrentMap.DecorateMap();
            }

            if (InputManager.Instance.IsKeyboardKeyDown(Keys.F9))
            {
                if (this.CurrentMap == this.MapDifficultyEasy)
                    this.CurrentMap = this.MapDifficultyMedium;
                else if (this.CurrentMap == this.MapDifficultyMedium)
                    this.CurrentMap = this.MapDifficultyHard;
                else if (this.CurrentMap == this.MapDifficultyHard)
                    this.CurrentMap = this.MapTown;
                else if (this.CurrentMap == this.MapTown)
                    this.CurrentMap = this.MapDifficultyEasy;

                this.PlayableCharacter.Position = this.CurrentMap.GetRandomFloorPoint();
                this.PlayableCharacter.Bounds.SetBounds(this.PlayableCharacter.Position, this.PlayableCharacter.Texture.Width, this.PlayableCharacter.Texture.Height);
                this.Camera.Position = this.PlayableCharacter.Position;
            }
        }
    }
}
