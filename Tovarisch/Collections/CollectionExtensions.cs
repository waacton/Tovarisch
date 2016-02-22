namespace Wacton.Tovarisch.Collections
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class CollectionExtensions
    {
        public static List<T> AsList<T>(this T item)
        {
            return new List<T> { item };
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
            var list = collection.ToList();
            if (!list.Any())
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();
            foreach (var item in list)
            {
                stringBuilder.Append(item);
                stringBuilder.Append(delimiter);
            }

            // remove the trailing delimiter
            stringBuilder.Remove(stringBuilder.Length - delimiter.Length, delimiter.Length);
            return stringBuilder.ToString();
        }
    }
}
