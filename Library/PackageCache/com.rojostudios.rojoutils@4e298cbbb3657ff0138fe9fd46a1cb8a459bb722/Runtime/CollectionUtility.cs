using System.Collections.Generic;
using UnityEngine;

namespace RoJoStudios.Utils
{
    public static class CollectionUtility
    {
        public static HashSet<T> ToHashSet<T>(
            this IEnumerable<T> source,
            IEqualityComparer<T> comparer = null)
        {
            return new HashSet<T>(source, comparer);
        }

        public static T GetRandom<T>(this IList<T> source)
        {
            if (source.Count == 0)
            {
                return default;
            }
            
            Random.InitState((int)Time.time);
            int index = Random.Range(0, source.Count);
            return source[index];
        }
    }
}