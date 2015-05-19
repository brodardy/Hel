using HelHelProject.Tools;
using HelProject.Tools;
using HelProject.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HelProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Game
    {
        private static MainGame _instance;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 _cursorPosition;


        /// <summary>
        /// Instance of the Main Game
        /// </summary>
        public static MainGame Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainGame();
                return _instance;
            }
        }

        /// <summary>
        /// private Constructor
        /// </summary>
        private MainGame()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base. Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //this.IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            _graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            _graphics.ApplyChanges();

            TextureManager.Instance.Load("Load/Textures.xml");

            this._cursorPosition = new Vector2();
            this.Window.Title = "Hel: The pixelated horror";
            this.Window.Position = new Point(GraphicsDevice.DisplayMode.Width / 2 - (int)ScreenManager.Instance.Dimensions.X / 2,
                                             GraphicsDevice.DisplayMode.Height / 2 - (int)ScreenManager.Instance.Dimensions.Y / 2);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Primitives2D.Instance.LoadContent();
            ScreenManager.Instance.SMGraphicsDevice = GraphicsDevice;
            ScreenManager.Instance.SMSpriteBatch = _spriteBatch;
            ScreenManager.Instance.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            ScreenManager.Instance.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //Exit();

            // TODO: Add your update logic here

            ScreenManager.Instance.Update(gameTime);
            InputManager.Instance.Update(gameTime);
            this._cursorPosition.X = InputManager.Instance.MsState.X;
            this._cursorPosition.Y = InputManager.Instance.MsState.Y;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            ScreenManager.Instance.Draw(_spriteBatch);
            _spriteBatch.Draw(TextureManager.Instance.LoadedTextures["cursor_normal"], this._cursorPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
