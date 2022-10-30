
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Extensions
{
    internal static class EnumerableExtension
    {
        public static bool HasMany<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Skip(1).Any();
        }

        public static bool HasOne<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Any() && !enumerable.Skip(1).Any();
        }
    }

}