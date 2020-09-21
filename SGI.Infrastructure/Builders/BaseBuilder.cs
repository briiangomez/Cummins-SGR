using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;
using SGI.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.Infrastructure.Builders
{
    public abstract class BaseBuilder<T> where T : BaseEntity
    {

        protected BaseBuilder(EntityTypeBuilder<T> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired().HasDefaultValueSql("NEWID()");
            entityBuilder.Property(x => x.Created).HasColumnName("Created").HasColumnType("datetime").IsRequired().HasDefaultValueSql("getdate()");
            entityBuilder.Property(x => x.Modified).HasColumnName("Modified").HasColumnType("datetime");
            entityBuilder.Property(x => x.Deleted).HasColumnName("Deleted").HasColumnType("datetime");
            entityBuilder.HasQueryFilter(x => x.Deleted == null);
        }

    }
}
