/*
 * Author : Yannick R. Brodard
 * File name : Image.cs
 * Version : 0.1.201504241405
 * Description : Image class, this manages all the images and text needed for the game
 */

#region USING STATEMENTS
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
#endregion

namespace HelProject.UI
{
    public class Image
    {
        #region ATTRIBUTES FOR PROP
        private float _alphaChannel;
        private string _text, _fontName, _imagePath;
        private Vector2 _position, _scale;
        private Texture2D _texture;
        private Rectangle _sourceRect;
        private string _effects;
        private bool _isActive;
        #endregion

        #region ATTRIBUTES
        private Vector2 _origin;
        private ContentManager _content;
        private RenderTarget2D _renderTarget;
        private SpriteFont _font;
        #endregion

        #region PROPRIETIES
        /// <summary>
        /// Aplha channel for transparacy
        /// </summary>
        [XmlElement("Alpha")]
        public float AlphaChannel
        {
            get { return _alphaChannel; }
            set { _alphaChannel = value; }
        }

        /// <summary>
        /// Path of the image file
        /// </summary>
        [XmlElement("Path")]
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }

        /// <summary>
        /// Name of the font
        /// </summary>
        public string FontName
        {
            get { return _fontName; }
            set { _fontName = value; }
        }

        /// <summary>
        /// Text for the image
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// Scale of the image
        /// </summary>
        public Vector2 Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        /// <summary>
        /// Position of the image
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Rectangle around the image
        /// </summary>
        public Rectangle SourceRect
        {
            get { return _sourceRect; }
            set { _sourceRect = value; }
        }

        /// <summary>
        /// Texture 2D, media for the image
        /// </summary>
        [XmlIgnore]
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        /// <summary>
        /// Is the image active
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Makes an empty image
        /// </summary>
        public Image()
        {
            ImagePath = Text = String.Empty;
            FontName = "Lane";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            AlphaChannel = 1.0f;
            SourceRect = Rectangle.Empty;
        }

        /// <summary>
        /// Effects of the image
        /// </summary>
        public string Effects
        {
            get { return _effects; }
            set { _effects = value; }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Loads the content of the image
        /// </summary>
        public void LoadContent()
        {
            _content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            // Gets the image if there is  one
            if (ImagePath != String.Empty)
            {
                this.Texture = this._content.Load<Texture2D>(ImagePath);
            }

            // loads the font
            this._font = _content.Load<SpriteFont>(FontName);

            Vector2 dimensions = Vector2.Zero;

            // Sets the width
            if (Texture != null)
                dimensions.X += Texture.Width;
            dimensions.X += _font.MeasureString(Text).X;

            // Sets the height
            if (Texture != null)
                dimensions.Y = Math.Max(Texture.Height, _font.MeasureString(Text).Y);
            else
                dimensions.Y = _font.MeasureString(Text).Y;

            // Creates the rectangle with the dimensions
            if (SourceRect == Rectangle.Empty)
            {
                SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);
            }

            /* Create the image */
            this._renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)dimensions.X, (int)dimensions.Y);
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(_renderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();
            if (Texture != null)
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.DrawString(_font, Text, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.End();

            // Places the new image in the texture
            this.Texture = _renderTarget;

            // Gives back the render target to default
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);
        }

        /// <summary>
        /// Unloads the content of the image
        /// </summary>
        public void UnloadContent()
        {
            _content.Unload();
        }

        /// <summary>
        /// Updates the image
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime) { /* no code... */ }

        /// <summary>
        /// Draws the image
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            _origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);
            spriteBatch.Draw(Texture, Position, SourceRect, Color.White * AlphaChannel, 0.0f, _origin, Scale, SpriteEffects.None, 0.0f);
        }
        #endregion
    }
}
