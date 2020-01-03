using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RoJoStudios.Utils
{
    [System.Serializable]
    public abstract class SerializableDictionary<K, V> : IDictionary<K, V>, ISerializationCallbackReceiver
    {
        [SerializeField] private K[] keys;
        [SerializeField] private V[] values;

        private Dictionary<K, V> dictionary = new Dictionary<K, V>();
        public V this[K key] { get => dictionary[key]; set => dictionary[key] = value; }
        public ICollection<K> Keys => dictionary.Keys;
        public ICollection<V> Values => dictionary.Values;
        public int Count => dictionary.Count;
        public bool IsReadOnly => false;


        public void OnAfterDeserialize()
        {
            if (keys == null)
            {
                return;
            }
            var c = keys.Length;
            for (int i = 0; i < c; i++)
            {
                Add(keys[i], values[i]);
            }
            keys = null;
            values = null;
        }

        public void OnBeforeSerialize()
        {
            var c = Count;

            keys = new K[c];
            values = new V[c];

            int i = 0;
            foreach (var pair in dictionary)
            {
                keys[i] = pair.Key;
                values[i] = pair.Value;
                i++;
            }
        }



        public void Add(K key, V value)
        {
            dictionary.Add(key, value);
        }

        public void Add(KeyValuePair<K, V> item)
        {
            dictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            dictionary.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            V i;
            bool contains = dictionary.TryGetValue(item.Key, out i);
            if (contains && i.Equals(item.Value))
            {
                return true;
            }
            return false;
        }

        public bool ContainsKey(K key)
        {
            return dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < array.Length; i++)
            {
                dictionary.Add(array[i].Key, array[i].Value);
            }
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }


        public bool Remove(K key)
        {
            return dictionary.Remove(key);
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            if (dictionary.ContainsKey(item.Key))
            {
                return dictionary.Remove(item.Key);
            }

            return false;
        }

        public bool TryGetValue(K key, out V value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }
    }
}