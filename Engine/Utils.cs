using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Utils
{
    public class Utils
    {
        public static string? GetTextFromFile(string path)
        {
            string returnString = "";
            try
            {
                using (StreamReader contentFile = new StreamReader("../../../" + path))
                {
                    returnString = contentFile.ReadToEnd();
                }
            } catch
            {
                return null;
            }
            /*for (int i = 0; i < returnString.Length; i++)
            {
                if (returnString[i] == '\r')
                {
                    returnString = returnString.Remove(i, 1);
                }
            }*/
            return returnString;
        }

        public static List<string[]> GetListFromFile(string path)
        {
            List<string[]> returnList = new List<string[]>();

            string textFromFile = GetTextFromFile(path);

            string[] rows = textFromFile.Split('\n');

            foreach (string row in rows)
            {
                string[] rawList = row.Split(',');

                returnList.Add(rawList);
            }
            return returnList;
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

        public static GridCase[,]? GetSpriteFromFile(string? path)
        {
            if (path == null) return null;

            string? textFromFile = GetTextFromFile(path);
            if (textFromFile == null) return null;

            string[] rows = textFromFile.Split('\n');

            //int width = rows[0].Length - ((rows[0].Split('(').Length - 1) * 8) - 1;
            int width = rows[0].Length - ((rows[0].Split('(').Length - 1) * 4) - ((rows[0].Split('[').Length - 1) * 4) - 1;
            GridCase[,]? returnSprite = new GridCase[rows.Length, width];

            ConsoleColor colorCase = ConsoleColor.Black;
            ConsoleColor colorBg = ConsoleColor.Black;
            
            for (int i = 0; i < rows.Length; i++)
            {
                string row = rows[i];
                int backToPos = 0;
                for (int j = 0; j < row.Length; j++)
                {
                    if (j < row.Length && (char)row[j] == '\r') continue;
                    if ((char)row[j] == '(')
                    {
                        if ((char)row[j + 1] == ')')
                        {
                            j += 1;
                            backToPos += 2;
                            continue;
                        }

                        string colorCode = row.Substring(j + 1, 2);
                        colorCase = (ConsoleColor)int.Parse(colorCode);
                        
                        j += 4;
                        backToPos += 4;
                    }

                    if ((char)row[j] == '[')
                    {
                        if ((char)row[j + 1] == ']')
                        {
                            j += 1;
                            backToPos += 2;
                            continue;
                        }

                        string colorCode = row.Substring(j + 1, 2);
                        colorBg = (ConsoleColor)int.Parse(colorCode);

                        j += 4;
                        backToPos += 4;
                    }

                    GridCase gridCase = new GridCase();
                    gridCase.value = row[j];
                    gridCase.bgColor = colorBg;
                    gridCase.fgColor = colorCase;
                    returnSprite[i, j - backToPos] = gridCase;
                }
            }
            return returnSprite;
        }
        public static ConsoleColor ClosestConsoleColor(Color rgbColor)
        {
            byte r = rgbColor.R;
            byte g = rgbColor.G;
            byte b = rgbColor.B;
            ConsoleColor ret = 0;
            double rr = r, gg = g, bb = b, delta = double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                var n = Enum.GetName(typeof(ConsoleColor), cc);
                var c = System.Drawing.Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
                var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
                if (t == 0.0)
                    return cc;
                if (t < delta)
                {
                    delta = t;
                    ret = cc;
                }
            }
            return ret;
        }

    }
}
