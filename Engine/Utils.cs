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

            GridCase[,]? returnSprite = new GridCase[rows.Length, rows[0].Split(')').Length - 1];

            ConsoleColor colorCase = ConsoleColor.Magenta;
            for (int i = 0; i < rows.Length; i++)
            {
                string[] values = rows[i].Split("(");
                for (int j = 1; j < values.Length; j++)
                {
                    string[] binom = values[j].Split(")");
                    char character = binom[1][0];
                    if (character == '\0') continue;
                    if (binom[0] != "") colorCase = ClosestConsoleColor(Color.FromArgb(int.Parse((binom[0]), System.Globalization.NumberStyles.HexNumber)));
                    GridCase gridCase = new GridCase();
                    gridCase.value = character;
                    gridCase.bgColor = ConsoleColor.White;
                    gridCase.fgColor = colorCase;
                    returnSprite[i, j - 1] = gridCase;
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
