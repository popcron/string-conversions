namespace Popcron.Conversion
{
    public sealed class CharConverter : Converter<char>
    {
        public override char Convert(string value)
        {
            return value[0];
        }

        public override bool IsCompatible(string value)
        {
            return value.Length == 1;
        }
    }
}