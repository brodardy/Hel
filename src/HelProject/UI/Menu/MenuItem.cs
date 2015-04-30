using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HelProject.UI.Menu
{
    public class MenuItem
    {
        private const LinkTypes DEFAULT_LINK_TYPE = LinkTypes.MENU;
        private const Image DEFAULT_IMAGE = null;

        private LinkTypes _linkType;
        private Image _itemImage;

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Image of the item
        /// </summary>
        [XmlElement("Image")]
        public Image ItemImage
        {
            get { return _itemImage; }
            set { _itemImage = value; }
        }

        /// <summary>
        /// Type of the link of the item
        /// </summary>
        //[XmlIgnore]
        public LinkTypes LinkType
        {
            get { return _linkType; }
            set { _linkType = value; }
        }

        /// <summary>
        /// Creates a menu item
        /// </summary>
        public MenuItem() : this(DEFAULT_LINK_TYPE, DEFAULT_IMAGE) { /* no code... */ }

        /// <summary>
        /// Creates a menu item
        /// </summary>
        /// <param name="linkType">Type of the link of the item</param>
        /// <param name="img">Image of the item</param>
        public MenuItem(LinkTypes linkType, Image img)
        {
            this.LinkType = linkType;
            this.ItemImage = img;
        }

        /// <summary>
        /// Types of links in the game
        /// </summary>
        public enum LinkTypes
        {
            [XmlEnum("0")]
            MENU = 0,
            [XmlEnum("1")]
            GAME = 1,
        }
    }
}
