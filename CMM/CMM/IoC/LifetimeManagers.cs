namespace CMM.IoC
{
    using System;
    using System.Runtime.CompilerServices;

    public sealed class LifetimeManagers
    {
        static LifetimeManagers()
        {
            Default = () => new PerRequestLifetimeManager();
            Shared = new SharedLifetimeManager();
        }

        public static Func<ILifetimeManager> Default
        {
            get;
            set;
        }

        public static ILifetimeManager Shared
        {
            get;
            set;
        }
    }
}

