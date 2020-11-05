using System;
using System.Collections.Generic;

namespace SGRBlazorApp.Data
{
    public partial class ClienteIncidencia
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid ClienteId { get; set; }
        public Guid IncidenciaId { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Incidencia Incidencia { get; set; }
    }
}
