/*
 * Author : Yannick R. Brodard
 * File name : MenuScreen.cs
 * Version : 0.1.201504241035
 * Description : Main menu screen of the game
 */

#region USING STATEMENTS
using HelProject.Tools;
using HelProject.UI.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
#endregion

namespace HelProject.UI.Menu
{
    /// <summary>
    /// Title screen of the game
    /// </summary>
    public class MenuScreen : GameScreen
    {
        private const int DEFAULT_MENU_POS_X = 0;
        private const int DEFAULT_MENU_POS_Y = 0;
        private const int DEFAULT_HIGHLIGHT_INDEX = 0;

        private Position _menuPosition;
        private List<MenuItem> _items;
        private int _highlightIndex;
        private Image _backgroundImage;
        private Image _selectionIndicator;

        /// <summary>
        /// Background image of the screen
        /// </summary>
        [XmlElement("Image")]
        public Image BackgroundImage
        {
            get { return _backgroundImage; }
            set { _backgroundImage = value; }
        }

        /// <summary>
        /// Index of the highlighted item of the menu
        /// </summary>
        public int HighlightIndex
        {
            get { return _highlightIndex; }
            set { _highlightIndex = value; }
        }

        /// <summary>
        /// Position of the menu
        /// </summary>
        public Position MenuPosition
        {
            get { return _menuPosition; }
            set { _menuPosition = value; }
        }

        /// <summary>
        /// Items of the menu
        /// </summary>
        public List<MenuItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        /// <summary>
        /// Creates an empty menu
        /// </summary>
        public MenuScreen() :
            this(new Position(DEFAULT_MENU_POS_X, DEFAULT_MENU_POS_Y), new List<MenuItem>()) { /* no code... */ }

        /// <summary>
        /// Creates a menu
        /// </summary>
        /// <param name="menuPosition">Position of the menu</param>
        /// <param name="menuItems">Items of the menu</param>
        public MenuScreen(Position menuPosition, List<MenuItem> menuItems)
            : this(menuPosition, menuItems, DEFAULT_HIGHLIGHT_INDEX) { /* no code... */ }

        public MenuScreen(Position menuPosition, List<MenuItem> menuItems, int highlightIndex)
        {
            this.MenuPosition = menuPosition;
            this.Items = menuItems;
            this.HighlightIndex = highlightIndex;
            this._selectionIndicator = new Image();
            this._selectionIndicator.ImagePath = "MenuScreen/strenght_mini";
        }

        /// <summary>
        /// Loads the content of the screen
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            this.BackgroundImage.LoadContent();
            this._selectionIndicator.LoadContent();
            foreach (MenuItem itm in this.Items)
            {
                itm.ItemImage.LoadContent();
            }
        }

        /// <summary>
        /// Unloads the content of the screen
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
            this.BackgroundImage.UnloadContent();
            this._selectionIndicator.UnloadContent();
            foreach (MenuItem itm in this.Items)
            {
                itm.ItemImage.UnloadContent();
            }
        }

        /// <summary>
        /// Updates the content of the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the content of the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            this.BackgroundImage.Draw(spriteBatch);

            int i = 0;
            foreach (MenuItem itm in this.Items)
            {
                if (i == this.HighlightIndex) {
                    this._selectionIndicator.Position = itm.ItemImage.Position;
                    this._selectionIndicator.Draw(spriteBatch);
                }
                itm.ItemImage.Draw(spriteBatch);
                i++;
            }
        }
    }
}
