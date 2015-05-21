/*
 * Author : Yannick R. Brodard
 * File name : HItem.cs
 * Version : 0.1.201505071037
 * Description : Class for the items
 */

using HelProject.Features;
using HelProject.Tools;
using HelProject.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelProject.GameWorld
{
    /// <summary>
    /// Item class
    /// </summary>
    public class HItem : HObject
    {
        private const bool DEFAULT_ISONFLOOR_VALUE = true;

        private string _name;
        private ItemTypes _itemType;
        private bool _isOnFloor;
        private FeatureCollection _features;
        private string _imageName;
        private string _description;

        /// <summary>
        /// description or summary, or story, just additional content for the eyes
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Name of the related texture2D
        /// </summary>
        public string ImageName
        {
            get { return _imageName; }
            set { _imageName = value; }
        }

        /// <summary>
        /// Name of the item
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Type of the item
        /// </summary>
        public ItemTypes ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }

        /// <summary>
        /// Is the item on the floor
        /// </summary>
        public bool IsOnFloor
        {
            get { return _isOnFloor; }
            set { _isOnFloor = value; }
        }

        /// <summary>
        /// Given features of the item
        /// </summary>
        public FeatureCollection Features
        {
            get { return _features; }
            set { _features = value; }
        }

        /// <summary>
        /// Creates an empty item
        /// </summary>
        public HItem() : this("DEFAULT_ITEM", ItemTypes.Sword, new FeatureCollection(), "cursor_normal", false, Vector2.Zero, string.Empty) { /* no code... */ }

        /// <summary>
        /// Creates an item on the floor
        /// </summary>
        /// <param name="name">Name of the item</param>
        /// <param name="type">Type of the item</param>
        /// <param name="features">Given features of the item</param>
        /// <param name="imageName">Image name</param>
        /// <remarks>
        /// IsOnFloor == true
        /// </remarks>
        public HItem(string name, ItemTypes type, FeatureCollection features, string imageName) : this(name, type, features, imageName, DEFAULT_ISONFLOOR_VALUE, Vector2.Zero, string.Empty) { /* no code... */ }

        /// <summary>
        /// Creates an item
        /// </summary>
        /// <param name="name">Name of the item</param>
        /// <param name="type">Type of the item</param>
        /// <param name="features">Given features of the item</param>
        /// <param name="isOnFloor">Is the item on the floor</param>
        /// <param name="imageName">Image name</param>
        /// <param name="position">Position of the item (IG unit)</param>
        public HItem(string name, ItemTypes type, FeatureCollection features, string imageName, bool isOnFloor, Vector2 position) : this(name, type, features, imageName, isOnFloor, position, string.Empty) { /* no code... */ }

        /// <summary>
        /// Creates an item
        /// </summary>
        /// <param name="name">Name of the item</param>
        /// <param name="type">Type of the item</param>
        /// <param name="features">Given features of the item</param>
        /// <param name="isOnFloor">Is the item on the floor</param>
        /// <param name="imageName">Image name</param>
        /// <param name="position">Position of the item (IG unit)</param>
        /// <param name="description">Summary of the weapon</param>
        public HItem(string name, ItemTypes type, FeatureCollection features, string imageName, bool isOnFloor, Vector2 position, string description)
        {
            this.Name = name;
            this.ItemType = type;
            this.Features = features;
            this.IsOnFloor = isOnFloor;
            this.ImageName = imageName;
            this.Position = position;
            this.IsWalkable = true;
            this.Description = description;
        }
        /// <summary>
        /// Draws the item
        /// </summary>
        /// <param name="spriteBatch">Sprite batch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Vector2 position = ScreenManager.Instance.GetCorrectScreenPosition(this.Position, PlayScreen.Instance.Camera.Position);
            spriteBatch.Draw(TextureManager.Instance.GetTexture(this.ImageName), position, null, null, null, 0.0f, new Vector2(1f, 1f), Color.White);
        }

        /// <summary>
        /// Item types
        /// </summary>
        /// <remarks>
        /// Weapons : 0 to 6,
        /// Accessories : 7 to 11,
        /// Armors : 12 to 17
        /// </remarks>
        public enum ItemTypes
        {
            // WEAPONS : 0 -> 6
            Sword = 0,
            TwoHandedSword = 1,
            Axe = 2,
            TwoHandedAxe = 3,
            Wand = 4,
            Staff = 5,
            Bow = 6,

            // ACCESSORIES : 7 -> 11
            Amulet = 7,
            Ring = 8,
            Shield = 9,
            Quiver = 10,
            PowerSource = 11,

            // ARMORS : 12 -> 17
            Head = 12,
            Shoulders = 13,
            Body = 14,
            Hands = 15,
            Legs = 16,
            Feet = 17
        }
    }
}
