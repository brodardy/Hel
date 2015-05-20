using HelProject.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.UI
{
    public class SelectionAid
    {
        private List<HObject> _selectedObjects;
        private PlayScreen playScreen;

        /// <summary>
        /// Selected objects
        /// </summary>
        public List<HObject> SelectedObjects
        {
            get { return _selectedObjects; }
            set { _selectedObjects = value; }
        }

        /// <summary>
        /// Creates a selection aider
        /// </summary>
        public SelectionAid()
        {
            this.SelectedObjects = new List<HObject>();
            playScreen = PlayScreen.Instance;
        }

        public void Update(GameTime gameTime)
        {
            this.SelectedObjects.Clear();

            int nbHostiles = playScreen.CurrentMap.Hostiles.Count;
            for (int i = 0; i < nbHostiles; i++)
            {
                if (playScreen.CurrentMap.Hostiles[i].Bounds.Intersects(playScreen.Camera.GetMousePositionRelativeToMap()))
                {
                    this.SelectedObjects.Add(playScreen.CurrentMap.Hostiles[i]);
                }
            }
        }
    }
}
