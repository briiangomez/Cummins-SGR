using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.Infrastructure.Builders
{
    public class FallaBuilder : BaseBuilder<Falla>
    {
        public FallaBuilder(EntityTypeBuilder<Falla> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("Falla");

            entityBuilder.Property(x => x.Codigo).HasColumnName("Codigo").HasColumnType("varchar(100)");
            entityBuilder.Property(x => x.IdFalla).HasColumnName("IdFalla").HasColumnType("int");
            entityBuilder.Property(x => x.Observaciones).HasColumnName("Address").HasColumnType("varchar(MAX)");

            entityBuilder
                .HasOne(a => a.Incidencia)
                .WithMany(b => b.Fallas)
                .HasForeignKey(c => c.IdIncidencia)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}