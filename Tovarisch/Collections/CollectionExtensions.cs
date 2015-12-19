namespace Wacton.Tovarisch.Collections
{
    using System.Collections.Generic;

    public static class CollectionExtensions
    {
        public static List<T> AsList<T>(this T item)
        {
            return new List<T> { item };
        }
    }
}
