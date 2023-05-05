using System;
using System.Collections.Generic;

namespace SGI_WebApi_Pauny.Models
{
    public partial class CertificacionDealer
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid CertificacionId { get; set; }
        public Guid DealerId { get; set; }

        public virtual Certificacion Certificacion { get; set; }
        public virtual Dealer Dealer { get; set; }
    }
}
