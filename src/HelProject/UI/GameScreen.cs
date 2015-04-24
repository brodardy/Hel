/*
 * Author : Yannick R. Brodard
 * File name : GameScreen.cs
 * Version : 0.1.201504241035
 * Description : Is the base class for all the screens of the game
 */

#region USING STATEMENTS
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
#endregion

namespace HelProject.UI
{
    /// <summary>
    /// Base class for all the screens of the game
    /// </summary>
    public class GameScreen
    {
        [XmlIgnore]
        private Type _type;
        private ContentManager _content;

        /// <summary>
        /// Content of the gamescreen
        /// </summary>
        protected ContentManager Content
        {
            get { return _content; }
            set { _content = value; }
        }

        /// <summary>
        /// Type of the class
        /// </summary>
        [XmlIgnore]
        public Type TypeClass
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Creates a game screen
        /// </summary>
        public GameScreen()
        {
            TypeClass = this.GetType(); // sets the type of the gamescreen
        }

        /// <summary>
        /// Loads the content of the screen
        /// </summary>
        public virtual void LoadContent()
        {
            this.Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        /// <summary>
        /// Unloads the content of the screen
        /// </summary>
        public virtual void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Updates the content of the screen
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draws the content of the screen
        /// </summary>
        /// <param name="spriteBatch">Sprite batch</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
