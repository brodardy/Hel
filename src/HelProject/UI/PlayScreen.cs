/*
 * Author : Yannick R. Brodard
 * File name : PlayScreen.cs
 * Version : 0.1.201504301315
 * Description : Screen for the gameplay
 */

#region USING STATEMENTS
using HelProject.GameWorld;
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
        private GMap _map;

        public GMap Map
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
            this.Map = new GMap(0.12f, 200, 200, 45);
            //this.Map.MakeCaverns();
            this.Map.LoadContent();
        }

        /// <summary>
        /// Unloads the content of the window
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
            this.Map.UnloadContent();
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
        }

        /// <summary>
        /// Draws on the window
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Map.Draw(spriteBatch);
        }
    }
}
