using GenericSerialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteRead
{
    /// <summary>
    ///
    /// </summary>
    public class Settings
    {
        private bool readMessageToUser = false; 

        private ColorSelection[] colorSelection;

        public ColorSelection[] ColorSelection
        {
            get 
            { 
                return colorSelection;
            }
            set 
            {
                colorSelection = value; 
            }
        }

        public bool ReadMessageToUser
        { 
            get 
            { 
                return readMessageToUser;
            }
            set 
            {
                readMessageToUser = value;
            }
        }

        public void ColorSerialize(ColorSelection[] colors, XmlHelper xmlHelper)
        {
            try
            {
                for (int currentColor = 0; currentColor < colors.Length; currentColor++)
                {
                    colors[currentColor] = colors[currentColor];
                }

                xmlHelper.Serialize(colors,
                                    xmlHelper.DefaultFilePath);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void ColorDeserialize(ColorSelection[] colors, XmlHelper xmlHelper)
        {
            try
            {
                string output;

                xmlHelper.Serialize(colors,
                                    xmlHelper.DefaultFilePath);
                colors = xmlHelper.Deserialize<ColorSelection[]>(xmlHelper.DefaultFilePath);

                for (int currentColor = 0; currentColor < colors.Length; currentColor++)
                {
                    output = $$"""

                                Color {{currentColor}}:
                                R: {{colors[currentColor].red}}
                                G: {{colors[currentColor].green}}
                                B: {{colors[currentColor].blue}}
                                A: {{colors[currentColor].alpha}}
                                Name: {{colors[currentColor].name}}
                              """;
                    Console.WriteLine(output);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
