namespace CMM.Job
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class JobExecutor
    {
        private Timer timer;

        public JobExecutor(IJob job, int interval, object executionState)
        {
            this.Job = job;
            this.Interval = interval;
            this.ExecutionState = executionState;
        }

        public void Start()
        {
            if (!this.Started)
            {
                this.timer = new Timer(new System.Threading.TimerCallback(this.TimerCallback), this.ExecutionState, this.Interval, this.Interval);
                this.Started = true;
            }
        }

        public void Stop()
        {
            this.timer.Dispose();
            this.timer = null;
        }

        private void TimerCallback(object state)
        {
            if (this.Started && !this.IsRunning)
            {
                this.timer.Change(-1, -1);
                try
                {
                    this.Job.Execute(state);
                }
                catch (Exception exception)
                {
                    this.Job.Error(exception);
                }
                this.IsRunning = false;
                if (this.Started)
                {
                    this.timer.Change(this.Interval, this.Interval);
                }
            }
        }

        public object ExecutionState { get; private set; }

        public int Interval { get; private set; }

        public bool IsRunning { get; private set; }

        public IJob Job { get; private set; }

        public bool Started { get; set; }
    }
}

