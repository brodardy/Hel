﻿/*
 * Author : Yannick R. Brodard
 * File name : SplashScreen.cs
 * Version : 0.1.201504241035
 * Description : Fills the screen with an image
 */

#region USING STATEMENTS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;
using HelProject.Tools;
using Microsoft.Xna.Framework.Input;
#endregion

namespace HelProject.UI
{
    /// <summary>
    /// Splash screen
    /// </summary>
    public class SplashScreen : GameScreen
    {
        private Image _backgroundImage;
        private GameScreen _nextScreen;

        /// <summary>
        /// Next screen after the splash screen
        /// </summary>
        public GameScreen NextScreen
        {
            get { return _nextScreen; }
            set { _nextScreen = value; }
        }

        /// <summary>
        /// Background Image of the splashscreen
        /// </summary>
        [XmlElement("Image")]
        public Image BackgroundImage
        {
            get { return _backgroundImage; }
            set { _backgroundImage = value; }
        }

        /// <summary>
        /// Loads the content of the screen
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            BackgroundImage.LoadContent();
        }

        /// <summary>
        /// Unloads the content of the screen
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
            BackgroundImage.UnloadContent();
        }

        /// <summary>
        /// Updates the content screen 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            BackgroundImage.Update(gameTime);
            if (InputManager.Instance.IsKeyboardKeyReleased(Keys.Space))
            {
                if (this.NextScreen == null)
                {
                    this.NextScreen = new SplashScreen();
                    this.NextScreen = ScreenManager.Instance.PrepareScreen("Load/SplashScreen2.xml",
                                                                           ScreenManager.ScreenTypes.SPLASH);
                }

                ScreenManager.Instance.Transition(this.NextScreen);
            }
        }

        /// <summary>
        /// Draws the content of the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            BackgroundImage.Draw(spriteBatch);
        }
    }
}
