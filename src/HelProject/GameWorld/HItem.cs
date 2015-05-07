/*
 * Author : Yannick R. Brodard
 * File name : HItem.cs
 * Version : 0.1.201505071037
 * Description : Class for the items
 */

using HelProject.Features;

namespace HelProject.GameWorld
{
    /// <summary>
    /// Item class
    /// </summary>
    public class HItem
    {
        private const bool DEFAULT_ISONFLOOR_VALUE = true;

        private string _name;
        private ItemTypes _itemType;
        private bool _isOnFloor;
        private FeatureCollection _features;

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
        /// Creates an item on the floor
        /// </summary>
        /// <param name="name">Name of the item</param>
        /// <param name="type">Type of the item</param>
        /// <param name="features">Given features of the item</param>
        /// <remarks>
        /// IsOnFloor == true
        /// </remarks>
        public HItem(string name, ItemTypes type, FeatureCollection features) : this(name, type, features, DEFAULT_ISONFLOOR_VALUE) { /* no code... */ }

        /// <summary>
        /// Creates an item
        /// </summary>
        /// <param name="name">Name of the item</param>
        /// <param name="type">Type of the item</param>
        /// <param name="features">Given features of the item</param>
        /// <param name="isOnFloor">Is the item on the floor</param>
        public HItem(string name, ItemTypes type, FeatureCollection features, bool isOnFloor)
        {
            this.Name = name;
            this.ItemType = type;
            this.Features = features;
            this.IsOnFloor = isOnFloor;
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
