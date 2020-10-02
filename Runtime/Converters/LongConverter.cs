namespace Popcron.Conversion
{
    public sealed class LongConverter : Converter<long>
    {
        public override long Convert(string value)
        {
            return ParseDigit(value);
        }

        public override string ToString(long value)
        {
            return value.ToString();
        }

        public override bool IsCompatible(string value)
        {
            return IsDigit(value);
        }
    }
}