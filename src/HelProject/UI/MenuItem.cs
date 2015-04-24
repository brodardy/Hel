using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.UI
{
    public class MenuItem
    {
        private string _linkType;
        private string _linkID;
        private Texture2D _image;

        /// <summary>
        /// Type of the link
        /// </summary>
        public string LinkType
        {
            get { return _linkType; }
            set { _linkType = value; }
        }

        /// <summary>
        /// ID of the link
        /// </summary>
        public string LinkID
        {
            get { return _linkID; }
            set { _linkID = value; }
        }

        /// <summary>
        /// Image of the link
        /// </summary>
        public Texture2D Image
        {
            get { return _image; }
            set { _image = value; }
        }
    }
}
