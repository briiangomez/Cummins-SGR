using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.ApplicationCore.Entities
{
    public class EstadoGarantia : BaseEntity
    {
        public Guid IdIncidencia { get; set; }

        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string ObservacionesGarantia { get; set; }
        public string ObservacionesProveedor { get; set; }
        public Incidencia Incidencia { get; set; }
    }
}
