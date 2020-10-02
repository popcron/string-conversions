namespace Popcron.Conversion
{
    public sealed class ULongConverter : Converter<ulong>
    {
        public override ulong Convert(string value)
        {
            return ParseUnsignedDigit(value);
        }

        public override string ToString(ulong value)
        {
            return value.ToString();
        }

        public override bool IsCompatible(string value)
        {
            return IsDigit(value);
        }
    }
}