using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HelProject;
using HelProject.Tools;

namespace HelHelProject.Tools
{
    public class Primitives2D
    {
        /* SINGLETON START */
        private static Primitives2D _instance;

        /// <summary>
        /// Instance of the class
        /// </summary>
        public static Primitives2D Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Primitives2D();
                return _instance;
            }
        }

        /// <summary>
        /// Creates a primitive 2D
        /// </summary>
        private Primitives2D() { /* no code... */ }
        /* SINGLETON END */
        private const int DEFAULT_THICKNESS = 1;
        private Texture2D _pixel;

        /// <summary>
        /// Loads the content of the primitives 2D
        /// </summary>
        public void LoadContent()
        {
            _pixel = new Texture2D(MainGame.Instance.GraphicsDevice, 1, 1);
            _pixel.SetData<Color>(new Color[] { Color.White });
        }

        /// <summary>
        /// Draws a line
        /// </summary>
        /// <param name="sb">Sprite batch</param>
        /// <param name="start">Start of the line</param>
        /// <param name="end">End of the line</param>
        /// <param name="color">Color</param>
        /// <param name="thickness">Thickness of the line</param>
        /// <see cref="http://gamedev.stackexchange.com/questions/44015/how-can-i-draw-a-simple-2d-line-in-xna-without-using-3d-primitives-and-shders"/>
        public void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, Color color, int thickness = DEFAULT_THICKNESS)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(this._pixel,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    thickness), //width of line, change this to make thicker line
                null,
                color, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);
        }

        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="sb">Sprite batch</param>
        /// <param name="start">Start point</param>
        /// <param name="end">End point</param>
        /// <param name="color">Color</param>
        /// <param name="thickness">Thickness of the edge of the rectangle</param>
        public void DrawRectangle(SpriteBatch sb, Vector2 start, Vector2 end, Color color, int thickness = DEFAULT_THICKNESS)
        {
            Vector2 pointA = start;
            Vector2 pointB = new Vector2(end.X, start.Y);
            Vector2 pointC = end;
            Vector2 pointD = new Vector2(start.X, end.Y);
            this.DrawLine(sb, pointA, pointB, color, thickness);
            this.DrawLine(sb, pointB, pointC, color, thickness);
            this.DrawLine(sb, pointC, pointD, color, thickness);
            this.DrawLine(sb, pointD, pointA, color, thickness);
        }

        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="sb">Sprite batch</param>
        /// <param name="rectangle">Rectangle to draw</param>
        /// <param name="color">Color</param>
        /// <param name="thickness">Thickness of the edge of the rectangle</param>
        public void DrawRectangle(SpriteBatch sb, FRectangle rectangle, Color color, int thickness = DEFAULT_THICKNESS)
        {
            Vector2 start = rectangle.Position;
            Vector2 end = new Vector2(rectangle.Position.X + rectangle.Width, rectangle.Position.Y + rectangle.Height);
            this.DrawRectangle(sb, start, end, color, thickness);
        }

        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="sb">Sprite batch</param>
        /// <param name="position">Position of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        /// <param name="color">Color</param>
        /// <param name="thickness">Thickness of the edge of the rectangle</param>
        public void DrawRectangle(SpriteBatch sb, Vector2 position, int width, int height, Color color, int thickness = DEFAULT_THICKNESS)
        {
            this.DrawRectangle(sb, position, new Vector2(width + position.X, height + position.Y), color, thickness);
        }

        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="sb">Sprite batch</param>
        /// <param name="x">X position of the rectangle</param>
        /// <param name="y">Y position of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        /// <param name="color">Color</param>
        /// <param name="thickness">Thickness of the edge of the rectangle</param>
        public void DrawRectangle(SpriteBatch sb, int x, int y, int width, int height, Color color, int thickness = DEFAULT_THICKNESS)
        {
            this.DrawRectangle(sb, new Vector2(x, y), new Vector2(width + x, height + y), color, thickness);
        }

        /// <summary>
        /// Fills a rectangle
        /// </summary>
        /// <param name="sb">Sprite batch</param>
        /// <param name="start">Start point</param>
        /// <param name="end">End point</param>
        /// <param name="color">Filling color</param>
        public void FillRectangle(SpriteBatch sb, Vector2 start, Vector2 end, Color color)
        {
            sb.Draw(this._pixel,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)(end.X - start.X), //sb will strech the texture to fill this rectangle
                    (int)(end.Y - start.Y)), //width of line, change this to make thicker line
                null,
                color, //colour of line
                0f,     //angle of line
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);
        }

        /// <summary>
        /// Fills a rectangle
        /// </summary>
        /// <param name="sb">Sprite batch</param>
        /// <param name="rectangle">Rectangle to fill</param>
        /// <param name="color">Filling color</param>
        public void FillRectangle(SpriteBatch sb, FRectangle rectangle, Color color)
        {
            Vector2 start = rectangle.Position;
            Vector2 end = new Vector2(rectangle.Position.X + rectangle.Width, rectangle.Position.Y + rectangle.Height);
            this.FillRectangle(sb, start, end, color);
        }

        /// <summary>
        /// Fills a rectangle
        /// </summary>
        /// <param name="sb">Sprite batch</param>
        /// <param name="position">Position of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        /// <param name="color">Filling color</param>
        public void FillRectangle(SpriteBatch sb, Vector2 position, int width, int height, Color color)
        {
            this.FillRectangle(sb, position, new Vector2(width + position.X, height + position.Y), color);
        }

        /// <summary>
        /// Fills a rectangle
        /// </summary>
        /// <param name="sb">Sprite batch</param>
        /// <param name="x">X position of the rectangle</param>
        /// <param name="y">Y position of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        /// <param name="color">Filling color</param>
        public void FillRectangle(SpriteBatch sb, int x, int y, int width, int height, Color color)
        {
            this.FillRectangle(sb, new Vector2(x, y), new Vector2(width + x, height + y), color);
        }
    }
}