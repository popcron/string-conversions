namespace Popcron.Conversion
{
    public sealed class UShortConverter : Converter<ushort>
    {
        public override ushort Convert(string value)
        {
            return (ushort)ParseUnsignedDigit(value);
        }

        public override string ToString(ushort value)
        {
            return value.ToString();
        }

        public override bool IsCompatible(string value)
        {
            return IsDigit(value);
        }
    }
}