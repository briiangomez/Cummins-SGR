namespace CMM.IoC
{
    using System;

    public class ObjectFactory : IObjectFacotry
    {
        public object CreateInstance(Type type)
        {
            object obj2;
            try
            {
                obj2 = Activator.CreateInstance(type);
            }
            catch (MissingMethodException exception)
            {
                throw new NotSupportedException(type.ToString(), exception);
            }
            return obj2;
        }
    }
}

