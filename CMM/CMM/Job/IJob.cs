namespace CMM.Job
{
    using System;

    public interface IJob
    {
        void Error(Exception e);
        void Execute(object executionState);
    }
}

