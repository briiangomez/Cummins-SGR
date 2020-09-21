namespace CMM.Domain
{
    using System;

    public interface ITransactionContext
    {
        void Begin();
        void Commit();
        void Rollback();

        bool IsStarted { get; }
    }
}

