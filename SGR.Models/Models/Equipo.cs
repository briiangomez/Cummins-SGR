using System;
using System.Collections.Generic;

#nullable disable

namespace SGR.Models.Models
{
    public partial class Equipo
    {
        public Equipo()
        {
            CertificacionMotors = new HashSet<CertificacionMotor>();
            MotorIncidencia = new HashSet<MotorIncidencium>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public string NumeroMotor { get; set; }
        public string Modelo { get; set; }
        public string Equipo1 { get; set; }
        public Guid? Oemid { get; set; }
        public Guid? MotorId { get; set; }

        public virtual Motor Motor { get; set; }
        public virtual Oem Oem { get; set; }
        public virtual ICollection<CertificacionMotor> CertificacionMotors { get; set; }
        public virtual ICollection<MotorIncidencium> MotorIncidencia { get; set; }
    }
}
