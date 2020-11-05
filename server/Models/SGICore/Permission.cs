using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sgi.Models.SgiCore
{
  [Table("Permissions", Schema = "dbo")]
  public partial class Permission
  {
    [Key]
    public Guid Id
    {
      get;
      set;
    }

    public ICollection<RolePermission> RolePermissions { get; set; }
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
    public string Name
    {
      get;
      set;
    }
  }
}
