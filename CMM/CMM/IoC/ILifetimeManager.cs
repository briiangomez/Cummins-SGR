namespace CMM.IoC
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface ILifetimeManager : IDisposable
    {
        void RegisterInstance<T>(T instance);
        void RegisterInstance<T>(string name, T instance);
        void RegisterInstance(Type type, object instance);
        void RegisterInstance(Type type, string name, object instance);
        T Resolve<T>();
        T Resolve<T>(string name);
        object Resolve(Type type);
        object Resolve(Type type, string name);
        IEnumerable<T> ResolveAll<T>();
        IEnumerable<T> ResolveAll<T>(string name);
        IEnumerable ResolveAll(Type type);
        IEnumerable ResolveAll(Type type, string name);
    }
}

