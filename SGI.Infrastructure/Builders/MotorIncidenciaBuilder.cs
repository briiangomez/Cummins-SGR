using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;

namespace SGI.Infrastructure.Builders
{
    public class MotorIncidenciaBuilder : BaseBuilder<MotorIncidencia>
    {
        public MotorIncidenciaBuilder(EntityTypeBuilder<MotorIncidencia> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("MotorIncidencia");
            entityBuilder.Property(x => x.FechaCompra).HasColumnName("FechaCompra").HasColumnType("datetime");
            entityBuilder.Property(x => x.FechaInicioGarantia).HasColumnName("FechaInicioGarantia").HasColumnType("datetime");
            entityBuilder.Property(x => x.FechaFalla).HasColumnName("FechaFalla").HasColumnType("datetime");


            //Foreign keys

            entityBuilder
                .HasOne(a => a.Motor)
                .WithMany(b => b.MotorIncidencias)
                .HasForeignKey(c => c.MotorId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
                .HasOne(a => a.Incidencia)
                .WithMany(b => b.MotorIncidencias)
                .HasForeignKey(c => c.IncidenciaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
