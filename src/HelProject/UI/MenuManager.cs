using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelProject.UI
{
    public class MenuManager
    {
        Menu menu;

        public MenuManager() {
            menu = new Menu();
            menu.OnMenuChange += menu_OnMenuChange;
        }

        void menu_OnMenuChange(object sender, EventArgs e)
        {
            //XmlManager<Menu>
        }

        public void LoadContent(string menuPath)
        {

        }

        public void UnloadContent() { }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch) { }
    }
}
