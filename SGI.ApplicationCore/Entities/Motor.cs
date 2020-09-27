using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.ApplicationCore.Entities
{
    public class Motor : BaseEntity
    {
        public string NumeroMotor { get; set; }
        public string NumeroChasis { get; set; }
        public string Modelo { get; set; }
        public int HsKm { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime FechaInicioGarantia { get; set; }
        public DateTime FechaFalla { get; set; }
        public string Equipo { get; set; }
        public string ModeloEquipo { get; set; }

        public Guid IdIncidencia { get; set; }

        public Incidencia Incidencia { get; set; }
    }
}
