using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sgi.Models.SgiCore
{
  [Table("Users", Schema = "dbo")]
  public partial class User
  {
    [Key]
    public Guid Id
    {
      get;
      set;
    }

    public ICollection<UserRole> UserRoles { get; set; }
    public DateTime Created
    {
      get;
      set;
    }
    public DateTime? Modified
    {
      get;
      set;
    }
    public DateTime? Deleted
    {
      get;
      set;
    }
    public string username
    {
      get;
      set;
    }
    public string email
    {
      get;
      set;
    }
    public string password
    {
      get;
      set;
    }
    public string name
    {
      get;
      set;
    }
    public string lastname
    {
      get;
      set;
    }

        public string AccessToken
        {
            get;
            set;
        }
        public string RefreshToken
        {
            get;
            set;
        }
    }
}
