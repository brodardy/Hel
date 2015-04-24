using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HelProject.UI
{
    public class Menu
    {
        public event EventHandler OnMenuChange;

        public string Axis;
        public string Effects;
        [XmlElement("Item")]
        public List<MenuItem> Items;
        int itemNumber;
        string id;

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnMenuChange(this, null);
            }
        }

        void AlignMenuItems()
        {

        }

        public Menu()
        {
            id = String.Empty;
            itemNumber = 0;
            Effects = String.Empty;
            Axis = "Y";
        }

        public void LoadContent() { }
        public void UnloadContent() { }
        public void Update(GameTime gameTime) { }
        public void Draw(SpriteBatch spriteBatch) { }
    }
}
