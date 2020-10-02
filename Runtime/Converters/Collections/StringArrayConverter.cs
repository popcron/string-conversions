using System.Collections.Generic;
using System.Linq;

namespace Popcron.Conversion
{
    public class StringArrayConverter : Converter<string[]>
    {
        public override string[] Convert(string value)
        {
            List<string> list = value.Convert<List<string>>();
            return list.ToArray();
        }

        public override string ToString(string[] value)
        {
            return global::Conversion.ToString(value.ToList());
        }
    }
}