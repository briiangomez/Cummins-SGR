namespace SGR.Communicator.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Microsoft.AspNetCore.Hosting;

    public class LogModel
    {
        private static readonly IWebHostEnvironment _webHostEnvironment;

        public LogModel()
        {
        }

        public const string BaseFileName = "log.txt";
        string webRootPath = _webHostEnvironment.WebRootPath;
        public static string ServiceDirectory = _webHostEnvironment.WebRootPath + "/Service";
        public static string WebDirectory = _webHostEnvironment.WebRootPath + "/logs";

        private static IEnumerable<LogListItem> FilterHistoryLogs(string dir)
        {
            foreach (FileInfo iteratorVariable0 in from o in new DirectoryInfo(dir).GetFiles()
                orderby o.Name descending
                select o)
            {
                string iteratorVariable1 = iteratorVariable0.Name.ToLower();
                if ((iteratorVariable1.Length > "log.txt".Length) && (iteratorVariable0.Length > 0L))
                {
                    string iteratorVariable2 = iteratorVariable1.Substring("log.txt".Length).TrimStart(new char[] { '.' });
                    LogListItem iteratorVariable3 = new LogListItem {
                        DateParameter = iteratorVariable2,
                        Date = Convert.ToDateTime(iteratorVariable2).ToShortDateString()
                    };
                    yield return iteratorVariable3;
                }
            }
        }

        public static LogModel Get()
        {
            return new LogModel { WebLog = GetWebLogs(), ServiceLog = GetServiceLogs() };
        }

        private static LogListModel GetLogs(string dir, string type)
        {
            LogListModel result;
            if (!Directory.Exists(dir))
            {
                result = new LogListModel
                {
                    Type = type,
                    Today = null,
                    Items = Enumerable.Empty<LogListItem>()
                };
            }
            else
            {
                FileInfo fileInfo = new FileInfo(Path.Combine(dir, "log.txt"));
                result = new LogListModel
                {
                    Type = type,
                    Today = (fileInfo.Exists && fileInfo.Length > 0L) ? new LogListItem() : null,
                    Items = LogModel.FilterHistoryLogs(dir)
                };
            }
            return result;
        }


        private static LogListModel GetServiceLogs()
        {
            return GetLogs(ServiceDirectory, "service");
        }

        private static LogListModel GetWebLogs()
        {
            return GetLogs(WebDirectory, "web");
        }

        public LogListModel ServiceLog { get; set; }

        public LogListModel WebLog { get; set; }

    }
}

