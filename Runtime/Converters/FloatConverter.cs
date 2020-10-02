using System.Globalization;

namespace Popcron.Conversion
{
    public sealed class FloatConverter : Converter<float>
    {
        public override float Convert(string value)
        {
            return (float)ParseDecimal(value);
        }

        public override string ToString(float value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public override bool IsCompatible(string value)
        {
            return IsDecimal(value);
        }
    }
}