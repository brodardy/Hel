using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RogueSharp;
using RogueSharp.Random;

namespace HelProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D _floor;
        private Texture2D _wall;
        private IMap _map;
        private Player _player;
        private InputState _inputState;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _inputState = new InputState();
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

            IMapCreationStrategy<Map> mapCreationStrategy = new RandomRoomsMapCreationStrategy<Map>(50, 30, 100, 7, 3);
            _map = Map.Create(mapCreationStrategy);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _floor = Content.Load<Texture2D>("scenary/floor");
            _wall = Content.Load<Texture2D>("scenary/wall");
            Cell startingCell = GetRandomEmptyCell();
            _player = new Player { X = startingCell.X, Y = startingCell.Y, Scale = .5f, Sprite = Content.Load<Texture2D>("entities/heroi") };
            UpdatePlayerFieldOfView();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (_inputState.IsExitGame(PlayerIndex.One))
            {
                Exit();
            }
            else
            {
                if (_player.HandleInput(_inputState, _map))
                {
                    UpdatePlayerFieldOfView();
                }
            }

            // TODO: Add your update logic here

            _inputState.Update();

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

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            int sizeOfSprites = 32;
            float scale = .5f;
            foreach (Cell cell in _map.GetAllCells())
            {
                var position = new Vector2(cell.X * sizeOfSprites * scale, cell.Y * sizeOfSprites * scale);
                if (!cell.IsExplored)
                {
                    continue;
                }
                Color tint = Color.White;
                if (!cell.IsInFov)
                {
                    tint = Color.DarkGray;
                }
                if (cell.IsWalkable)
                {
                    spriteBatch.Draw(_floor, position,
                      null, null, null, 0.0f, new Vector2(scale, scale),
                      tint, SpriteEffects.None, 0.8f);
                }
                else
                {
                    spriteBatch.Draw(_wall, position,
                       null, null, null, 0.0f, new Vector2(scale, scale),
                       tint, SpriteEffects.None, 0.8f);
                }
            }

            _player.Draw(spriteBatch);

            spriteBatch.End();

            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //spriteBatch.Draw(_wall, Vector2.Zero, Color.White);
            //spriteBatch.Draw(_floor, new Vector2(32, 32), Color.White);
            //spriteBatch.End();

            base.Draw(gameTime);
        }

        private Cell GetRandomEmptyCell()
        {
            IRandom random = new DotNetRandom();

            while (true)
            {
                int x = random.Next(49);
                int y = random.Next(29);
                if (_map.IsWalkable(x, y))
                {
                    return _map.GetCell(x, y);
                }
            }
        }

        private void UpdatePlayerFieldOfView()
        {
            _map.ComputeFov(_player.X, _player.Y, 4, true);
            foreach (Cell cell in _map.GetAllCells())
            {
                if (_map.IsInFov(cell.X, cell.Y))
                {
                    _map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }
    }
}
