using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.JDI.Commons
{
    public static class CommonExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var element in enumerable)
                action(element);
        }
        public static string FromNewLine(this string s)
        {
            return " " + Environment.NewLine + s;
        }

        public static string LineBreak(this string s)
        {
            return s + " " + Environment.NewLine;
        }

        public static string CutTestData(this string str)
        {
            var index = str.IndexOf('(');
                return (index > 0)
                ? str.Substring(0, index)
                : str;
        }

        public static string Print(this IEnumerable<string> list, string separator = ", ", string format = "{0}")
        {
            return (list != null) ? string.Join(separator, list.Select(el => string.Format(format, el))) : "";
        }

        public static string ToString(this IEnumerable<string> list, string separator = ", ", string format = "{0}")
        {
            return (list != null) ? string.Join(separator, list.Select(el => string.Format(format, el))) : "";
        }

        public static string Print<TValue>(this IEnumerable<KeyValuePair<string, TValue>> collection, string separator = "; ", string pairFormat = "{0}: {1}")
        {
            return (collection != null) ? string.Join(separator, collection.Select(pair => string.Format(pairFormat, pair.Key, pair.Value))) : "";
        }


        public static Object GetFieldByName(this Object obj, string fieldName)
        {
            var fieldsQueue = new Queue<string> (fieldName.Split('.'));
            var result = obj;
            while (fieldsQueue.Any() && result != null)
            {
                var fieldsName = fieldsQueue.Dequeue();
                var fieldInfo = result.GetType().GetField(fieldsName);
                if (fieldInfo != null)
                {
                    result = fieldInfo.GetValue(result);
                    continue;
                }
                var propInfo = result.GetType().GetProperty(fieldsName);
                result = propInfo?.GetValue(result);
            }
            return result;
        }
        
    }
}
