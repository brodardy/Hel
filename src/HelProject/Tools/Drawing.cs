using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.Tools
{
    public class Drawing
    {
        private static Drawing _instance;
        private Texture2D _t; //base for the line texture

        public static Drawing Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Drawing();
                return _instance;
            }
        }

        /// <summary>
        /// Constructor of Drawing
        /// </summary>
        private Drawing()
        {
            _t = new Texture2D(MainGame.Instance.GraphicsDevice, 1, 1);
            _t.SetData<Color>(new Color[] { Color.White });// fill the texture with white
        }

        public void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, Color color)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(_t,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    50), //width of line, change this to make thicker line
                null,
                color, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
    }
}
