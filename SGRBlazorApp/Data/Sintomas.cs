using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGRBlazorApp.Data
{
    public class Sintomas
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }
    }
}
