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
using Microsoft.Xna.Framework.Input;
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

        private Point _menuPosition;
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
        public Point MenuPosition
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
            this(new Point(DEFAULT_MENU_POS_X, DEFAULT_MENU_POS_Y), new List<MenuItem>()) { /* no code... */ }

        /// <summary>
        /// Creates a menu
        /// </summary>
        /// <param name="menuPosition">Position of the menu</param>
        /// <param name="menuItems">Items of the menu</param>
        public MenuScreen(Point menuPosition, List<MenuItem> menuItems)
            : this(menuPosition, menuItems, DEFAULT_HIGHLIGHT_INDEX) { /* no code... */ }

        public MenuScreen(Point menuPosition, List<MenuItem> menuItems, int highlightIndex)
        {
            this.MenuPosition = menuPosition;
            this.Items = menuItems;
            this.HighlightIndex = highlightIndex;
            this._selectionIndicator = new Image();
            this._selectionIndicator.ImagePath = "MenuScreen/selectorw";
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
            this.updateInputSelection(gameTime);
        }

        /// <summary>
        /// Draws the content of the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            this.BackgroundImage.Draw(spriteBatch);
            this.drawIndicator(spriteBatch);
        }

        /// <summary>
        /// Draws the indicator
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void drawIndicator(SpriteBatch spriteBatch)
        {
            int i = 0;
            foreach (MenuItem itm in this.Items)
            {
                if (i == this.HighlightIndex)
                {
                    int totalOffSet = ((itm.ItemImage.SourceRect.Width / 2) + (this._selectionIndicator.SourceRect.Width / 2) + 10);

                    this._selectionIndicator.Scale = new Vector2(1, 1);
                    this._selectionIndicator.Position = new Vector2(
                        itm.ItemImage.Position.X - totalOffSet, itm.ItemImage.Position.Y);
                    this._selectionIndicator.Draw(spriteBatch);

                    this._selectionIndicator.Scale = new Vector2(-1, -1);
                    this._selectionIndicator.Position = new Vector2(
                        itm.ItemImage.Position.X + totalOffSet, itm.ItemImage.Position.Y);
                    this._selectionIndicator.Draw(spriteBatch);
                }
                itm.ItemImage.Draw(spriteBatch);
                i++;
            }
        }

        /// <summary>
        ///  Input management
        /// </summary>
        /// <param name="gameTime"></param>
        private void updateInputSelection(GameTime gameTime)
        {
            int itemNbr = this.Items.Count;

            if (InputManager.Instance.IsKeyboardKeyDown(Keys.Down))
            {
                this.HighlightIndex++;
                if (this.HighlightIndex >= itemNbr)
                    this.HighlightIndex = 0;
            }

            if (InputManager.Instance.IsKeyboardKeyReleased(Keys.Up))
            {
                this.HighlightIndex--;
                if (this.HighlightIndex < 0)
                    this.HighlightIndex = itemNbr - 1;
            }

            Point mousePosition = InputManager.Instance.MsState.Position;
            bool enterKeyDown = InputManager.Instance.IsKeyboardKeyDown(Keys.Enter);
            // initialisation to only create variable ONCE
            int i = 0; // Index
            int posX = 0; // position of the item
            int posY = 0; // position of the item
            int offSetX = 0; // Offset from the center
            int offSetY = 0; // Offset from the center
            foreach (MenuItem itm in this.Items)
            {
                /* MOUSE SELECTION */
                posX = (int)itm.ItemImage.Position.X;
                posY = (int)itm.ItemImage.Position.Y;
                offSetX = itm.ItemImage.SourceRect.Width / 2;
                offSetY = itm.ItemImage.SourceRect.Height / 2;

                if ((mousePosition.Y > (posY - offSetY)) &&
                    (mousePosition.Y < (posY + offSetY)) &&
                    (mousePosition.X > (posX - offSetX)) &&
                    (mousePosition.X < (posX + offSetX)))
                {
                    this.HighlightIndex = i;
                }
                updateInputEntries(gameTime, itm, enterKeyDown);
                /* END MOUSE SELECTION */
                i++;
            }
        }

        /// <summary>
        ///  Input management
        /// </summary>
        /// <param name="gameTime"></param>
        private void updateInputEntries(GameTime gameTime, MenuItem itm, bool enter)
        {
            if (enter)
            {
                if (itm.Id == 1 && this.HighlightIndex == 0)
                {
                    this.UnloadContent();
                    GameScreen ps = PlayScreen.Instance;
                    ScreenManager.Instance.Transition(ps);
                }
                else if (itm.Id == 3 && this.HighlightIndex == 2)
                {
                    MainGame.Instance.Exit();
                }
            }
        }
    }
}
