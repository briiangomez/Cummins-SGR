namespace CMM
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web;

    public class CallContext : IDisposable
    {
        [ThreadStatic]
        private static CallContext _current;
        internal static readonly string CONTEXT_CONTAINER = "__CONTEXT_CONTAINER__";

        private CallContext()
        {
            this.RegisteredObjects = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
        }

        public void Dispose()
        {
            if (this.RegisteredObjects == null)
            {
                foreach (object obj2 in this.RegisteredObjects.Values)
                {
                    if (obj2 is IDisposable)
                    {
                        ((IDisposable) obj2).Dispose();
                    }
                }
            }
            this.RegisteredObjects = null;
        }

        public static string GenerateKey(string name, Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("Type");
            }
            if (name == null)
            {
                name = "NULL";
            }
            return (name + "_" + type.ToString());
        }

        public T GetObject<T>()
        {
            string key = GenerateKey(string.Empty, typeof(T));
            return this.GetObject<T>(key);
        }

        public T GetObject<T>(string key)
        {
            object obj2 = null;
            if (this.RegisteredObjects.TryGetValue(key, out obj2))
            {
                return (T) obj2;
            }
            return default(T);
        }

        public void RegisterObject<T>(T obj)
        {
            string key = GenerateKey(string.Empty, typeof(T));
            this.RegisteredObjects.Add(key, obj);
        }

        public void RegisterObject<T>(string key, T obj)
        {
            this.RegisteredObjects[key] = obj;
        }

        public T Resolve<T>()
        {
            string key = GenerateKey(string.Empty, typeof(T));
            T local = this.GetObject<T>(key);
            if (local == null)
            {
                local = Activator.CreateInstance<T>();
                this.RegisterObject<T>(key, local);
            }
            return local;
        }

        public static CallContext Current
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    CallContext context = HttpContext.Current.Items[CONTEXT_CONTAINER] as CallContext;
                    if (context == null)
                    {
                        context = new CallContext();
                        HttpContext.Current.Items.Add(CONTEXT_CONTAINER, context);
                    }
                    return context;
                }
                if (_current == null)
                {
                    _current = new CallContext();
                }
                return _current;
            }
        }

        private IDictionary<string, object> RegisteredObjects { get; set; }
    }
}

