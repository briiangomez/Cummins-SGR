using System;
using System.Collections.Generic;
using System.Linq;

namespace SGR.Communicator
{
    public class CommunicatorContext
    {
        public Guid PortalId { get; set; }
        // private static object ContextKey = new object();
        // private ContactGroup[] _groups;
        // private Role[] _roles;
        // private System.Collections.Generic.Dictionary<System.Guid, ContactGroup> _allGroups;
        // private Tag[] _tags;
        // private System.Guid? _userId;
        // private User _user;
        // private ContactSchemaModel _contactSchema;
        // private MailPortal _mailPortal;
        // public PortalManager PortalManager
        // {
        //     get;
        //     set;
        // }
        // public ContactManager ContactManager
        // {
        //     get;
        //     set;
        // }
        // public MailManager MailManager
        // {
        //     get;
        //     set;
        // }
        // public CampaignManager CampaignManager
        // {
        //     get;
        //     set;
        // }
        // public ReportManager ReportManager
        // {
        //     get;
        //     set;
        // }
        // public User User
        // {
        //     get
        //     {
        //         if (this._user == null)
        //         {
        //             this._user = (HttpContext.Current.Session["User"] as User);
        //             if (this._user == null)
        //             {
        //                 this._user = this.PortalManager.GetUser(this.UserId);
        //                 if (this._user == null)
        //                 {
        //                     this.Resignin();
        //                 }
        //                 else
        //                 {
        //                     HttpContext.Current.Session["User"] = this._user;
        //                 }
        //             }
        //         }
        //         return this._user;
        //     }
        // }
        // public System.Guid UserId
        // {
        //     get
        //     {
        //         if (!this._userId.HasValue)
        //         {
        //             this._userId = new System.Guid?(new System.Guid(HttpContext.Current.User.Identity.Name));
        //         }
        //         return this._userId.Value;
        //     }
        // }
        // public string UserName
        // {
        //     get
        //     {
        //         return this.User.Entry;
        //     }
        // }
        // public System.Collections.Generic.IDictionary<System.Guid, string> AccessablePortals
        // {
        //     get
        //     {
        //         System.Collections.Generic.IDictionary<System.Guid, string> dictionary = HttpContext.Current.Session["AccessablePortals"] as System.Collections.Generic.IDictionary<System.Guid, string>;
        //         if (dictionary == null)
        //         {
        //             dictionary = this.PortalManager.GetAccessablePortals(this.UserId).ToDictionary((Portal o) => o.Id, (Portal o) => o.Name);
        //             HttpContext.Current.Session["AccessablePortals"] = dictionary;
        //         }
        //         return dictionary;
        //     }
        // }
        // public HashSet<string> AccessableMenus
        // {
        //     get
        //     {
        //         HashSet<string> hashSet = HttpContext.Current.Session["AccessableMenus"] as HashSet<string>;
        //         if (hashSet == null)
        //         {
        //             System.Collections.Generic.IEnumerable<Role> userRoles = this.PortalManager.GetUserRoles(this.PortalId, this.UserId);
        //             hashSet = new HashSet<string>();
        //             foreach (Role current in userRoles)
        //             {
        //                 foreach (string current2 in current.Functions)
        //                 {
        //                     string[] array = current2.Split(new char[]
        //{
        //	'_'
        //}, System.StringSplitOptions.RemoveEmptyEntries);
        //                     for (int num = 1; num <= array.Length; num++)
        //                     {
        //                         hashSet.Add(string.Join("_", array, 0, num));
        //                     }
        //                 }
        //             }
        //             HttpContext.Current.Session["AccessableMenus"] = hashSet;
        //         }
        //         return hashSet;
        //     }
        // }
        // public System.Guid PortalId
        // {
        //     get
        //     {
        //         System.Guid? portalId = this.User.PortalId;
        //         System.Guid result;
        //         if (portalId.HasValue)
        //         {
        //             portalId = this.User.PortalId;
        //             result = portalId.Value;
        //         }
        //         else
        //         {
        //             result = System.Guid.Empty;
        //         }
        //         return result;
        //     }
        //     set
        //     {
        //         using (UnitOfWork unitOfWork = new UnitOfWork())
        //         {
        //             User user = this.PortalManager.GetUser(this.UserId);
        //             user.PortalId = new System.Guid?(value);
        //             IUserRepository userRepository = PersistenceManager.ResolveDao<IUserRepository>();
        //             userRepository.Update(user);
        //             unitOfWork.Complete();
        //         }
        //         HttpContext.Current.Session.Clear();
        //     }
        // }
        // public Portal Portal
        // {
        //     get
        //     {
        //         string key = this.PortalId.ToString();
        //         Portal portal = HttpContext.Current.Cache[key] as Portal;
        //         if (portal == null)
        //         {
        //             portal = this.PortalManager.GetPortal(this.PortalId);
        //             if (portal != null)
        //             {
        //                 HttpContext.Current.Cache.Add(key, portal, null, Cache.NoAbsoluteExpiration, System.TimeSpan.FromMinutes(10.0), CacheItemPriority.High, null);
        //             }
        //         }
        //         return portal;
        //     }
        // }
        // public MailPortal MailPortal
        // {
        //     get
        //     {
        //         if (this._mailPortal == null)
        //         {
        //             this._mailPortal = this.MailManager.GetPortal(this.PortalId);
        //         }
        //         return this._mailPortal;
        //     }
        // }
        // public System.Collections.Generic.IDictionary<System.Guid, ContactGroup> AllContactGroups
        // {
        //     get
        //     {
        //         if (this._allGroups == null)
        //         {
        //             //Ingenium
        //             //this._allGroups = this.ContactManager.GetContactGroups(this.PortalId, false).ToDictionary((ContactGroup o) => o.Id);
        //             this._allGroups = this.ContactManager.GetContactGroups(this.PortalId, true).ToDictionary((ContactGroup o) => o.Id);
        //         }
        //         return this._allGroups;
        //     }
        // }
        // public ContactGroup[] ContactGroups
        // {
        //     get
        //     {
        //         if (this._groups == null)
        //         {
        //             this._groups = (
        //                 from o in this.AllContactGroups.Values
        //                 where !o.Duration.IsOver
        //                 select o).ToArray<ContactGroup>();
        //         }
        //         return this._groups;
        //     }
        // }
        // public Role[] Roles
        // {
        //     get
        //     {
        //         if (this._roles == null)
        //         {
        //             this._roles = this.PortalManager.GetRoles().ToArray<Role>();
        //         }
        //         return this._roles;
        //     }
        // }

