using System;
using System.Text;

namespace Popcron.Conversion
{
    public abstract class Converter
    {
        /// <summary>
        /// The character to use when delimiting arrays or lists.
        /// </summary>
        public const char CollectionDelimeter = '■';
        public const char Positive = '+';
        public const char Negative = '-';
        public const char Decimal = '.';
        public const int CharOffset = '0';

        private static string nullString;

        /// <summary>
        /// A string that is considered null, used by the default implementation of the ToString method.
        /// </summary>
        public static string NullString
        {
            get
            {
                if (nullString == null)
                {
                    int length = 54;
                    Random random = new Random();
                    StringBuilder builder = new StringBuilder(length);
                    for (int i = 0; i < length; i++)
                    {
                        char character = (char)random.Next(char.MinValue, char.MaxValue);
                        builder.Append(character);
                    }

                    nullString = builder.ToString();
                }

                return nullString;
            }
        }

        public abstract Type Type { get; }

        public abstract object ConvertToObject(string value);
        public abstract string ToString(object obj);

        public virtual bool IsCompatible(string value)
        {
            return false;
        }
    }

    public abstract class Converter<T> : Converter
    {
        public sealed override Type Type => typeof(T);
        public abstract T Convert(string value);

        public override object ConvertToObject(string value) => Convert(value);
        public sealed override string ToString(object obj) => ToString((T)obj);

        public virtual string ToString(T value)
        {
            return value?.ToString();
        }

        /// <summary>
        /// Returns true if the value contains decimals.
        /// </summary>
        protected bool IsDecimal(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            else if (value.Length == 1 && IsCharSymbol(value[0]))
            {
                return false;
            }

            int decimalPlace = value.IndexOf(Decimal);
            for (int i = 0; i < value.Length; i++)
            {
                if (i == 0 && IsCharSign(value[0]))
                {
                    continue;
                }

                if (i == decimalPlace)
                {
                    continue;
                }

                if (!IsCharNumber(value[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Parses a positive or negative digit.
        /// </summary>
        protected double ParseDecimal(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            int delim = value.IndexOf(Decimal);
            int whole = 0;
            int deci = 0;
            bool negative = false;
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if (i == value.Length - 1)
                {
                    if (c == 'f' || c == 'l' || c == 'd')
                    {
                        continue;
                    }
                }

                if (i == 0 && IsCharSign(c))
                {
                    negative = c == Negative;
                    continue;
                }
                else if (i == delim)
                {
                    continue;
                }
                else if (i < delim || delim == -1)
                {
                    whole = whole * 10 + (c - CharOffset);
                }
                else
                {
                    deci = deci * 10 + (c - CharOffset);
                }
            }

            if (delim != -1)
            {
                int size = (value.Length - delim) - 1;
                int pow = 1;
                for (int i = 0; i < size; i++)
                {
                    pow *= 10;
                }

                double result = whole + deci / (double)pow;
                return negative ? -result : result;
            }
            else
            {
                return negative ? -whole : whole;
            }
        }

        /// <summary>
        /// Returns true if this a symbol that is allowed as part of a digit, but not a digit itself.
        /// </summary>
        protected bool IsCharSymbol(char c)
        {
            return c == Positive || c == Negative || c == Decimal;
        }

        /// <summary>
        /// Returns true if this is a character that represents signage.
        /// </summary>
        protected bool IsCharSign(char c)
        {
            return c == Positive || c == Negative;
        }

        /// <summary>
        /// Returns true if this character is a digit.
        /// </summary>
        protected bool IsCharNumber(char c)
        {
            switch (c)
            {
                case '0':
                    return true;
                case '1':
                    return true;
                case '2':
                    return true;
                case '3':
                    return true;
                case '4':
                    return true;
                case '5':
                    return true;
                case '6':
                    return true;
                case '7':
                    return true;
                case '8':
                    return true;
                case '9':
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Returns true if this string is a digit, with or without decimals.
        /// </summary>
        protected bool IsDigit(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (value.Length == 1 && IsCharSymbol(value[0]))
            {
                return false;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (i == 0 && IsCharSign(value[0]))
                {
                    continue;
                }

                if (!IsCharNumber(value[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Parses out a positive or negative digit as a long.
        /// </summary>
        protected long ParseDigit(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            long number = 0;
            bool negative = false;
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if (i == value.Length - 1)
                {
                    if (c == 'f' || c == 'l' || c == 'd')
                    {
                        continue;
                    }
                }

                if (i == 0 && IsCharSign(c))
                {
                    negative = c == Negative;
                    continue;
                }
                else if (c == Decimal)
                {
                    break;
                }
                else
                {
                    number = number * 10 + (c - CharOffset);
                }
            }

            return negative ? -number : number;
        }

        /// <summary>
        /// Parses out a positive only unsigned digit as a long.
        /// </summary>
        protected ulong ParseUnsignedDigit(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            ulong number = 0;
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if (i == value.Length - 1)
                {
                    if (c == 'f' || c == 'l' || c == 'd')
                    {
                        continue;
                    }
                }

                if (i == 0 && IsCharSign(c))
                {
                    continue;
                }
                else if (c == Decimal)
                {
                    break;
                }
                else
                {
                    number = number * 10 + (c - (uint)CharOffset);
                }
            }

            return number;
        }
    }
}