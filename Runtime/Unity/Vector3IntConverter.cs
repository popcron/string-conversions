using System.Globalization;
using System.Text;
using UnityEngine;

namespace Popcron.Conversion
{
    public sealed class Vector3IntConverter : Converter<Vector3Int>
    {
        private static StringBuilder builder = new StringBuilder();

        public override Vector3Int Convert(string value)
        {
            string[] array = value.Replace(" ", "").Replace("(", "").Replace(")", "").Split(',');
            if (array.Length <= 1)
            {
                return default;
            }

            int x = array[0].Convert<int>();
            int y = array[1].Convert<int>();
            int z = array.Length >= 3 ? array[2].Convert<int>() : 0;
            return new Vector3Int(x, y, z);
        }

        public override string ToString(Vector3Int value)
        {
            builder.Clear();
            string x = value.x.ToString(CultureInfo.InvariantCulture);
            string y = value.y.ToString(CultureInfo.InvariantCulture);
            string z = value.z.ToString(CultureInfo.InvariantCulture);
            builder.Append(x);
            builder.Append(',');
            builder.Append(y);
            builder.Append(',');
            builder.Append(z);
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

            int start = value.IndexOf(',');
            int end = value.IndexOf(',');
            return start + end != -2;
        }
    }
}