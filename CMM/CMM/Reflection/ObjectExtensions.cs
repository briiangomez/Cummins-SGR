namespace CMM.Reflection
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ObjectExtensions
    {
        public static CMM.Reflection.Members Members(this object o)
        {
            return new CMM.Reflection.Members(o);
        }
    }
}

