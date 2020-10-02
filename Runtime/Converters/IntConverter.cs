namespace Popcron.Conversion
{
    public sealed class IntConverter : Converter<int>
    {
        public override int Convert(string value)
        {
            return (int)ParseDigit(value);
        }

        public override string ToString(int value)
        {
            return value.ToString();
        }

        public override bool IsCompatible(string value)
        {
            return IsDigit(value);
        }
    }
}