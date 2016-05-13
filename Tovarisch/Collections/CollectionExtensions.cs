namespace Wacton.Tovarisch.Collections
{
    using System.Collections.Generic;
    using System.Linq;

    public static class CollectionExtensions
    {
        public static List<T> AsList<T>(this T item)
        {
            return new List<T> { item };
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> collection, params T[] items)
        {
            return collection.Concat(items);
        }

        public static void AddOrReplace<TK, TV>(this Dictionary<TK, TV> dictionary, TK key, TV value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static List<T> Duplicates<T>(this IEnumerable<T> collection)
        {
            return collection.GroupBy(item => item)
                             .Where(group => group.Count() > 1)
                             .Select(group => group.Key)
                             .ToList();
        }

        public static string ToDelimitedString<T>(this IEnumerable<T> collection, string delimiter)
        {
            return string.Join(delimiter, collection);
        }
    }
}
