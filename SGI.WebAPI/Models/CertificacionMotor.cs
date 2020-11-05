using System;
using System.Collections.Generic;

namespace SGIWebApi.Models
{
    public partial class CertificacionMotor
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid MotorId { get; set; }
        public Guid CertificacionId { get; set; }

        public virtual Certificacion Certificacion { get; set; }
        public virtual Motor Motor { get; set; }
    }
}
