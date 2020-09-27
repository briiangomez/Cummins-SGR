using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;

namespace SGI.Infrastructure.Builders
{
    public class RoleBuilder : BaseBuilder<Role>
    {
        public RoleBuilder(EntityTypeBuilder<Role> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("Roles");

            entityBuilder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar(100)").IsRequired();
            entityBuilder.Property(x => x.Code).HasColumnName("code").HasColumnType("varchar(100)").IsRequired();
        }
    }
}
