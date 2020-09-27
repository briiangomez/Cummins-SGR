using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.ApplicationCore.Entities
{
    public class Estado : BaseEntity
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public virtual IEnumerable<EstadoIncidencia> EstadoIncidencias { get; set; }
    }
}
