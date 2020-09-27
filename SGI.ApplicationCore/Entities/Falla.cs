using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.ApplicationCore.Entities
{
    public class Falla : BaseEntity
    {
        public string Nombre { get; set; }

        public int IdFalla { get; set; }

        public long Codigo { get; set; }

        public string Observaciones { get; set; }

        public Guid IdIncidencia { get; set; }

        public Incidencia Incidencia { get; set; }
    }
}
