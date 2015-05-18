using HelProject.Features;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.GameWorld.Entities
{
    public class HHostile : HEntity
    {
        private float fieldOfView;

        /// <summary>
        /// Field of view of the hostile
        /// </summary>
        public float FieldOfView
        {
            get { return fieldOfView; }
            set { fieldOfView = value; }
        }


        public HHostile(FeatureCollection initialFeatures, Vector2 position, float width, float height, string textureName)
            : base(initialFeatures, position, width, height, textureName)
        {

        }

        /// <summary>
        /// Loads the content of the hostile
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
        }

        /// <summary>
        /// Unloads the content of the hostile
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Updates the mechanismes of the hostile
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the hostile
        /// </summary>
        /// <param name="spriteBatch">Sprite batch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
