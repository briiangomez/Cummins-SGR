using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;

namespace SGI.Infrastructure.Builders
{
    public class RolePermissionBuilder : BaseBuilder<RolePermission>
    {
        public RolePermissionBuilder(EntityTypeBuilder<RolePermission> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("RolePermissions");

            //Foreign keys

            entityBuilder
                .HasOne(a => a.Permission)
                .WithMany(b => b.RolePermissions)
                .HasForeignKey(c => c.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
                .HasOne(a => a.Role)
                .WithMany(b => b.RolePermissions)
                .HasForeignKey(c => c.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
