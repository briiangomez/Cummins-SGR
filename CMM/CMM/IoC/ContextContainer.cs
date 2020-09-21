namespace CMM.IoC
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ContextContainer : IDisposable
    {
        private static ContextContainer _Shared = new ContextContainer(LifetimeManagers.Shared);

        private ContextContainer(ILifetimeManager liftetime)
        {
            this.Lifetime = liftetime;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this.Lifetime != null))
            {
                this.Lifetime.Dispose();
                this.Lifetime = null;
            }
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~ContextContainer()
        {
            this.Dispose(false);
        }

        public void RegisterInstance<T>(T instance)
        {
            this.Lifetime.RegisterInstance<T>(instance);
        }

        public void RegisterInstance<T>(string name, T instance)
        {
            this.Lifetime.RegisterInstance<T>(name, instance);
        }

        public void RegisterInstance(Type type, object instance)
        {
            this.Lifetime.RegisterInstance(type, instance);
        }

        public void RegisterInstance(Type type, string name, object instance)
        {
            this.Lifetime.RegisterInstance(type, name, instance);
        }

        public T Resolve<T>()
        {
            T instance = this.Lifetime.Resolve<T>();
            if (instance == null)
            {
                instance = ObjectContainer.CreateInstance<T>(null);
                this.Lifetime.RegisterInstance<T>(instance);
            }
            return instance;
        }

        public T Resolve<T>(string name)
        {
            T instance = this.Lifetime.Resolve<T>(name);
            if (instance == null)
            {
                instance = ObjectContainer.CreateInstance<T>(null);
                this.Lifetime.RegisterInstance<T>(name, instance);
            }
            return instance;
        }

        public object Resolve(Type type)
        {
            object instance = this.Lifetime.Resolve(type);
            if (instance == null)
            {
                instance = ObjectContainer.CreateInstance(type, null);
                this.Lifetime.RegisterInstance(type, instance);
            }
            return instance;
        }

        public T Resolve<T>(string name, string contractName)
        {
            T instance = this.Lifetime.Resolve<T>(name);
            if (instance == null)
            {
                instance = ObjectContainer.CreateInstance<T>(contractName);
                this.Lifetime.RegisterInstance<T>(name, instance);
            }
            return instance;
        }

        public object Resolve(Type type, string name)
        {
            object instance = this.Lifetime.Resolve(type, name);
            if (instance == null)
            {
                instance = ObjectContainer.CreateInstance(type, null);
                this.Lifetime.RegisterInstance(type, name, instance);
            }
            return instance;
        }

        public object Resolve(Type type, string name, string contractName)
        {
            object instance = this.Lifetime.Resolve(type, name);
            if (instance == null)
            {
                instance = ObjectContainer.CreateInstance(type, contractName);
                this.Lifetime.RegisterInstance(type, name, instance);
            }
            return instance;
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            IEnumerable<T> enumerable = this.Lifetime.ResolveAll<T>();
            if (enumerable == null)
            {
                enumerable = ObjectContainer.CreateInstances<T>(null);
                foreach (T local in enumerable)
                {
                    this.Lifetime.RegisterInstance<T>(local);
                }
            }
            return enumerable;
        }

        public IEnumerable<T> ResolveAll<T>(string name)
        {
            IEnumerable<T> enumerable = this.Lifetime.ResolveAll<T>(name);
            if (enumerable == null)
            {
                enumerable = ObjectContainer.CreateInstances<T>(null);
                foreach (T local in enumerable)
                {
                    this.Lifetime.RegisterInstance<T>(name, local);
                }
            }
            return enumerable;
        }

        public IEnumerable ResolveAll(Type type)
        {
            IEnumerable enumerable = this.Lifetime.ResolveAll(type);
            if (enumerable == null)
            {
                enumerable = ObjectContainer.CreateInstances(type, null);
                foreach (object obj2 in enumerable)
                {
                    this.Lifetime.RegisterInstance(type, obj2);
                }
            }
            return enumerable;
        }

        public IEnumerable<T> ResolveAll<T>(string name, string contractName)
        {
            IEnumerable<T> enumerable = this.Lifetime.ResolveAll<T>(name);
            if (enumerable == null)
            {
                enumerable = ObjectContainer.CreateInstances<T>(contractName);
                foreach (T local in enumerable)
                {
                    this.Lifetime.RegisterInstance<T>(name, local);
                }
            }
            return enumerable;
        }

        public IEnumerable ResolveAll(Type type, string name)
        {
            IEnumerable enumerable = this.Lifetime.ResolveAll(type, name);
            if (enumerable == null)
            {
                enumerable = ObjectContainer.CreateInstances(type, null);
                foreach (object obj2 in enumerable)
                {
                    this.Lifetime.RegisterInstance(type, name, obj2);
                }
            }
            return enumerable;
        }

        public IEnumerable ResolveAll(Type type, string name, string contractName)
        {
            IEnumerable enumerable = this.Lifetime.ResolveAll(type, name);
            if (enumerable == null)
            {
                enumerable = ObjectContainer.CreateInstances(type, contractName);
                foreach (object obj2 in enumerable)
                {
                    this.Lifetime.RegisterInstance(type, name, obj2);
                }
            }
            return enumerable;
        }

        public static ContextContainer Current
        {
            get
            {
                return new ContextContainer(LifetimeManagers.Default());
            }
        }

        private ILifetimeManager Lifetime { get; set; }

        public static ContextContainer Shared
        {
            get
            {
                return _Shared;
            }
        }
    }
}

