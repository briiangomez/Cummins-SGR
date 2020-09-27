using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.Infrastructure.Builders
{
    public class EstadoBuilder : BaseBuilder<Estado>
    {
        public EstadoBuilder(EntityTypeBuilder<Estado> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("Estados");

            entityBuilder.Property(x => x.Codigo).HasColumnName("Codigo").HasColumnType("int").IsRequired().UseMySqlIdentityColumn();
            entityBuilder.Property(x => x.Descripcion).HasColumnName("Descripcion").HasColumnType("varchar(MAX)");

        }
    }
}