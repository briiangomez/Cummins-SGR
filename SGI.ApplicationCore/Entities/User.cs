using System.Collections.Generic;

namespace SGI.ApplicationCore.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public virtual IEnumerable<UserRole> UserRoles { get; set; }

    }
}
