using System;
using System.Collections.Generic;

#nullable disable

namespace SGR.Models.Models
{
    public partial class Pai
    {
        public Pai()
        {
            Dealers = new HashSet<Dealer>();
            Provincia = new HashSet<Provincium>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public string Nombre { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }

        public virtual ICollection<Dealer> Dealers { get; set; }
        public virtual ICollection<Provincium> Provincia { get; set; }
    }
}
