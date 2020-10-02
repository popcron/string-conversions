using System.Collections.Generic;
using System.Text;

namespace Popcron.Conversion
{
    public class StringListConverter : Converter<List<string>>
    {
        private static StringBuilder builder = new StringBuilder();

        public override List<string> Convert(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                char delim = CollectionDelimeter;
                if (value.IndexOf(delim) == -1)
                {
                    //square dont thou existseth'
                    //so try a comma
                    if (value.IndexOf(',') != -1)
                    {
                        delim = ',';
                    }
                }

                if (value.IndexOf(delim) != -1)
                {
                    string[] splits = value.Split(delim);
                    List<string> list = new List<string>(splits.Length);
                    for (int i = 0; i < splits.Length; i++)
                    {
                        list.Add(splits[i].Convert<string>());
                    }

                    return list;
                }
                else
                {
                    return new List<string>()
                    {
                        value.Convert<string>()
                    };
                }
            }

            return new List<string>();
        }

        public override string ToString(List<string> value)
        {
            builder.Clear();
            for (int i = 0; i < value.Count; i++)
            {
                builder.Append(global::Conversion.ToString(value[i]));
                if (i < value.Count - 1)
                {
                    builder.Append(',');
                }
            }

            return builder.ToString();
        }
    }
}