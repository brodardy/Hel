/*
 * Author : Yannick R. Brodard
 * File name : ScreenManager.cs
 * Version : 0.2.201504271045
 * Description : All the screens of the game are manage here
 */

#region USING STATEMENTS
using HelProject.Tools;
using HelProject.UI.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace HelProject.UI
{
    /// <summary>
    /// Singleton class, all the screens of the game are managed here
    /// </summary>
    public class ScreenManager
    {
        #region PROTECTED CONSTANTS
        protected const int DEFAULT_SCREEN_WIDTH = 1280;
        protected const int DEFAULT_SCREEN_HEIGHT = 720;
        protected const int DEFAULT_SPLASH_SCREEN_TIME = 3;
        #endregion

        #region TRANSITION PRIVATE VARIABLES
        private bool _transitionDelayActive;
        private int _transitionTime;
        private double _transitionFirstCount;
        private GameScreen _transitionScreen;
        #endregion

        #region PRIVATE VARIABLES
        private static ScreenManager _instance; // instance of this class
        private XmlManager<GameScreen> _xmlGameScreenManager; //xml manager for the screens
        private GameScreen _currentScreen; // current screen shown in the game
        #endregion

        #region PUBLIC VARIABLES
        public GraphicsDevice SMGraphicsDevice;
        public SpriteBatch SMSpriteBatch;
        #endregion

        #region PROPRIETIES
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
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Creates a screen manager
        /// </summary>
        private ScreenManager()
        {
            // Initialisation
            this.Dimensions = new Vector2(DEFAULT_SCREEN_WIDTH, DEFAULT_SCREEN_HEIGHT); // fix the dim of the window
            this._transitionDelayActive = false; // init transition variables
            this._transitionTime = 0;
            this._transitionScreen = null;

            // shows the first splash screen
            this._currentScreen = this.PrepareScreen("Load/SplashScreen.xml", ScreenTypes.SPLASH);
            (this._currentScreen as SplashScreen).NextScreen = this.PrepareScreen("Load/MenuScreen1.xml", ScreenTypes.MENU);
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Loads the content
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            this.Content = new ContentManager(content.ServiceProvider, "Content");
            this._currentScreen.LoadContent();
        }

        /// <summary>
        /// Unloads the content
        /// </summary>
        public void UnloadContent()
        {
            this._currentScreen.UnloadContent();
        }

        /// <summary>
        /// Updates the content
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            this._currentScreen.Update(gameTime);

            // Transition mechanism
            if (this._transitionDelayActive)
            {
                double currentTime = gameTime.TotalGameTime.TotalSeconds; // gets the current time

                // initialise the first count if it's the first time it passes here
                if (this._transitionFirstCount < 0.0d)
                {
                    this._transitionFirstCount = currentTime;
                }
                else
                {
                    // calculates the time difference between the first count and the current count
                    double diff = currentTime - (double)this._transitionFirstCount;

                    // if this time is superior or equal to the specified transition time
                    // the Transition method is called with the specified screen
                    if (diff >= (double)this._transitionTime)
                    {
                        this.Transition(this._transitionScreen);
                    }
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
        /// <param name="nextScreen"></param>
        public void Transition(GameScreen nextScreen)
        {
            // resets the transition variables
            this._transitionTime = 0;
            this._transitionDelayActive = false;
            this._transitionFirstCount = -1.0d;

            this.UnloadContent(); // unloads the content of the current screen
            this._currentScreen = nextScreen; // place the new screen
            this._currentScreen.LoadContent(); // loads the new screen
        }

        /// <summary>
        /// Activates a screen transition for the specified time
        /// </summary>
        /// <param name="nextScreen">Next screen that will appear</param>
        /// <param name="time">Time before transition</param>
        public void Transition(GameScreen nextScreen, int time)
        {
            this._transitionScreen = nextScreen;
            this._transitionTime = time;
            this._transitionDelayActive = true;
            this._transitionFirstCount = -1.0d;
        }

        /// <summary>
        /// Prepares an initialized screen
        /// </summary>
        /// <param name="loadContent">Path to the XML file for the initialization information</param>
        /// <param name="screenType">Type of the screen</param>
        /// <returns>The prepared screen</returns>
        public GameScreen PrepareScreen(string loadContent, ScreenTypes screenType)
        {
            GameScreen preparedScreen;

            switch (screenType)
            {
                case ScreenTypes.SPLASH:
                    preparedScreen = new SplashScreen();
                    break;
                case ScreenTypes.MENU:
                    preparedScreen = new MenuScreen();
                    break;
                case ScreenTypes.INGAME:
                    preparedScreen = new SplashScreen();
                    break;
                case ScreenTypes.LOADING:
                    preparedScreen = new SplashScreen();
                    break;
                default:
                    preparedScreen = new SplashScreen();
                    break;
            }

            this._xmlGameScreenManager = new XmlManager<GameScreen>();
            this._xmlGameScreenManager.TypeClass = preparedScreen.TypeClass;
            preparedScreen = _xmlGameScreenManager.Load(loadContent);

            return preparedScreen;
        }

        /// <summary>
        /// Gives the current screen type of the game
        /// </summary>
        /// <returns></returns>
        public ScreenTypes GetCurrentScreenType()
        {
            if (this._currentScreen is SplashScreen)
            {
                return ScreenTypes.SPLASH;
            }
            else if (this._currentScreen is MenuScreen)
            {
                return ScreenTypes.MENU;
            }
            else
            {
                return ScreenTypes.LOADING;
            }
        }

        public Vector2 GetCorrectScreenPosition(Vector2 pos, int tileSize = 32, float scale = 1.0f)
        {
            float offSetX = 0f, offSetY = 0f;
            offSetX = -pos.X;
            offSetY = -pos.Y;

            return new Vector2(pos.X * tileSize * scale + // X Pos
                               offSetX * tileSize * scale +
                               this.Dimensions.X / 2,
                               pos.Y * tileSize * scale + // Y Pos
                               offSetY * tileSize * scale +
                               this.Dimensions.Y / 2);
        }
        #endregion

        #region PUBLIC ENUMERATORS
        /// <summary>
        /// Available screen types
        /// </summary>
        public enum ScreenTypes
        {
            SPLASH, // screen with an image
            MENU,   // screen with an interactive menu
            INGAME, // screen with integrated gameplay
            LOADING // screen used for loading moments
        };
        #endregion
    }
}
