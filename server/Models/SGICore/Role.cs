using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sgi.Models.SgiCore
{
  [Table("Roles", Schema = "dbo")]
  public partial class Role
  {
    [Key]
    public Guid Id
    {
      get;
      set;
    }

    public ICollection<RolePermission> RolePermissions { get; set; }
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
    public string code
    {
      get;
      set;
    }
    public string name
    {
      get;
      set;
    }
  }
}
