namespace Popcron.Conversion
{
    public sealed class ShortConverter : Converter<short>
    {
        public override short Convert(string value)
        {
            return (short)ParseDigit(value);
        }

        public override bool IsCompatible(string value)
        {
            return IsDigit(value);
        }
    }
}