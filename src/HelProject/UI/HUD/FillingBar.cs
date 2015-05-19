/*
 * Author : Yannick R. Brodard
 * File name : FillingBar.cs
 * Version : 0.1.201505191335
 * Description : Represents a filling bar
 */


using HelHelProject.Tools;
using HelProject.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HelProject.UI.HUD
{
    /// <summary>
    /// Class of the filling bar
    /// </summary>
    public class FillingBar
    {
        private const float FILLER_MINIMUM = 0f;
        private const float FILLER_MAXIMUM = 100f;

        private FRectangle _container;
        private Color _borderColor;
        private Color _fillerColor;
        private Color _backgroundColor;
        private FillingDirection _movementDirection;
        private float _maxValue;
        private float _actualValue;

        /// <summary>
        /// Actual value of the filling
        /// </summary>
        public float ActualValue
        {
            get { return _actualValue; }
            set { _actualValue = value; }
        }

        /// <summary>
        /// Maximum value of the filling
        /// </summary>
        public float MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        /// <summary>
        /// Filling percentage
        /// </summary>
        public float FillerPercentage
        {
            get { return this.ActualValue * 100f / this.MaxValue; }
        }

        /// <summary>
        /// Direction of the filling
        /// </summary>
        public FillingDirection MovementDirection
        {
            get { return _movementDirection; }
            set { _movementDirection = value; }
        }

        /// <summary>
        /// Container of the bar
        /// </summary>
        public FRectangle Container
        {
            get { return _container; }
            set
            {
                _container = value;
            }
        }

        /// <summary>
        /// Color of the border
        /// </summary>
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

        /// <summary>
        /// Color of the filler
        /// </summary>
        public Color FillerColor
        {
            get { return _fillerColor; }
            set { _fillerColor = value; }
        }

        /// <summary>
        /// Color of the background
        /// </summary>
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        /// <summary>
        /// Creates a filling bar
        /// </summary>
        public FillingBar(FillingDirection fillingDirection, FRectangle rectangle, Color borderColor, Color fillerColor, Color backgroundColor,
            float maxValue = FILLER_MAXIMUM, float actualValue = FILLER_MAXIMUM, int borderThickness = 1)
        {
            this.MovementDirection = fillingDirection;
            this.Container = rectangle;
            this.BorderColor = borderColor;
            this.FillerColor = fillerColor;
            this.BackgroundColor = backgroundColor;
            this.MaxValue = maxValue;
            this.ActualValue = actualValue;
        }

        /// <summary>
        /// Draws the filling bar
        /// </summary>
        /// <param name="spriteBatch">Sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Primitives2D.Instance.FillRectangle(spriteBatch, this.Container, this.BackgroundColor);
            Primitives2D.Instance.DrawRectangle(spriteBatch, this.Container, this.BorderColor);
            Vector2 start = Vector2.Zero;
            Vector2 end = Vector2.Zero;
            int fillingPos = 0;
            switch (this.MovementDirection)
            {
                case FillingDirection.LeftToRight:
                    fillingPos = (int)(((this.FillerPercentage / 100f) * this.Container.Width) + this.Container.X);
                    start = new Vector2(this.Container.Position.X + 1, this.Container.Position.Y + 1);
                    end = new Vector2(fillingPos, this.Container.Position.Y + this.Container.Height - 1);
                    Primitives2D.Instance.FillRectangle(spriteBatch, start, end, this.FillerColor);
                    break;
                case FillingDirection.RightToLeft:
                    fillingPos = (int)((this.Container.X + this.Container.Width) - ((this.FillerPercentage / 100f) * this.Container.Width));
                    start = new Vector2(fillingPos, this.Container.Position.Y + 1);
                    end = new Vector2(this.Container.Position.Y + this.Container.Width - 1, this.Container.Position.Y + this.Container.Height - 1);
                    Primitives2D.Instance.FillRectangle(spriteBatch, start, end, this.FillerColor);
                    break;
                case FillingDirection.TopToBottom:
                    fillingPos = (int)(((this.FillerPercentage / 100f) * this.Container.Height) + this.Container.Y);
                    start = new Vector2(this.Container.Position.X + 1, this.Container.Position.Y + 1);
                    end = new Vector2(this.Container.Position.X + this.Container.Width - 1, fillingPos);
                    Primitives2D.Instance.FillRectangle(spriteBatch, start, end, this.FillerColor);
                    break;
                case FillingDirection.BottomToTop:
                    fillingPos = (int)((this.Container.Y + this.Container.Height) - ((this.FillerPercentage / 100f) * this.Container.Height));
                    start = new Vector2(this.Container.Position.X + 1, fillingPos +1);
                    end = new Vector2(this.Container.Position.X + this.Container.Width - 1, this.Container.Position.Y + this.Container.Height - 1);
                    Primitives2D.Instance.FillRectangle(spriteBatch, start, end, this.FillerColor);
                    break;
            }
        }

        /// <summary>
        /// Direction of the filling
        /// </summary>
        public enum FillingDirection
        {
            LeftToRight,
            RightToLeft,
            TopToBottom,
            BottomToTop
        }
    }
}
