/*
 * Author : Yannick R. Brodard
 * File name : PlayScreen.cs
 * Version : 0.3.201505120902
 * Description : Screen for the gameplay
 */

#region USING STATEMENTS
using HelHelProject.Tools;
using HelProject.Features;
using HelProject.GameWorld;
using HelProject.GameWorld.Entities;
using HelProject.GameWorld.Map;
using HelProject.Tools;
using HelProject.UI.HUD;
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

        private SelectionAid _selectionAssistant;

        private SpriteFont font;

        /// <summary>
        /// Selection assistant for the game
        /// </summary>
        public SelectionAid SelectionAssistant
        {
            get { return _selectionAssistant; }
            set { _selectionAssistant = value; }
        }

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
            font = Content.Load<SpriteFont>("Lane");

            this.LoadMaps();
            this.LoadPlayableCharacter();
            this.LoadHostiles();
            this.SelectionAssistant = new SelectionAid();

            // Camera initialisation, gets the width and height of the window
            // and the position of the hero
            this.Camera = new Camera(this.PlayableCharacter.Position, MainGame.Instance.GraphicsDevice.Viewport.Width, MainGame.Instance.GraphicsDevice.Viewport.Height);

            XmlManager<HItem> item = new XmlManager<HItem>();
            item.TypeClass = typeof(HItem);
            FeatureCollection itFeatures = new FeatureCollection();
            itFeatures.SetAllToZero();
            itFeatures.InitialAttackSpeed = 1.4f;
            itFeatures.MinimumDamage = 15.0f;
            itFeatures.MaximumDamage = 24.0f;
            itFeatures.Strenght = 6.0f;
            itFeatures.Vitality = 3.0f;
            itFeatures.AttackSpeed = 15.0f;
            HItem it = new HItem("Death blade", HItem.ItemTypes.Sword, itFeatures, "cursor_normal", true, new Vector2(50f, 50f), "Blade of the death maiden");

            item.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\item.xml", it);

            this.MapDifficultyEasy.OnFloorItems.Add(it);
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
            this.UpdateHostiles(gameTime);
            this.ManageDeadEntities();
            this.SelectionAssistant.Update(gameTime);
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
            this.DrawSelection(spriteBatch);

            if (MainGame.DEBUG_MODE)
            {
                Primitives2D.Instance.FillRectangle(spriteBatch, 0, 0, 200, 150, new Color(Color.LightBlue, 0.5f));
                Primitives2D.Instance.DrawRectangle(spriteBatch, 0, 0, 200, 150, Color.Black, 5);

                spriteBatch.DrawString(font, "Camera position (IG unit)", new Vector2(10, 10), Color.Black);
                spriteBatch.DrawString(font, "X => " + Camera.Position.X.ToString(), new Vector2(15, 30), Color.Black);
                spriteBatch.DrawString(font, "Y => " + Camera.Position.Y.ToString(), new Vector2(15, 50), Color.Black);
                spriteBatch.DrawString(font, "Mouse position (IG unit)", new Vector2(10, 80), Color.Black);
                spriteBatch.DrawString(font, "X => " + Camera.GetMousePositionRelativeToMap().X, new Vector2(15, 100), Color.Black);
                spriteBatch.DrawString(font, "Y => " + Camera.GetMousePositionRelativeToMap().Y.ToString(), new Vector2(15, 120), Color.Black);
            }
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

        /// <summary>
        /// Loads the hostiles on the different maps
        /// </summary>
        private void LoadHostiles()
        {
            FeatureCollection f = new FeatureCollection();
            f.SetToDraugrLvlOne();

            for (int i = 0; i < 75; i++)
            {
                this.MapDifficultyEasy.Hostiles.Add(new HHostile(f, this.MapDifficultyEasy.GetRandomFloorPoint(), 1f, 1.5f, "draugr"));
            }

            for (int i = 0; i < 175; i++)
            {
                this.MapDifficultyMedium.Hostiles.Add(new HHostile(f, this.MapDifficultyMedium.GetRandomFloorPoint(), 1f, 1.5f, "draugr"));
            }

            for (int i = 0; i < 250; i++)
            {
                this.MapDifficultyHard.Hostiles.Add(new HHostile(f, this.MapDifficultyHard.GetRandomFloorPoint(), 1f, 1.5f, "draugr"));
            }
        }

        /// <summary>
        /// Draws the hostiles on the current map
        /// </summary>
        /// <param name="sb">Sprite batch</param>
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
        /// Updates the mechanismes of the hostiles
        /// </summary>
        /// <param name="gameTime">Game time</param>
        private void UpdateHostiles(GameTime gameTime)
        {
            List<HHostile> hostiles = this.CurrentMap.Hostiles;
            int nbrHostiles = hostiles.Count;

            for (int i = 0; i < nbrHostiles; i++)
            {
                hostiles[i].Update(gameTime);
            }
        }

        /// <summary>
        /// Removes dead hostiles
        /// </summary>
        private void ManageDeadEntities()
        {
            List<HHostile> hostiles = this.CurrentMap.Hostiles;
            List<HHostile> hostilesToDestroy = new List<HHostile>();
            int nbrHostiles = hostiles.Count;

            for (int i = 0; i < nbrHostiles; i++)
            {
                if (hostiles[i].IsDead)
                    hostilesToDestroy.Add(hostiles[i]);
            }

            int nbrHostilesToDestroy = hostilesToDestroy.Count;
            for (int i = 0; i < nbrHostilesToDestroy; i++)
            {
                hostilesToDestroy[i].UnloadContent();
                hostiles.Remove(hostilesToDestroy[i]);
            }

            if (this.PlayableCharacter.IsDead)
            {
                PlayScreen.Instance.TransitionToMap(PlayScreen.Instance.MapTown);
                this.PlayableCharacter.ActualFeatures.LifePoints = this.PlayableCharacter.MaximizedFeatures.LifePoints;
                this.PlayableCharacter.IsDead = false;
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
        /// Draws the selection of the mouse
        /// </summary>
        /// <param name="sb">Sprite batch</param>
        private void DrawSelection(SpriteBatch sb)
        {
            int nbObjects = this.SelectionAssistant.SelectedObjects.Count;
            for (int i = 0; i < nbObjects; i++)
            {
                if (this.SelectionAssistant.SelectedObjects[i] is HHostile)
                {
                    HHostile hostile = this.SelectionAssistant.SelectedObjects[i] as HHostile;
                    Vector2 start = ScreenManager.Instance.GetCorrectScreenPosition(hostile.Bounds.Position, this.Camera.Position);
                    Vector2 end = ScreenManager.Instance.GetCorrectScreenPosition(new Vector2(hostile.Bounds.Position.X + hostile.Bounds.Width, hostile.Bounds.Position.Y + hostile.Bounds.Height), this.Camera.Position);
                    end.X += 1f;
                    end.Y += 1f;
                    Primitives2D.Instance.DrawRectangle(sb, start, end, Color.Yellow, 2);
                }
            }
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
                    TransitionToMap(this.MapDifficultyMedium);
                else if (this.CurrentMap == this.MapDifficultyMedium)
                    TransitionToMap(this.MapDifficultyHard);
                else if (this.CurrentMap == this.MapDifficultyHard)
                    TransitionToMap(this.MapTown);
                else if (this.CurrentMap == this.MapTown)
                    TransitionToMap(this.MapDifficultyEasy);
            }
        }

        /// <summary>
        /// Transitions to another map
        /// </summary>
        /// <param name="map"></param>
        public void TransitionToMap(HMap map)
        {
            this.CurrentMap = map;
            this.PlayableCharacter.Position = this.CurrentMap.GetRandomFloorPoint();
            this.PlayableCharacter.Bounds.SetBoundsWithTexture(this.PlayableCharacter.Position, this.PlayableCharacter.Texture.Width, this.PlayableCharacter.Texture.Height);
            this.Camera.Position = this.PlayableCharacter.Position;
        }
    }
}
