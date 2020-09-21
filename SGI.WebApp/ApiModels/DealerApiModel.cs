using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGI.WebApp.ApiModels
{
    public class DealerApiModel : BaseApiModel
    {
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
        public decimal LatitudGps { get; set; }
        public decimal LongitudGps { get; set; }
        public string SkipLongitude { get; set; }

        public IncidenciaApiModel[] Incidencia { get; set; }
    }
}
