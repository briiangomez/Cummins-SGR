using System;
using System.Collections.Generic;

namespace SGRBlazorApp.Data
{
    public partial class Estado
    {
        public Estado()
        {
            EstadoIncidencias = new HashSet<EstadoIncidencia>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<EstadoIncidencia> EstadoIncidencias { get; set; }
    }
}
