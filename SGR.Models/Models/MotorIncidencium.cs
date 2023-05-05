using System;
using System.Collections.Generic;

#nullable disable

namespace SGR.Models.Models
{
    public partial class MotorIncidencium
    {
        public Guid Id { get; set; }
        public Guid IncidenciaId { get; set; }
        public Guid MotorId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }
        public string NumeroMotor { get; set; }
        public string NumeroChasis { get; set; }
        public int? HsKm { get; set; }
        public string ModeloEquipo { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? FechaCompra { get; set; }
        public DateTime? FechaFalla { get; set; }
        public DateTime? FechaInicioGarantia { get; set; }

        public virtual Incidencia Incidencia { get; set; }
        public virtual Equipo Motor { get; set; }
    }
}
