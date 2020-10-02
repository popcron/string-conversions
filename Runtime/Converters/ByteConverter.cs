using System;
using System.Collections.Generic;

namespace Popcron.Conversion
{
    public sealed class ByteConverter : Converter<byte>
    {
        private static Dictionary<string, byte> byteCache = null;

        private static Dictionary<string, byte> Table
        {
            get
            {
                if (byteCache == null)
                {
                    byteCache = new Dictionary<string, byte>();
                    for (int i = byte.MinValue; i < byte.MaxValue; i++)
                    {
                        byteCache[i.ToString()] = (byte)i;
                    }
                }

                return byteCache;
            }
        }

        public override byte Convert(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return byte.MaxValue;
            }

            if (Table.TryGetValue(value, out byte result))
            {
                return result;
            }
            else
            {
                if (IsDigit(value))
                {
                    throw new OverflowException("Value was either too large or too small for an unsigned byte.");
                }
                else
                {
                    throw new InvalidCastException($"Could not convert {value} to a byte.");
                }
            }
        }

        public override string ToString(byte value)
        {
            return value.ToString();
        }

        public override bool IsCompatible(string value)
        {
            return Table.ContainsKey(value);
        }
    }
}