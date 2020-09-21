namespace CMM.IoC
{
    using System;

    public interface IObjectFacotry
    {
        object CreateInstance(Type type);
    }
}

