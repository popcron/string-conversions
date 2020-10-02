using System.Text;
using UnityEngine;

namespace Popcron.Conversion
{
    public sealed class BoundsConverter : Converter<Bounds>
    {
        private static StringBuilder builder = new StringBuilder();

        public override Bounds Convert(string value)
        {
            if (value.IndexOf('&') != -1)
            {
                string[] array = value.Split('&');
                Vector3 center = array[0].Convert<Vector3>();
                Vector3 size = array[1].Convert<Vector3>();
                return new Bounds(center, size);
            }

            return default;
        }

        public override string ToString(Bounds bounds)
        {
            builder.Clear();
            builder.Append(global::Conversion.ToString(bounds.center));
            builder.Append('&');
            builder.Append(global::Conversion.ToString(bounds.size));
            return builder.ToString();
        }

        public override bool IsCompatible(string value)
        {
            return value.IndexOf('&') != -1;
        }
    }
}