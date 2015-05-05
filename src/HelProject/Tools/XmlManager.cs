/*
 * Author : Yannick R. Brodard
 * File name : XmlManager.cs
 * Version : 0.1.201504241035
 * Description : Used to manage the xml fils for the classes
 */

#region USING STATEMENTS
using System;
using System.IO;
using System.Xml.Serialization;
#endregion

namespace HelProject.Tools
{
    public class XmlManager<T>
    {
        private Type _type;

        /// <summary>
        /// Type of the class that the XmlManager represents
        /// </summary>
        public Type TypeClass
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Loads a XML
        /// </summary>
        /// <param name="path">path of the xml</param>
        /// <returns>Object of the xml</returns>
        public T Load(string path)
        {
            T instance;
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(TypeClass);
                instance = (T)xml.Deserialize(reader);
            }
            return instance;
        }

        /// <summary>
        /// Saves an XML
        /// </summary>
        /// <param name="path">path of the xml file</param>
        /// <param name="obj">object to serialize</param>
        public void Save(string path, object obj)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer xml = new XmlSerializer(TypeClass);
                xml.Serialize(writer, obj);
            }
        }
    }
}
