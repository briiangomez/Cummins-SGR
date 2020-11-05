using System;
using System.Collections.Generic;

namespace SGIWebApi.Models
{
    public partial class EstadoGarantia
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string ObservacionesGarantia { get; set; }
        public string ObservacionesProveedor { get; set; }
        public Guid? IdIncidencia { get; set; }

        public virtual Incidencia IdIncidenciaNavigation { get; set; }
    }
}
