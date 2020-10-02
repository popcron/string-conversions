using System.Collections.Generic;
using System.Linq;

namespace Popcron.Conversion
{
    public class UShortArrayConverter : Converter<ushort[]>
    {
        public override ushort[] Convert(string value)
        {
            return value.Convert<List<ushort>>().ToArray();
        }

        public override string ToString(ushort[] value)
        {
            return global::Conversion.ToString(value.ToList());
        }
    }
}