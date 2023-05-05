using System;
using System.Collections.Generic;

namespace SGI_WebApi_Pauny.Models
{
    public partial class Falla
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public int? IdFalla { get; set; }
        public string Codigo { get; set; }
        public string Observaciones { get; set; }
        public Guid? IdIncidencia { get; set; }
        public string Nombre { get; set; }

        public virtual Incidencia IdIncidenciaNavigation { get; set; }
    }
}
