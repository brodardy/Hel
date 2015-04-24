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
using System.Threading.Tasks;
using System.Diagnostics;

namespace HelProject.UI
{
    /// <summary>
    /// Singleton class, all the screens of the game are managed here
    /// </summary>
    public class ScreenManager
    {
        protected const int DEFAULT_SCREEN_WIDTH = 1280;
        protected const int DEFAULT_SCREEN_HEIGHT = 720;

        private bool _transitionDelayActive;
        private int _transitionCountDown;
        private int _lastCount;
        private GameScreen _transitionScreen;

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
            this.Dimensions = new Vector2(DEFAULT_SCREEN_WIDTH, DEFAULT_SCREEN_HEIGHT);
            this._currentScreen = new SplashScreen();
            this._xmlGameScreenManager = new XmlManager<GameScreen>();
            this._xmlGameScreenManager.TypeClass = _currentScreen.TypeClass;
            this._currentScreen = _xmlGameScreenManager.Load("Load/SplashScreen.xml");

            this._transitionDelayActive = false;
            this._transitionCountDown = 0;
            _transitionScreen = null;

            GameScreen sndSC = new SplashScreen();
            this._xmlGameScreenManager = new XmlManager<GameScreen>();
            this._xmlGameScreenManager.TypeClass = sndSC.TypeClass;
            sndSC = _xmlGameScreenManager.Load("Load/SplashScreen2.xml");
            this.TransitionCountDown(sndSC, 20);
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

            if (this._transitionDelayActive)
            {
                int currentTime = Convert.ToInt32(gameTime.TotalGameTime.Seconds);

                Debug.WriteLine(currentTime);

                if (this._lastCount == -1)
                {
                    this._lastCount = currentTime;
                }
                else
                {
                    int diff = currentTime - this._lastCount;
                    if (diff > 0)
                    {
                        this._transitionCountDown -= diff;
                    }
                }

                if (this._transitionCountDown <= 0)
                {
                    this._transitionDelayActive = false;
                    this.Transition(_transitionScreen);
                }
            }
        }

        /// <summary>
        /// Draws the content
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            _currentScreen.Draw(spriteBatch);
        }

        /// <summary>
        /// Transitions the screen to another one
        /// </summary>
        /// <param name="gameScreen"></param>
        public void Transition(GameScreen gameScreen)
        {
            this._transitionCountDown = 0;
            this._transitionDelayActive = false;
            this._lastCount = -1;

            this._currentScreen = gameScreen;
            this._currentScreen.LoadContent();
        }

        public void TransitionCountDown(GameScreen nextScreen, int time)
        {
            this._transitionScreen = nextScreen;
            this._transitionCountDown = time;
            this._transitionDelayActive = true;
            this._lastCount = -1;
        }
    }
}
