using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;

namespace SGI.Infrastructure.Builders
{
    public class UserBuilder : BaseBuilder<User>
    {
        public UserBuilder(EntityTypeBuilder<User> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("Users");

            entityBuilder.Property(x => x.UserName).HasColumnName("username").HasColumnType("varchar(100)").IsRequired();
            entityBuilder.Property(x => x.Email).HasColumnName("email").HasColumnType("varchar(100)").IsRequired();
            entityBuilder.Property(x => x.Password).HasColumnName("password").HasColumnType("varchar(100)").IsRequired();
            entityBuilder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar(100)");
            entityBuilder.Property(x => x.LastName).HasColumnName("lastname").HasColumnType("varchar(100)");

        }
    }
}
