using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MFrameWork
{
    public static partial class Utility
    {
        public static class Xml
        {
            /// <summary>
            /// ,逗号分割
            /// </summary>
            public static char[] SplitComma = new char[] { ',' };
            /// <summary>
            /// | 竖线分割
            /// </summary>
            public static char[] SplitVerticalBar = new char[] { '|' };
            /// <summary>
            /// ; 分号分割
            /// </summary>
            public static char[] SplitSemicolon = new char[] { ';' };
            /// <summary>
            /// : 冒号分割
            /// </summary>
            public static char[] SplitColon = new char[] { ':' };

            public static bool AttributeValid(System.Xml.XmlNode node, string attributeName)
            {
                if (node == null || attributeName == null || attributeName.Length <= 0)
                    return false;

                if (node.Attributes[attributeName] == null)
                    return false;

                return true;
            }

            public static void AssignAttribute<T>(ref T value, System.Xml.XmlNode node, string attributeName)
            {
                if (!AttributeValid(node, attributeName))
                    return;

                value = GetAttribute<T>(node, attributeName);
            }

            public static T GetAttribute<T>(System.Xml.XmlNode data, string attributeName)
            {
                if (data == null || attributeName == null || attributeName.Length <= 0)
                    return default(T);

                if (!AttributeValid(data, attributeName))
                    return default(T);

                object attribute = GetAttribute(data, attributeName);

                if (attribute == null || string.IsNullOrEmpty((string)attribute))
                    return default(T);

                if (typeof(T) == typeof(int))
                    attribute = int.Parse((string)attribute);

                else if (typeof(T) == typeof(float))
                    attribute = float.Parse((string)attribute);

                else if (typeof(T) == typeof(bool))
                    attribute = int.Parse((string)attribute) <= 0 ? false : true;

                else if (typeof(T) == typeof(long))
                    attribute = long.Parse((string)attribute);

                return (T)attribute;
            }



            public static T[] ParseString<T>(string str, char[] split)
            {
                string[] splitArray = ParseString(str, split);

                if (splitArray == null || splitArray.Length <= 0)
                    return null;

                T[] resolveArray = new T[splitArray.Length];
                for (int i = 0; i < splitArray.Length; i++)
                {
                    resolveArray[i] = ParseString<T>(splitArray[i]);
                }

                return resolveArray;
            }
            public static void ParseString<T>(string str, char[] split, ref List<T> res)
            {
                string[] splitArray = ParseString(str, split);
                if (splitArray == null || splitArray.Length <= 0)
                    return;
                if (res == null)
                {
                    res = new List<T>();
                }
                for (int i = 0; i < splitArray.Length; i++)
                {
                    res.Add(ParseString<T>(splitArray[i]));
                }
            }
            public static void ParseString<T>(string str, char[] split, ref T[] resolveArray)
            {
                resolveArray = null;

                string[] splitArray = ParseString(str, split);

                if (splitArray == null || splitArray.Length <= 0)
                    return;

                resolveArray = new T[splitArray.Length];
                for (int i = 0; i < splitArray.Length; i++)
                {
                    resolveArray[i] = ParseString<T>(splitArray[i]);
                }
            }

            public static string[] ParseString(string str, char[] split)
            {
                if (str == null || str.Length <= 0)
                    return null;

                if (split == null || split.Length <= 0)
                    return null;

                string[] splitArray = str.Split(split);

                return splitArray;
            }

            public static void ParseString<T, T1>(string str, char[] split, ref T key, ref T1 value)
            {
                key = default(T);
                value = default(T1);

                string[] splitArray = ParseString(str, split);

                if (splitArray.Length >= 1)
                    key = ParseString<T>(splitArray[0]);

                if (splitArray.Length >= 2)
                    value = ParseString<T1>(splitArray[1]);
            }

            public static void ParseString<T, T1>(string str, char[] split1, char[] split2, ref T[] key, ref T1[] value)
            {
                key = null;
                value = null;

                string[] splitString = ParseString(str, split1);

                if (splitString == null || splitString.Length <= 0)
                    return;

                key = new T[splitString.Length];
                value = new T1[splitString.Length];

                for (int i = 0; i < splitString.Length; i++)
                {
                    ParseString<T, T1>(splitString[i], split2, ref key[i], ref value[i]);
                }
            }
            public static Dictionary<T, T1> ParseString<T, T1>(string str, char[] split1, char[] split2)
            {
                string[] splitString = ParseString(str, split1);

                if (splitString == null || splitString.Length <= 0)
                    return null;


                T key = default(T);
                T1 value = default(T1);
                Dictionary<T, T1> res = new Dictionary<T, T1>();

                for (int i = 0; i < splitString.Length; i++)
                {
                    ParseString<T, T1>(splitString[i], split2, ref key, ref value);
                    res.Add(key, value);
                }
                return res;
            }
            public static T ParseString<T>(string str)
            {
                object value = null;

                if (str == null || str.Length <= 0)
                    return default(T);

                if (typeof(T) == typeof(int))
                    value = int.Parse(str);

                else if (typeof(T) == typeof(float))
                    value = float.Parse(str);

                else if (typeof(T) == typeof(long))
                    value = long.Parse(str);
                else
                    value = str;

                return (T)value;
            }

            public static void ParseString<T>(string str, char[] split, char[] split1, ref T[][] resolveArray)
            {
                resolveArray = null;

                string[] splitString = ParseString(str, split);

                if (splitString == null || splitString.Length <= 0)
                    return;

                resolveArray = new T[splitString.Length][];

                for (int i = 0; i < splitString.Length; i++)
                {
                    ParseString(splitString[i], split1, ref resolveArray[i]);
                }
            }
            public static bool ParseString(string str, char[] split, ref Vector2 vectro3)
            {
                string[] array = ParseString(str, split);
                if (array != null && array.Length == 2)
                {
                    float.TryParse(array[0], out vectro3.x);
                    float.TryParse(array[1], out vectro3.y);
                    return true;
                }
                return false;
            }
            public static void ParseString(string str, char[] split, ref Vector3 vectro3)
            {
                string[] array = ParseString(str, split);
                if (array != null && array.Length == 3)
                {
                    float.TryParse(array[0], out vectro3.x);
                    float.TryParse(array[1], out vectro3.y);
                    float.TryParse(array[2], out vectro3.z);
                }
            }

            public static void ParseString(string str, char[] split, ref Vector4 vectro4)
            {
                string[] array = ParseString(str, split); ;
                if (array != null && array.Length == 4)
                {
                    float.TryParse(array[0], out vectro4.x);
                    float.TryParse(array[1], out vectro4.y);
                    float.TryParse(array[2], out vectro4.z);
                    float.TryParse(array[3], out vectro4.w);
                }
            }

            private static object GetAttribute(System.Xml.XmlNode node, string attributeName)
            {
                if (node == null || attributeName == null || attributeName.Length <= 0)
                    return null;

                return node.Attributes[attributeName].Value;
            }
        }
    }
}