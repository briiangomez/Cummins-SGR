using System;
using System.Collections.Generic;

namespace SGRBlazorApp.Data
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

        public virtual ICollection<CertificacionMotor> CertificacionMotors { get; set; }
        public virtual ICollection<MotorIncidencia> MotorIncidencias { get; set; }
    }
}
