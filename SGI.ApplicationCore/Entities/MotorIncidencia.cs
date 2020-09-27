using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.ApplicationCore.Entities
{
    public class MotorIncidencia : BaseEntity
    {
        public Guid MotorId { get; set; }

        public Guid IncidenciaId { get; set; }

        public string Observaciones { get; set; }

        public DateTime Fecha { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public virtual Motor Motor { get; set; }

        public DateTime FechaCompra { get; set; }
        public DateTime FechaInicioGarantia { get; set; }
        public DateTime FechaFalla { get; set; }

        public virtual Incidencia Incidencia { get; set; }

    }
}
