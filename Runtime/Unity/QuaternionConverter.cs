using System.Globalization;
using System.Text;
using UnityEngine;

namespace Popcron.Conversion
{
    public sealed class QuaternionConverter : Converter<Quaternion>
    {
        private static StringBuilder builder = new StringBuilder();

        public override Quaternion Convert(string value)
        {
            string[] array = value.Replace(" ", "").Replace("(", "").Replace(")", "").Split(',');
            if (array.Length <= 3)
            {
                return default;
            }

            float x = array[0].Convert<float>();
            float y = array[1].Convert<float>();
            float z = array[2].Convert<float>();
            float w = array[3].Convert<float>();
            return new Quaternion(x, y, z, w);
        }

        public override string ToString(Quaternion value)
        {
            builder.Clear();
            string x = value.x.ToString(CultureInfo.InvariantCulture);
            string y = value.y.ToString(CultureInfo.InvariantCulture);
            string z = value.z.ToString(CultureInfo.InvariantCulture);
            string w = value.w.ToString(CultureInfo.InvariantCulture);
            builder.Append(x);
            builder.Append(',');
            builder.Append(y);
            builder.Append(',');
            builder.Append(z);
            builder.Append(',');
            builder.Append(w);
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