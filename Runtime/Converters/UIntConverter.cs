namespace Popcron.Conversion
{
    public sealed class UIntConverter : Converter<uint>
    {
        public override uint Convert(string value)
        {
            return (uint)ParseUnsignedDigit(value);
        }

        public override string ToString(uint value)
        {
            return value.ToString();
        }

        public override bool IsCompatible(string value)
        {
            return IsDigit(value);
        }
    }
}