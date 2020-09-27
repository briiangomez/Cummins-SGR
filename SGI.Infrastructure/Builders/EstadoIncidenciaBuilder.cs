using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;
namespace SGI.Infrastructure.Builders
{
    public class EstadoIncidenciaBuilder : BaseBuilder<EstadoIncidencia>
    {
        public EstadoIncidenciaBuilder(EntityTypeBuilder<EstadoIncidencia> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("EstadoIncidencia");

            //Foreign keys

            entityBuilder
                .HasOne(a => a.Estado)
                .WithMany(b => b.EstadoIncidencias)
                .HasForeignKey(c => c.EstadoId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
                .HasOne(a => a.Incidencia)
                .WithMany(b => b.EstadoIncidencias)
                .HasForeignKey(c => c.IncidenciaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
