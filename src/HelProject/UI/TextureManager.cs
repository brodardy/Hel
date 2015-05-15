using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HelProject.UI
{
    /// <summary>
    /// Gets all the textures
    /// </summary>
    public class TextureManager
    {
        private static TextureManager _instance;
        private IDictionary<string, string> _texturesPaths;
        private IDictionary<string, Texture2D> _loadedTextures;

        private XmlSerializer _serializer;

        /// <summary>
        /// Path of all the textures
        /// </summary>
        /// <remarks>
        /// key = name of the texture
        /// value = path to texture
        /// </remarks>
        public IDictionary<string, string> TexturesPaths
        {
            get { return _texturesPaths; }
            set { _texturesPaths = value; }
        }

        /// <summary>
        /// Loaded textures
        /// </summary>
        public IDictionary<string, Texture2D> LoadedTextures
        {
            get { return _loadedTextures; }
            set { _loadedTextures = value; }
        }

        /// <summary>
        /// Instance of the class
        /// </summary>
        public static TextureManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TextureManager();
                return _instance;
            }
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        private TextureManager()
        {
            this._texturesPaths = new Dictionary<string, string>();
            this._loadedTextures = new Dictionary<string, Texture2D>();
            this._serializer = new XmlSerializer(typeof(TemporaryDictionnaryItem[]), new XmlRootAttribute() { ElementName = "items" });
        }

        /// <summary>
        /// Loads all the textures paths from the given file
        /// </summary>
        /// <param name="path">Path of the initialization file (.xml)</param>
        public void Load(string path)
        {
            using (TextReader reader = new StreamReader(path))
            {
                this._texturesPaths = ((TemporaryDictionnaryItem[])this._serializer.Deserialize(reader)).ToDictionary(i => i.id, i => i.path);
            }

            foreach (KeyValuePair<string, string> entry in this._texturesPaths)
            {
                this._loadedTextures.Add(entry.Key, MainGame.Instance.Content.Load<Texture2D>(entry.Value));
            }

        }

        /// <summary>
        /// Save all the textures paths to the given location
        /// </summary>
        /// <param name="path">Path of the location/file</param>
        public void Save(string path)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                this._serializer.Serialize(writer, this._texturesPaths.Select(kv => new TemporaryDictionnaryItem() { id = kv.Key, path = kv.Value }).ToArray());
            }
        }

        /// <summary>
        /// Gets the texture by the key
        /// </summary>
        /// <param name="key"></param>
        public Texture2D GetTexture(string key)
        {
            Texture2D texture;

            try
            {
                texture = this.LoadedTextures[key];
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                texture = this.LoadedTextures["notexture"];
            }

            return texture;
        }
    }

    /// <summary>
    /// Class used for the serialization/deserialization of the TextureManager class
    /// </summary>
    public class TemporaryDictionnaryItem
    {
        [XmlAttribute]
        public string id;
        [XmlAttribute]
        public string path;
    }
}
