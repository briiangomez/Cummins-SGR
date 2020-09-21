using System.Linq;
using SGR.Communicator;

namespace SGR.Communicator.Models
{
    

    public class AddTagModel
    {
        private string[] _groups;

        public string Group { get; set; }

        public string[] Groups
        {
            get
            {
                if (this._groups == null)
                {
                    //this._groups = (from o in CommunicatorContext.Current.ContactManager.GetTagGroups(CommunicatorContext.Current.PortalId) select o.Name).ToArray<string>();
                }
                return this._groups;
            }
        }

        public string TagName { get; set; }
    }
}

