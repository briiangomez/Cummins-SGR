using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;

namespace SGI.Infrastructure.Builders
{
    public class UserRoleBuilder : BaseBuilder<UserRole>
    {
        public UserRoleBuilder(EntityTypeBuilder<UserRole> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("UserRoles");

            //Foreign keys

            entityBuilder
                .HasOne(a => a.User)
                .WithMany(b => b.UserRoles)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
                .HasOne(a => a.Role)
                .WithMany(b => b.UserRoles)
                .HasForeignKey(c => c.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
