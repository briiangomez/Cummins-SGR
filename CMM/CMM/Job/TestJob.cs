namespace CMM.Job
{
    using System;
    using System.IO;
    using System.Threading;

    public class TestJob : IJob
    {
        private string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testjob.txt");

        public void Error(Exception e)
        {
            File.AppendAllLines(this.logFile, new string[] { string.Format("Exception throwed on {0}, message:{1}", DateTime.Now, e.Message) });
        }

        public void Execute(object executionState)
        {
            Thread.Sleep(0xbb8);
            File.AppendAllLines(this.logFile, new string[] { string.Format("Run on {0}", DateTime.Now) });
        }
    }
}

