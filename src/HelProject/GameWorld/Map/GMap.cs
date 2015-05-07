using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using HelProject.UI;
using Microsoft.Xna.Framework;
using HelProject.GameWorld.Entities;
using HelProject.Tools;

namespace HelProject.GameWorld.Map
{
    public class GMap : HMap
    {
        private Texture2D _floor;
        private Texture2D _wall;
        private ContentManager _content;
        private float _scale;
        private HEntity _hero;

        public HEntity Hero
        {
            get { return _hero; }
            set { _hero = value; }
        }

        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public GMap(float zoom, int height, int width, int smoothness = HMap.DEFAULT_NONWALKABLE_CELLS_PERCENTAGE)
            : base(height, width, smoothness)
        {
            this.Scale = zoom;
            //this.Hero = new HEntity();
        }

        public void LoadContent()
        {
            this._content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            _floor = this._content.Load<Texture2D>("scenary/floor");
            _wall = this._content.Load<Texture2D>("scenary/wall");
        }

        public void UnloadContent()
        {
            this._content.Unload();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int sizeOfSprites = this._floor.Height;
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    HCell cell = this.GetCell(x, y);
                    Vector2 position = new Vector2(cell.Position.X * sizeOfSprites * this.Scale, cell.Position.Y * sizeOfSprites * this.Scale);
                    if (cell.IsWalkable)
                    {
                        spriteBatch.Draw(_floor, position, null, null, null, 0.0f, new Vector2(this.Scale, this.Scale), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(_wall, position, null, null, null, 0.0f, new Vector2(this.Scale, this.Scale), Color.White);
                    }
                }
            }
        }
    }
}
