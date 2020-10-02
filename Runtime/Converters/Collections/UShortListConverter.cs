using System.Collections.Generic;
using System.Text;

namespace Popcron.Conversion
{
    public class UShortListConverter : Converter<List<ushort>>
    {
        private static StringBuilder builder = new StringBuilder();

        public override List<ushort> Convert(string value)
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
                    else
                    {
                        //comma dont exist, look for a space
                        if (value.IndexOf(' ') != -1)
                        {
                            delim = ' ';
                        }
                    }
                }

                if (value.IndexOf(delim) != -1)
                {
                    string[] splits = value.Split(CollectionDelimeter);
                    List<ushort> list = new List<ushort>(splits.Length);
                    for (int i = 0; i < splits.Length; i++)
                    {
                        list.Add(splits[i].Convert<ushort>());
                    }

                    return list;
                }
                else
                {
                    return new List<ushort>()
                    {
                        value.Convert<ushort>()
                    };
                }
            }

            return new List<ushort>();
        }

        public override string ToString(List<ushort> value)
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