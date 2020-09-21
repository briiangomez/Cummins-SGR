namespace CMM.IoC
{
    using System;
    using System.Runtime.CompilerServices;

    public class CreateHandler
    {
        public Func<object> Invoker { get; set; }

        public System.Type Type { get; set; }
    }
}

