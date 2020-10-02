using System;

namespace Popcron.Conversion
{
    public sealed class BoolConverter : Converter<bool>
    {
        public override bool Convert(string value)
        {
            return value.Equals("true", StringComparison.OrdinalIgnoreCase) || value == "1";
        }

        public override string ToString(bool value)
        {
            return value ? "true" : "false";
        }

        public override bool IsCompatible(string value)
        {
            if (value.Equals("true", StringComparison.OrdinalIgnoreCase) || value == "1")
            {
                return true;
            }
            else if (value.Equals("false", StringComparison.OrdinalIgnoreCase) || value == "0")
            {
                return true;
            }

            return false;
        }
    }
}