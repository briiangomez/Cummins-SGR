namespace SGR.Communicator.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class TabItem
    {
        public TabItem()
        {
            this.Enabled = true;
        }

        public bool Enabled { get; set; }

        public string Href { get; set; }

        public bool Selected { get; set; }

        public string Text { get; set; }

        public string Value { get; set; }
    }
}

