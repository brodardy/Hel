﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.Tools
{
    public class FRectangle
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public FRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public float Top
        {
            get { return Y; }
        }
        public float Bottom
        {
            get { return Y + Height; }
        }
        public float Left
        {
            get { return X; }
        }
        public float Right
        {
            get { return X + Width; }
        }

        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
        }
    }
}
