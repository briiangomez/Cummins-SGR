namespace CMM.IoC
{
    using System.Collections;

    public interface IRequestContext
    {
        IDictionary Items { get; }
    }
}

