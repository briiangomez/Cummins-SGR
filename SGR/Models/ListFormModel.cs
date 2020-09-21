namespace SGR.Communicator.Models
{
    using CMM.Globalization;
    using System;
    using System.Runtime.CompilerServices;

    public class ListFormModel
    {
        private bool? _selectAll;
        private Guid[] _selected;
        public static readonly ListFormModel Empty = new ListFormModel();

        public bool IsCommand(string command)
        {
            return (this.Command == command.Localize(""));
        }

        public string Command { get; set; }

        public bool SelectAll
        {
            get
            {
                if (!this._selectAll.HasValue)
                {
                    //this._selectAll = new bool?(FormUtility.Checked("SelectAll"));
                }
                return this._selectAll.Value;
            }
            set
            {
                this._selectAll = new bool?(value);
            }
        }

        public Guid[] Selected
        {
            get
            {
                if (this._selected == null)
                {
                    this._selected = new Guid[0];
                }
                return this._selected;
            }
            set
            {
                this._selected = value;
            }
        }
    }
}

