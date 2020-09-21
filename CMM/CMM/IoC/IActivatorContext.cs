namespace CMM.IoC
{
    using System.Collections.Generic;

    public interface IActivatorContext
    {
        IEnumerable<CreateHandler> GetHandlers();
    }
}

