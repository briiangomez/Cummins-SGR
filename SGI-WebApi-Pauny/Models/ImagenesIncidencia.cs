using System;
using System.Collections.Generic;

namespace SGI_WebApi_Pauny.Models
{
    public partial class ImagenesIncidencia
    {
        public Guid Id { get; set; }
        public Guid IncidenciaId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }
        public string Imagen { get; set; }
        public string Tipo { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? FechaCarga { get; set; }

        public virtual Incidencia Incidencia { get; set; }
    }
}
