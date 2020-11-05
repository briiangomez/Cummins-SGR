using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sgi.Models.SgiCore
{
  [Table("UserRoles", Schema = "dbo")]
  public partial class UserRole
  {
    [Key]
    public Guid Id
    {
      get;
      set;
    }
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
    public Guid UserId
    {
      get;
      set;
    }
    public User User { get; set; }
    public Guid RoleId
    {
      get;
      set;
    }
    public Role Role { get; set; }
  }
}
