namespace CMM.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class ObjectPool : IDisposable
    {
        public ObjectPool()
        {
            this.Items = new Dictionary<ObjectKey, List<object>>();
        }

        public void Add(ObjectKey key, object value)
        {
            if (!this.Items.ContainsKey(key))
            {
                this.Items.Add(key, new List<object>());
            }
            this.Items[key].Add(value);
        }

        public void AddRange(ObjectKey key, List<object> items)
        {
            if (this.Items.ContainsKey(key))
            {
                this.Items[key].AddRange(items);
            }
            else
            {
                this.Items.Add(key, items);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (KeyValuePair<ObjectKey, List<object>> pair in this.Items)
                {
                    foreach (IDisposable disposable in pair.Value.OfType<IDisposable>())
                    {
                        disposable.Dispose();
                    }
                    pair.Value.Clear();
                }
                this.Items.Clear();
            }
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~ObjectPool()
        {
            this.Dispose(false);
        }

        public void Remove(ObjectKey key, object value)
        {
            if (this.Items.ContainsKey(key))
            {
                this.Items[key].Remove(value);
            }
        }

        public List<object> this[ObjectKey key]
        {
            get
            {
                if (this.Items.ContainsKey(key))
                {
                    return this.Items[key];
                }
                return null;
            }
        }

        private Dictionary<ObjectKey, List<object>> Items { get; set; }
    }
}

