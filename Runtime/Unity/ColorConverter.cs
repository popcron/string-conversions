using System.Globalization;
using System.Text;
using UnityEngine;

namespace Popcron.Conversion
{
    public sealed class ColorConverter : Converter<Color>
    {
        private static StringBuilder builder = new StringBuilder();

        public override Color Convert(string value)
        {
            if (ColorUtility.TryParseHtmlString(value, out Color color))
            {
                return color;
            }
            else
            {
                string[] array = value.Replace(" ", "").Replace("(", "").Replace(")", "").Split(',');
                if (array.Length <= 2)
                {
                    return default;
                }

                float r = array[0].Convert<float>();
                float g = array[1].Convert<float>();
                float b = array[2].Convert<float>();
                float a = array.Length >= 4 ? array[3].Convert<float>() : 1f;
                return new Color(r, g, b, a);
            }
        }

        public override string ToString(Color value)
        {
            builder.Clear();
            string r = value.r.ToString(CultureInfo.InvariantCulture);
            string g = value.g.ToString(CultureInfo.InvariantCulture);
            string b = value.b.ToString(CultureInfo.InvariantCulture);
            string a = value.a.ToString(CultureInfo.InvariantCulture);
            builder.Append(r);
            builder.Append(',');
            builder.Append(g);
            builder.Append(',');
            builder.Append(b);
            builder.Append(',');
            builder.Append(a);
            return builder.ToString();
        }

        public override bool IsCompatible(string value)
        {
            if (ColorUtility.TryParseHtmlString(value, out Color color))
            {
                return true;
            }
            else
            {
                int commas = 0;
                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i] == ',')
                    {
                        commas++;
                    }
                }

                return commas == 2 || commas == 3;
            }
        }
    }
}