using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using WriteRead;

namespace GenericSerialize
{
    public struct ColorSelection
    {
        public byte red;
        public byte green;
        public byte blue;
        public byte alpha;

        public string name;

        public ColorSelection(byte red, byte green, byte blue, byte alpha, string name)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;

            this.name = name;
        }
    }

    class Program
    {
        static void Main()
        {
            ColorSelection[] colors = new ColorSelection[3];

            for (byte currentColor = 0; currentColor < colors.Length; currentColor++)
            {
                switch (currentColor )
                {
                    case 0:
                        colors[currentColor] = new ColorSelection(currentColor,
                                                               currentColor,
                                                               currentColor,
                                                               currentColor,
                                                               "High Contrast");
                        break;
                    case 1:
                        colors[currentColor] = new ColorSelection(currentColor,
                                                               currentColor,
                                                               currentColor,
                                                               currentColor,
                                                               "Low Contrast");
                        break;
                    case 2:
                        colors[currentColor] = new ColorSelection(currentColor,
                                                               currentColor,
                                                               currentColor,
                                                               currentColor,
                                                               "Current Color");
                        break;
                }
            }

            XmlHelper xmlHelper = new XmlHelper();
            Settings settings = new Settings();

            settings.ColorSelection = colors;
            settings.ReadMessageToUser = true;
            xmlHelper.Serialize(settings, xmlHelper.DefaultFilePath);
            settings = xmlHelper.Deserialize<Settings>(xmlHelper.DefaultFilePath);

            for (int currentColor = 0; currentColor < settings.ColorSelection.Length; currentColor++)
            {
                Console.WriteLine(settings.ColorSelection[currentColor].name);
            }
            Console.WriteLine(settings.ReadMessageToUser);
        }
    }

    public class XmlHelper
    {
        private string defaultFilePath = @"C:\Users\elias.nimlandlind\Desktop\text.xml";

        public string DefaultFilePath
        {
            get
            {
                return defaultFilePath;
            }

            private set
            {
                defaultFilePath = value;
            }
        }

        /// <summary>
        /// Serializes the data in the object to the designated file path
        /// </summary>
        /// <typeparam name="T">Type of Object to serialize</typeparam>
        /// <param name="dataToSerialize">Data to serialize</param>
        /// <param name="filePath">FilePath for the XML file</param>
        
        public void Serialize<T>(T dataToSerialize, string filePath)
        {
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Create, FileAccess.Write))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    XmlTextWriter xmlTextWriter = new XmlTextWriter(stream,
                                                                    Encoding.Default);

                    xmlTextWriter.Formatting = Formatting.Indented;

                    xmlSerializer.Serialize(xmlTextWriter,
                                            dataToSerialize);
                    xmlTextWriter.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        /// <summary>
        /// Deserializes the data in the XML file into an object
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="filePath">FilePath to XML file</param>
        /// <returns>Object containing deserialized data</returns>
        public T Deserialize<T>(string filePath)
        {
            T? serializedData = default;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

                using (Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    serializedData = (T)xmlSerializer.Deserialize(stream);
                }

                return serializedData;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception} : The data that failed to deserialize {serializedData}");
                return serializedData;
            }
        }
    }
}
