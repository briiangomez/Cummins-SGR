using System;
using System.Collections.Generic;

#nullable disable

namespace SGR.Models.Models
{
    public partial class ImagenesIncidencium
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
