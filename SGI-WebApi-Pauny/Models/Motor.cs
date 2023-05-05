using System;
using System.Collections.Generic;

namespace SGI_WebApi_Pauny.Models
{
    public partial class Motor
    {
        public Motor()
        {
            CertificacionMotors = new HashSet<CertificacionMotor>();
            MotorIncidencias = new HashSet<MotorIncidencia>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public string NumeroMotor { get; set; }
        public string Modelo { get; set; }
        public string Equipo { get; set; }
        public Guid? Oemid { get; set; }

        public virtual Oem Oem { get; set; }
        public virtual ICollection<CertificacionMotor> CertificacionMotors { get; set; }
        public virtual ICollection<MotorIncidencia> MotorIncidencias { get; set; }
    }
}
