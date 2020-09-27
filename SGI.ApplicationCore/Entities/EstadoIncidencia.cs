using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.ApplicationCore.Entities
{
    public class EstadoIncidencia : BaseEntity
    {
        public Guid EstadoId { get; set; }

        public Guid IncidenciaId { get; set; }

        public string Observaciones { get; set; }

        public DateTime Fecha { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public virtual Estado Estado { get; set; }

        public virtual Incidencia Incidencia { get; set; }

    }
}
