/*
 * Author : Yannick R. Brodard
 * File name : Camera.cs
 * Version : 0.1.201505120855
 * Description : Camera class for the screen
 */

using Microsoft.Xna.Framework;

namespace HelProject.UI
{
    /// <summary>
    /// Camera class for the screen
    /// </summary>
    public class Camera
    {
        private float _zoom;
        private Vector2 _position;
        private int _width;
        private int _height;

        /// <summary>
        /// Height of the camera
        /// </summary>
        /// <remarks>
        /// Usually the height of the window of the game
        /// </remarks>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// Width of the camera
        /// </summary>
        /// <remarks>
        /// Usually the width of the window of the game
        /// </remarks>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Gets the view port of the camera
        /// </summary>
        public Rectangle ViewPort
        {
            get
            {
                return new Rectangle((int)this.Position.X, (int)this.Position.Y,
                                     (int)this.Position.X + this.Width, (int)this.Position.Y + this.Height);
            }
        }

        /// <summary>
        /// Position of the camera, relative to the map
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Zoom effect
        /// </summary>
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; }
        }

        /// <summary>
        /// Create a camera
        /// </summary>
        /// <param name="position">Position of the camera</param>
        /// <param name="width">Width of the camera</param>
        /// <param name="height">Height of the camera</param>
        /// <param name="zoom">Zoom of the camera</param>
        public Camera(Vector2 position, int width, int height, float zoom = 1.0f)
        {
            this.Position = position;
            this.Width = width;
            this.Height = height;
            this.Zoom = zoom;
        }
    }
}
