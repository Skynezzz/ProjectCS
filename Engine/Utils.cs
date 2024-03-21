using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Utils
{
    public class Utils
    {
        public static string GetTextFromFile(string path)
        {
            string returnString = "";
            using (StreamReader contentFile = new StreamReader("../../../" + path))
            {
                returnString = contentFile.ReadToEnd();
            }
            return returnString;
        }

        public static Dictionary<string, List<string>> GetDictFromFile(string path)
        {
            Dictionary<string, List<string>> returnDict = new Dictionary<string, List<string>>();

            string textFromFile = GetTextFromFile(path);

            string[] rows = textFromFile.Split('\n');

            foreach (string row in rows)
            {
                string[] keyValue = row.Split(':');

                string key = keyValue[0];
                string[] listValue = keyValue[1].Split(',');

                List<string> value = new List<string>();
                foreach (string valueString in listValue)
                {
                    value.Add(valueString);
                }

                returnDict[key] = value;
            }
            return returnDict;
        }
    }
}
