using System;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace Popcron.Conversion
{
    public sealed class Vector2Converter : Converter<Vector2>
    {
        private static StringBuilder builder = new StringBuilder();

        public override Vector2 Convert(string value)
        {
            string[] array = value.Replace(" ", "").Replace("(", "").Replace(")", "").Split(',');
            if (array.Length <= 1)
            {
                return default;
            }

            float x = array[0].Convert<float>();
            float y = array[1].Convert<float>();
            return new Vector2(x, y);
        }

        public override string ToString(Vector2 value)
        {
            builder.Clear();
            string x = value.x.ToString(CultureInfo.InvariantCulture);
            string y = value.y.ToString(CultureInfo.InvariantCulture);
            builder.Append(x);
            builder.Append(',');
            builder.Append(y);
            return builder.ToString();
        }

        public override bool IsCompatible(string value)
        {
            return value.IndexOf(',') != -1;
        }
    }
}