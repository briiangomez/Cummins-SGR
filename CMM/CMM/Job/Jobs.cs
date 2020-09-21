namespace CMM.Job
{
    using System;
    using System.Collections.Generic;

    public class Jobs
    {
        private static readonly Jobs instance = new Jobs();
        private Dictionary<string, JobExecutor> jobs = new Dictionary<string, JobExecutor>(StringComparer.CurrentCultureIgnoreCase);
        private static object lockHelper = new object();

        public void AttachJob(string name, IJob job, int interval, object executionState, bool start)
        {
            lock (lockHelper)
            {
                JobExecutor executor = new JobExecutor(job, interval, executionState);
                this.jobs[name] = executor;
                executor.Start();
            }
        }

        public void Start()
        {
            lock (lockHelper)
            {
                foreach (JobExecutor executor in this.jobs.Values)
                {
                    executor.Start();
                }
            }
        }

        public void Stop()
        {
            lock (this.jobs)
            {
                foreach (JobExecutor executor in this.jobs.Values)
                {
                    executor.Stop();
                }
            }
        }

        public static Jobs Instance
        {
            get
            {
                return instance;
            }
        }
    }
}

