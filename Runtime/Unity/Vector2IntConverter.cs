using System;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace Popcron.Conversion
{
    public sealed class Vector2IntConverter : Converter<Vector2Int>
    {
        private static StringBuilder builder = new StringBuilder();

        public override Vector2Int Convert(string value)
        {
            string[] array = value.Replace(" ", "").Replace("(", "").Replace(")", "").Split(',');
            if (array.Length <= 1)
            {
                return default;
            }

            int x = array[0].Convert<int>();
            int y = array[1].Convert<int>();
            return new Vector2Int(x, y);
        }

        public override string ToString(Vector2Int value)
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
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (value.IndexOf('.') != -1)
            {
                return false;
            }

            return value.IndexOf(',') != -1;
        }
    }
}