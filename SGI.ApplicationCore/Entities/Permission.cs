﻿using System.Collections.Generic;

namespace SGI.ApplicationCore.Entities
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }

        public virtual IEnumerable<RolePermission> RolePermissions { get; set; }
    }
}