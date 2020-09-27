using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.Infrastructure.Builders
{
    public class EstadoGarantiaBuilder : BaseBuilder<EstadoGarantia>
    {
        public EstadoGarantiaBuilder(EntityTypeBuilder<EstadoGarantia> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("EstadoGarantia");

            entityBuilder.Property(x => x.Codigo).HasColumnName("Codigo").HasColumnType("int").IsRequired().UseMySqlIdentityColumn();
            entityBuilder.Property(x => x.Nombre).HasColumnName("Nombre").HasColumnType("varchar(200)");
            entityBuilder.Property(x => x.ObservacionesGarantia).HasColumnName("ObservacionesGarantia").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.ObservacionesProveedor).HasColumnName("ObservacionesProveedor").HasColumnType("varchar(MAX)");

            entityBuilder
                .HasOne(a => a.Incidencia)
                .WithMany(b => b.EstadoGarantias)
                .HasForeignKey(c => c.IdIncidencia)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}