using System.Collections.Generic;

namespace SGI.ApplicationCore.Entities
{
    public class Role : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<RolePermission> RolePermissions { get; set; }
        public virtual IEnumerable<UserRole> UserRoles { get; set; }
    }
}
