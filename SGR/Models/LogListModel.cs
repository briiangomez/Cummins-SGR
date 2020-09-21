namespace SGR.Communicator.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class LogListModel : ListModel<LogListItem>
    {
        public LogListItem Today { get; set; }

        public string Type { get; set; }
    }
}

