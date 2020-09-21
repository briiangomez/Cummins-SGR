using System.Collections.Generic;

namespace SGR.Communicator.Models
{
    public class MenuItem
    {
        private IList<MenuItem> _subMenus;

        public MenuItem()
        {
            this.NeedAdmin = false;
        }

        public string Class { get; set; }

        public string Group { get; set; }

        public string Id { get; set; }

        public bool NeedAdmin { get; set; }

        public IList<MenuItem> SubMenus
        {
            get
            {
                if (this._subMenus == null)
                {
                    this._subMenus = new List<MenuItem>();
                }
                return this._subMenus;
            }
            set
            {
                this._subMenus = value;
            }
        }

        public string Text { get; set; }

        public string Url { get; set; }
    }
}

