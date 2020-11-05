using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.Infrastructure.Builders
{
    public class ClienteBuilder : BaseBuilder<Cliente>
    {
        public ClienteBuilder(EntityTypeBuilder<Cliente> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("Clientes");

            entityBuilder.Property(x => x.Nombre).HasColumnName("Nombre").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.Telefono).HasColumnName("Telefono").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.Email).HasColumnName("Email").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.Direccion).HasColumnName("Direccion").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.DNI).HasColumnName("DNI").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.TipoDNI).HasColumnName("TipoDNI").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.Celular).HasColumnName("Celular").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.Localidad).HasColumnName("Localidad").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.Provincia).HasColumnName("Provincia").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.LatitudGpsContacto).HasColumnName("LatitudGpsContacto").HasColumnType("float");
            entityBuilder.Property(x => x.LongitudGpsContacto).HasColumnName("LongitudGpsContacto").HasColumnType("float");

        }
    }
}
