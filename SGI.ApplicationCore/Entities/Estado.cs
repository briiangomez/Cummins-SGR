using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.ApplicationCore.Entities
{
    public class Estado : BaseEntity
    {
        public int Codigo { get; set; }
        public Guid IdIncidencia { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public virtual Incidencia Incidencia { get; set; }
    }
}
