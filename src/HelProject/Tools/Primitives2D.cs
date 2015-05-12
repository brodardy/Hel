using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelHelProject.Tools
{
    public static class Primitives2D
    {
        //Reference of the GraphicsDevice of the Main Game class.
        static GraphicsDevice graphicsDevice;

        //Contains the shader required to draw.
        static BasicEffect basicEffect;

        /// <summary>
        /// Initializes the class variables one time only for optimization.
        /// Call in Initialize or Load method of Game class.
        /// </summary>
        /// <param name="device">GraphicsDevice on which to draw.</param>
        public static void Initialize(GraphicsDevice device)
        {
            graphicsDevice = device;

            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
                (0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, 0, 0, 1);
        }

        /// <summary>
        /// Draws a line from <paramref name="vertex1"/> to <paramref name="vertex2"/> in the specified color.
        /// </summary>
        /// <param name="vertex1">First Vertex.</param>
        /// <param name="vertex2">Second Vertex.</param>
        /// <param name="color">Color of the line.</param>
        public static void DrawLine(Vector2 vertex1, Vector2 vertex2, Color color)
        {
            VertexPositionColor[] vertices = new VertexPositionColor[2];

            vertices[0].Position = new Vector3(vertex1, 0);
            vertices[0].Color = color;

            vertices[1].Position = new Vector3(vertex2, 0);
            vertices[1].Color = color;

            basicEffect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, vertices, 0, 1);
        }

        /// <summary>
        /// Draws a polygon with the specified vertices.
        /// </summary>
        /// <param name="verts">Array of the vertices. The value in the last index should be equal to the first index to close the polygon.</param>
        /// <param name="color">Color of the polygon.</param>
        public static void DrawPolygon(Vector2[] verts, Color color)
        {
            VertexPositionColor[] vertices = new VertexPositionColor[verts.Length + 1];

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].Position = new Vector3(verts[i], 0);
                vertices[i].Color = color;
            }

            vertices[verts.Length] = vertices[0];

            basicEffect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, vertices, 0, vertices.Length - 1);
        }

        /// <summary>
        /// Draws a circle with the specified position and radius.
        /// </summary>
        /// <param name="position">Position of the circle (ie. the center)</param>
        /// <param name="radius">Radius of the circle in pixels.</param>
        /// <param name="color">Color of the circle.</param>
        public static void DrawCircle(Vector2 position, float radius, Color color)
        {
            DrawEllipse(position, radius, radius, color);
        }

        /// <summary>
        /// Draws an ellipse with the specified width and the height.
        /// </summary>
        /// <param name="position">Position of the ellipse (ie. the center)</param>
        /// <param name="halfWidth">Half of the width of the ellipse.</param>
        /// <param name="halfHeight">Half of the height of the ellipse.</param>
        /// <param name="color">Color of the ellipse.</param>
        public static void DrawEllipse(Vector2 position, float halfWidth, float halfHeight, Color color)
        {
            //Need not be 60 only, can be as less as 30 but depends on visual requirement. 
            const byte segments = 60;

            VertexPositionColor[] vertices = new VertexPositionColor[segments + 1];

            for (int i = 0; i < segments; i++)
            {
                float angle = (float)(i / (double)segments * Math.PI * 2);
                vertices[i].Position = new Vector3(position.X + (float)Math.Cos(angle) * halfWidth, position.Y + (float)Math.Sin(angle) * halfHeight, 0);
                vertices[i].Color = color;
            }

            vertices[segments] = vertices[0];

            basicEffect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, vertices, 0, vertices.Length - 1);
        }
    }
}