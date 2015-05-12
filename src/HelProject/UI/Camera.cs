using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.UI
{
    public class Camera
    {
        private float _zoom;
        private Vector2 _position;
        private int _width;
        private int _height;

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public Rectangle ViewPort
        {
            get
            {
                return new Rectangle((int)this.Position.X, (int)this.Position.Y,
                                     (int)this.Position.X + this.Width, (int)this.Position.Y + this.Height);
            }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; }
        }

        public Camera(Vector2 position, int width, int height, float zoom = 1.0f)
        {
            this.Position = position;
            this.Width = width;
            this.Height = height;
            this.Zoom = zoom;
        }
    }
}
