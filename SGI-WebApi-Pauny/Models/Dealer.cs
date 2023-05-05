using System;
using System.Collections.Generic;

namespace SGI_WebApi_Pauny.Models
{
    public partial class Dealer
    {
        public Dealer()
        {
            CertificacionDealers = new HashSet<CertificacionDealer>();
            Incidencias = new HashSet<Incidencia>();
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public string Name { get; set; }
        public string LocationCode { get; set; }
        public string DistributorCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public double? LatitudGps { get; set; }
        public double? LongitudGps { get; set; }
        public string SkipLongitude { get; set; }
        public Guid? IdUser { get; set; }
        public string Contacto { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }

        public virtual ICollection<CertificacionDealer> CertificacionDealers { get; set; }
        public virtual ICollection<Incidencia> Incidencias { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
