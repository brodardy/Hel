/*
 * Author : Yannick R. Brodard
 * File name : PlayScreen.cs
 * Version : 0.1.201504301315
 * Description : Screen for the gameplay
 */

#region USING STATEMENTS
using HelProject.GameWorld;
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
        private Texture2D _floor;
        private Texture2D _wall;

        /// <summary>
        /// Loads the content of the window
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            _map = new HMap(80, 80, 46);
            _map.MakeRandomlyFilledMap();
            _map.MakeCaverns(5);

            _floor = Content.Load<Texture2D>("scenary/floor");
            _wall = Content.Load<Texture2D>("scenary/wall");
        }

        /// <summary>
        /// Unloads the content of the window
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Updates the mechanisms
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.IsKeyboardKeyDown(Keys.Space))
            {
                _map.MakeRandomlyFilledMap();
                _map.MakeCaverns(3);
            }
        }

        /// <summary>
        /// Draws on the window
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            int sizeOfSprites = 32;
            float scale = .25f;
            for (int y = 0; y < _map.Height; y++)
            {
                for (int x = 0; x < _map.Width; x++)
                {
                    HObject cell = _map.GetCell(x, y);
                    Vector2 position = new Vector2(cell.Position.X * sizeOfSprites * scale, cell.Position.Y * sizeOfSprites * scale);
                    if (cell.IsWalkable)
                    {
                        spriteBatch.Draw(_floor, position, null, null, null, 0.0f, new Vector2(scale, scale), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(_wall, position, null, null, null, 0.0f, new Vector2(scale, scale), Color.White);
                    }
                }
            }
        }
    }
}
