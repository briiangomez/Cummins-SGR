using System;
using System.Collections.Generic;

namespace SGI_WebApi_Pauny.Models
{
    public partial class Certificacion
    {
        public Certificacion()
        {
            CertificacionDealers = new HashSet<CertificacionDealer>();
            CertificacionMotors = new HashSet<CertificacionMotor>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }

        public virtual ICollection<CertificacionDealer> CertificacionDealers { get; set; }
        public virtual ICollection<CertificacionMotor> CertificacionMotors { get; set; }
    }
}
