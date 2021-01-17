using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wpf_DietTracking
{
    public class MyStorage
    {
        public static void WriteXml<T>(T data, string file)
        {
            XmlSerializer sr = new XmlSerializer(typeof(T)); //
            FileStream stream = new FileStream(file, FileMode.Create);
            sr.Serialize(stream, data);
            stream.Close();
        }

        public static T ReadXml<T>(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(sr);
                }
            }
            catch (Exception)
            {

                return default(T);
            }
        }
    }
}
