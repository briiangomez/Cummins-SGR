using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGI.ApplicationCore.Entities;
namespace SGI.Infrastructure.Builders
{
    public class ClienteIncidenciaBuilder : BaseBuilder<ClienteIncidencia>
    {
        public ClienteIncidenciaBuilder(EntityTypeBuilder<ClienteIncidencia> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.ToTable("ClienteIncidencia");

            //Foreign keys

            entityBuilder
                .HasOne(a => a.Cliente)
                .WithMany(b => b.ClienteIncidencia)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
                .HasOne(a => a.Incidencia)
                .WithMany(b => b.ClienteIncidencia)
                .HasForeignKey(c => c.IncidenciaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
