using System;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace Popcron.Conversion
{
    public sealed class Vector3Converter : Converter<Vector3>
    {
        private static StringBuilder builder = new StringBuilder();

        public override Vector3 Convert(string value)
        {
            string[] array = value.Replace(" ", "").Replace("(", "").Replace(")", "").Split(',');
            if (array.Length <= 2)
            {
                return default;
            }

            float x = array[0].Convert<float>();
            float y = array[1].Convert<float>();
            float z = array.Length >= 3 ? array[2].Convert<float>() : 0;
            return new Vector3(x, y, z);
        }

        public override string ToString(Vector3 value)
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
            int start = value.IndexOf(',');
            int end = value.IndexOf(',');
            return start + end != -2;
        }
    }
}