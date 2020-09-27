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
        public string Equipo { get; set; }
        public string ModeloEquipo { get; set; }
        public IEnumerable<MotorIncidencia> MotorIncidencias { get; set; }
    }
}
