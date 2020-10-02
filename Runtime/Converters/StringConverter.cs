namespace Popcron.Conversion
{
    public sealed class StringConverter : Converter<string>
    {
        public override string Convert(string value)
        {
            return value;
        }

        public override bool IsCompatible(string value)
        {
            return value != null;
        }
    }
}