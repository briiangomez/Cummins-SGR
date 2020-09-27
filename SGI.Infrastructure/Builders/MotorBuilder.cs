using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.Infrastructure.Builders
{
    public class MotorBuilder : BaseBuilder<Motor>
    {
        public MotorBuilder(EntityTypeBuilder<Motor> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("Motor");

            entityBuilder.Property(x => x.NumeroMotor).HasColumnName("NumeroMotor").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.NumeroChasis).HasColumnName("NumeroChasis").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.Modelo).HasColumnName("Modelo").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.HsKm).HasColumnName("HsKm").HasColumnType("int");
            entityBuilder.Property(x => x.Equipo).HasColumnName("Equipo").HasColumnType("varchar(MAX)");
            entityBuilder.Property(x => x.ModeloEquipo).HasColumnName("ModeloEquipo").HasColumnType("varchar(MAX)");

        }
    }
}