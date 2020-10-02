using System;
using System.Collections.Generic;

namespace Popcron.Conversion
{
    public sealed class SByteConverter : Converter<sbyte>
    {
        private static Dictionary<string, sbyte> sbyteCache = null;

        private static Dictionary<string, sbyte> Table
        {
            get
            {
                if (sbyteCache == null)
                {
                    sbyteCache = new Dictionary<string, sbyte>();
                    for (int i = sbyte.MinValue; i <= sbyte.MaxValue; i++)
                    {
                        sbyteCache[i.ToString()] = (sbyte)i;
                    }
                }

                return sbyteCache;
            }
        }

        public override sbyte Convert(string value)
        {
            if (Table.TryGetValue(value, out sbyte result))
            {
                return result;
            }
            else
            {
                if (IsDigit(value))
                {
                    throw new OverflowException("Value was either too large or too small for an signed byte.");
                }
                else
                {
                    throw new InvalidCastException($"Could not convert {value} to an sbyte.");
                }
            }
        }

        public override string ToString(sbyte value)
        {
            return value.ToString();
        }

        public override bool IsCompatible(string value)
        {
            return Table.ContainsKey(value);
        }
    }
}