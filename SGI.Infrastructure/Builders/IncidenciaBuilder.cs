using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.Infrastructure.Builders
{
    public class IncidenciaBuilder : BaseBuilder<Incidencia>
    {
        public IncidenciaBuilder(EntityTypeBuilder<Incidencia> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("Incidencias");

            entityBuilder.Property(x => x.NumeroIncidencia).HasColumnName("NumeroIncidencia").HasColumnType("bigint").IsRequired().UseMySqlIdentityColumn();
            entityBuilder.Property(x => x.NumeroOperacion).HasColumnName("NumeroOperacion").HasColumnType("bigint").IsRequired().UseMySqlIdentityColumn();
            entityBuilder.Property(x => x.ConfiguracionCorta).HasColumnName("ConfiguracionCorta").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.FechaPreEntrega).HasColumnName("FechaPreEntrega").HasColumnType("datetime");
            entityBuilder.Property(x => x.FechaIncidencia).HasColumnName("FechaIncidencia").HasColumnType("datetime");
            entityBuilder.Property(x => x.FechaRegistro).HasColumnName("FechaRegistro").HasColumnType("datetime");
            entityBuilder.Property(x => x.FechaCierre).HasColumnName("FechaCierre").HasColumnType("datetime");
            entityBuilder.Property(x => x.NroReclamoConcesionario).HasColumnName("NroReclamoConcesionario").HasColumnType("int");
            entityBuilder.Property(x => x.NroReclamoCummins).HasColumnName("NroReclamoCummins").HasColumnType("int");
            entityBuilder.Property(x => x.Descripcion).HasColumnName("Descripcion").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.DireccionInspeccion).HasColumnName("DireccionInspeccion").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.LatitudGps).HasColumnName("LatitudGps").HasColumnType("float");
            entityBuilder.Property(x => x.LongitudGps).HasColumnName("LongitudGps").HasColumnType("float");
            entityBuilder.Property(x => x.PathImagenes).HasColumnName("PathImagenes").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.MostrarEnTv).HasColumnName("MostrarEnTv").HasColumnType("int");
            entityBuilder.Property(x => x.Aux1).HasColumnName("Aux1").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.Aux2).HasColumnName("Aux2").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.Aux3).HasColumnName("Aux3").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.Aux4).HasColumnName("Aux4").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.Aux5).HasColumnName("Aux5").HasColumnType("varchar(MAX)");

            entityBuilder
                .HasOne(a => a.Dealer)
                .WithMany(b => b.Incidencia)
                .HasForeignKey(c => c.IdDealer)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}