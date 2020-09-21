using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;

namespace SGI.Infrastructure.Builders
{
    public class PermissionBuilder : BaseBuilder<Permission>
    {
        public PermissionBuilder(EntityTypeBuilder<Permission> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("Permissions");

            entityBuilder.Property(x => x.Name).HasColumnName("Name").HasColumnType("varchar(200)").IsRequired();


        }
    }
}
