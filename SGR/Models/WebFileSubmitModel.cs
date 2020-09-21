namespace SGR.Communicator.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WebFileSubmitModel
    {
        public string RelativeDirectory { get; set; }

        public List<string> SuccessMessages { get; set; }
    }
}

