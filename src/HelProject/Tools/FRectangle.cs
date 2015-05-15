/*
 * Author : Yannick R. Brodard
 * File name : FRectangle.cs
 * Version : 0.1.201505130900
 * Description : Rectangle with float parameters
 */

using HelProject.GameWorld.Map;
using Microsoft.Xna.Framework;

namespace HelProject.Tools
{
    /// <summary>
    /// Rectangle with float parameters
    /// </summary>
    public class FRectangle
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        /// <summary>
        /// Creates a rectangle with float parameters
        /// </summary>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public FRectangle(float width, float height) : this(0f, 0f, width, height) { /* no code... */ }

        /// <summary>
        /// Creates a rectangle with float parameters
        /// </summary>
        /// <param name="x">X position of the rectangle</param>
        /// <param name="y">Y position of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public FRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Top position
        /// </summary>
        public float Top
        {
            get { return Y; }
        }

        /// <summary>
        /// Bottom position
        /// </summary>
        public float Bottom
        {
            get { return Y + Height; }
        }

        /// <summary>
        /// Left position
        /// </summary>
        public float Left
        {
            get { return X; }
        }

        /// <summary>
        /// Right position
        /// </summary>
        public float Right
        {
            get { return X + Width; }
        }

        /// <summary>
        /// Position of the rectangle
        /// </summary>
        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }

        /// <summary>
        /// Finds if this rectangle is intersecting with another
        /// </summary>
        /// <param name="rectangle">Other rectangle tested</param>
        /// <returns>Results if it's intersecting</returns>
        /// <see cref="http://stackoverflow.com/questions/13390333/two-rectangles-intersection"/>
        public bool Intersects(FRectangle rectangle)
        {
            float X = this.X;
            float Y = this.Y;
            float A = this.Width + X;
            float B = this.Height + Y;
            float X1 = rectangle.X;
            float Y1 = rectangle.Y;
            float A1 = rectangle.Width + X1;
            float B1 = rectangle.Height + Y1;

            if (A < X1 || A1 < X || B < Y1 || B1 < Y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Sets the position of the bounds accordingly to the given position
        /// </summary>
        public void SetBounds(Vector2 position, int textureWidth, int textureHeight)
        {
            this.X = position.X - (float)textureWidth / 2f / (float)HCell.TILE_SIZE;
            this.Y = position.Y - (float)textureHeight / 2f / (float)HCell.TILE_SIZE;
        }
    }
}
