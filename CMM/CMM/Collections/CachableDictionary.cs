namespace CMM.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class CachableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
    {
        private IDictionary<TKey, TValue> cache;

        public CachableDictionary()
        {
            this.cache = new Dictionary<TKey, TValue>();
        }

        public CachableDictionary(IDictionary<TKey, TValue> dictionary)
        {
            this.cache = dictionary;
        }

        public CachableDictionary(IEqualityComparer<TKey> comparer)
        {
            this.cache = new Dictionary<TKey, TValue>(comparer);
        }

        public CachableDictionary(int capacity)
        {
            this.cache = new Dictionary<TKey, TValue>(capacity);
        }

        public CachableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            this.cache = new Dictionary<TKey, TValue>(dictionary, comparer);
        }

        public CachableDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            this.cache = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.cache.Add(item);
        }

        public void Add(TKey key, TValue value)
        {
            this.cache.Add(key, value);
        }

        public void Clear()
        {
            this.cache.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.cache.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return this.cache.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            this.cache.CopyTo(array, arrayIndex);
        }

        public TValue Get(TKey key, Func<TValue> createCachingValue)
        {
            TValue local = default(TValue);
            if (!this.TryGetValue(key, out local))
            {
                lock (this.cache)
                {
                    if (!this.cache.TryGetValue(key, out local))
                    {
                        local = createCachingValue();
                        this.cache[key] = local;
                    }
                }
            }
            return local;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.cache.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            return this.cache.Remove(key);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.cache.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.cache.GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.cache.TryGetValue(key, out value);
        }

        public int Count
        {
            get
            {
                return this.cache.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.cache.IsReadOnly;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.cache[key];
            }
            set
            {
                this.cache[key] = value;
            }
        }

        public TValue this[TKey key, Func<TValue> createCachingValue]
        {
            get
            {
                return this.Get(key, createCachingValue);
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return this.cache.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return this.cache.Values;
            }
        }
    }
}

