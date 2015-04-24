/*
 * Author : Yannick R. Brodard
 * File name : ScreenManager.cs
 * Version : 0.1.201504241035
 * Description : All the screens of the game are manage here
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using HelProject.Tools;

namespace HelProject.UI
{
    /// <summary>
    /// Singleton class, all the screens of the game are managed here
    /// </summary>
    public class ScreenManager
    {
        protected const int DEFAULT_SCREEN_WIDTH = 1280;
        protected const int DEFAULT_SCREEN_HEIGHT = 720;

        private static ScreenManager _instance; // instance of this class
        private XmlManager<GameScreen> _xmlGameScreenManager; //xml manager for the screens
        private GameScreen _currentScreen; // current screen shown in the game
        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;

        /// <summary>
        /// Dimensions of the screen
        /// </summary>
        public Vector2 Dimensions { get; private set; }

        /// <summary>
        /// Content of the screen
        /// </summary>
        public ContentManager Content { get; private set; }

        /// <summary>
        /// Singleton instance of this class
        /// </summary>
        public static ScreenManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScreenManager();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Creates a screen manager
        /// </summary>
        private ScreenManager()
        {
            Dimensions = new Vector2(DEFAULT_SCREEN_WIDTH, DEFAULT_SCREEN_HEIGHT);
            _currentScreen = new SplashScreen();
            _xmlGameScreenManager = new XmlManager<GameScreen>();
            _xmlGameScreenManager.TypeClass = _currentScreen.TypeClass;
            _currentScreen = _xmlGameScreenManager.Load("Load/SplashScreen.xml");
        }

        /// <summary>
        /// Loads the content
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            this.Content = new ContentManager(content.ServiceProvider, "Content");
            _currentScreen.LoadContent();
        }

        /// <summary>
        /// Unloads the content
        /// </summary>
        public void UnloadContent()
        {
            _currentScreen.UnloadContent();
        }

        /// <summary>
        /// Updates the content
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            _currentScreen.Update(gameTime);
        }

        /// <summary>
        /// Draws the content
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            _currentScreen.Draw(spriteBatch);
        }
    }
}
