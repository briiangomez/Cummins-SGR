using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.ApplicationCore.Entities
{
    public class ClienteIncidencia : BaseEntity
    {
        public Guid ClienteId { get; set; }

        public Guid IncidenciaId { get; set; }

        public string Observaciones { get; set; }

        public DateTime Fecha { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Incidencia Incidencia { get; set; }

    }
}
