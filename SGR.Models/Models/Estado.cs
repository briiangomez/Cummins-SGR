using System;
using System.Collections.Generic;

#nullable disable

namespace SGR.Models.Models
{
    public partial class Estado
    {
        public Estado()
        {
            EstadoIncidencia = new HashSet<EstadoIncidencium>();
            Notificaciones = new HashSet<Notificacione>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<EstadoIncidencium> EstadoIncidencia { get; set; }
        public virtual ICollection<Notificacione> Notificaciones { get; set; }
    }
}
