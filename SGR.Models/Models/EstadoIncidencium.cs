using System;
using System.Collections.Generic;

#nullable disable

namespace SGR.Models.Models
{
    public partial class EstadoIncidencium
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid IncidenciaId { get; set; }
        public Guid EstadoId { get; set; }
        public string Observacion { get; set; }
        public Guid? IdUser { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual User IdUserNavigation { get; set; }
        public virtual Incidencia Incidencia { get; set; }
    }
}