        // public Tag[] Tags
        // {
        //     get
        //     {
        //         if (this._tags == null)
        //         {
        //             this._tags = (from o in this.ContactManager.GetTags(this.PortalId)
        //                           orderby o.Name
        //                           select o).ToArray<Tag>();
        //         }
        //         return this._tags;
        //     }
        // }


        // public ContactSchemaModel ContactSchema
        // {
        //     get
        //     {
        //         if (this._contactSchema == null)
        //         {
        //             this._contactSchema = new ContactSchemaModel();
        //         }
        //         return this._contactSchema;
        //     }
        // }

        public static CommunicatorContext Current
        {
            //get
            //{
            //    CommunicatorContext communicatorContext = HttpContext.Current.Items[CommunicatorContext.ContextKey] as CommunicatorContext;

            //    if (communicatorContext == null)
            //        HttpContext.Current.Items[CommunicatorContext.ContextKey] = (object)(communicatorContext = new CommunicatorContext());

            //    return communicatorContext;
            //}
            get
            {
                return null;
            }
        }

        public CommunicatorContext()
        {
            //this.PortalManager = new PortalManager();
            //this.ContactManager = new ContactManager();
            //this.MailManager = new MailManager();
            //this.CampaignManager = new CampaignManager();
            //this.ReportManager = new ReportManager();
        }



        public static string ContextExecutingAssembly
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }


        }
    }
}
