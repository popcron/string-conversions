using System.Globalization;

namespace Popcron.Conversion
{
    public sealed class DoubleConverter : Converter<double>
    {
        public override double Convert(string value)
        {
            return ParseDecimal(value);
        }

        public override string ToString(double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public override bool IsCompatible(string value)
        {
            return IsDecimal(value);
        }
    }
}