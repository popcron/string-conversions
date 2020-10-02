using System;
using System.Collections.Generic;
using System.Reflection;
using Popcron.Conversion;
using UnityEngine;

public static class Conversion
{
    private static Dictionary<Type, Converter> dictionary = null;
    private static Dictionary<object, string> enumToString = new Dictionary<object, string>();

    private static void CreateTypeTable()
    {
        //add default converters
        dictionary = new Dictionary<Type, Converter>
        {
            { typeof(string), new StringConverter() },
            { typeof(sbyte), new SByteConverter() },
            { typeof(byte), new ByteConverter() },
            { typeof(short), new ShortConverter() },
            { typeof(ushort), new UShortConverter() },
            { typeof(int), new IntConverter() },
            { typeof(uint), new UIntConverter() },
            { typeof(long), new LongConverter() },
            { typeof(ulong), new ULongConverter() },
            { typeof(float), new FloatConverter() },
            { typeof(double), new DoubleConverter() },
            { typeof(bool), new BoolConverter() }
        };

        //then find any extras in all assemblies
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        for (int a = 0; a < assemblies.Length; a++)
        {
            Assembly assembly = assemblies[a];
            Type[] types = assembly.GetTypes();
            for (int t = 0; t < types.Length; t++)
            {
                Type type = types[t];
                if (!type.IsAbstract)
                {
                    if (type.IsSubclassOf(typeof(Converter)))
                    {
                        Converter c = (Converter)Activator.CreateInstance(type);
                        if (dictionary.ContainsKey(c.Type))
                        {
                            continue;
                        }

                        dictionary.Add(c.Type, c);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Returns the appropriate string value that represents this object.
    /// </summary>
    public static string ToString(object value)
    {
        if (value != null)
        {
            Type type = value.GetType();
            if (type.IsEnum)
            {
                if (enumToString.TryGetValue(value, out string enumString))
                {
                    return enumString;
                }
                else
                {
                    enumString = value?.ToString();
                    enumToString[value] = enumString;
                    return enumString;
                }
            }
            else
            {
                if (dictionary == null)
                {
                    CreateTypeTable();
                }

                if (dictionary.TryGetValue(type, out Converter converter))
                {
                    string result = converter.ToString(value);
                    if (result != Converter.NullString)
                    {
                        return result;
                    }
                }
                else
                {
                    Debug.LogError($"[conversion] no converter available for {type}");
                }

                return value.ToString();
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Returns true if a converter exists for this type.
    /// </summary>
    public static bool ConverterExists(Type type)
    {
        if (dictionary == null)
        {
            CreateTypeTable();
        }

        return dictionary.ContainsKey(type);
    }

    /// <summary>
    /// Checks if this string is compatible with another type. If a converter isnt available for the type, it will assume that it's not compatible.
    /// This doesn't necessarily mean that the conversion wont happen however.
    /// </summary>
    public static bool IsCompatible(this string value, Type type)
    {
        if (dictionary == null)
        {
            CreateTypeTable();
        }

        if (dictionary.TryGetValue(type, out Converter converter))
        {
            return converter.IsCompatible(value);
        }
        else
        {
            return false;
        }
    }

    public static T Convert<T>(this string value)
    {
        object obj = value.Convert(typeof(T));
        try
        {
            return (T)obj;
        }
        catch
        {
            Debug.LogError($"[conversion] could not convert {obj} from {value} to type {typeof(T)}");
            return default;
        }
    }

    public static object Convert(this string s, Type type)
    {
        if (type == typeof(string))
        {
            return s;
        }

        if (type.IsEnum)
        {
            try
            {
                return Enum.Parse(type, s, true);
            }
            catch
            {
                Debug.LogError($"[conversion] could not convert {s} to enum of type {type}");
                return default;
            }
        }
        else
        {
            if (dictionary == null)
            {
                CreateTypeTable();
            }

            if (dictionary.TryGetValue(type, out Converter converter))
            {
                object v = converter.ConvertToObject(s);
                return v;
            }
            else
            {
                Debug.LogError($"[conversion] no converter available for {type}");
                return default;
            }
        }
    }
}