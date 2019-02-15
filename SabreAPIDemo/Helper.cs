using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SabreAPIDemo
{
    public class Helper
    {
        /// <summary>
        /// 对象转xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public string ToXml<T>(T t)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8);
            writer.Formatting = System.Xml.Formatting.None;
            xs.Serialize(writer, t);

            stream.Position = 0;
            StringBuilder sb = new StringBuilder();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    sb.Append(line);
                }
                reader.Close();
            }
            writer.Close();
            return sb.ToString();
        }

        /// <summary>
        /// xml转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public T ToObj<T>(string xml)
        {
            T myObject;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(xml);
            myObject = (T)serializer.Deserialize(reader);
            reader.Close();
            return myObject;
        }
    }
}
