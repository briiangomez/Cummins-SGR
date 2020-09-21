namespace CMM.Dynamic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class DynamicDictionary : DynamicObject, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        private IDictionary<string, object> dictionary;

        public DynamicDictionary()
        {
            this.dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        public DynamicDictionary(IDictionary<string, object> dictionary)
        {
            this.dictionary = new Dictionary<string, object>(dictionary, StringComparer.OrdinalIgnoreCase);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            this.dictionary.Add(item);
        }

        public void Add(string key, object value)
        {
            this.dictionary[key] = value;
        }

        public void Clear()
        {
            this.dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return this.dictionary.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return this.dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            this.dictionary.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return this.dictionary.Remove(item);
        }

        public bool Remove(string key)
        {
            return this.dictionary.Remove(key);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string name = binder.Name;
            if (!this.dictionary.TryGetValue(name, out result))
            {
                result = null;
            }
            return true;
        }

        public bool TryGetValue(string key, out object value)
        {
            return this.dictionary.TryGetValue(key, out value);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this.dictionary[binder.Name] = value;
            return true;
        }

        public int Count
        {
            get
            {
                return this.dictionary.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.dictionary.IsReadOnly;
            }
        }

        public object this[string key]
        {
            get
            {
                if (this.dictionary.ContainsKey(key))
                {
                    return this.dictionary[key];
                }
                return null;
            }
            set
            {
                this.dictionary[key] = value;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                return this.dictionary.Keys;
            }
        }

        public ICollection<object> Values
        {
            get
            {
                return this.dictionary.Values;
            }
        }
    }
}

