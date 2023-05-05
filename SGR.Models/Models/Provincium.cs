using System;
using System.Collections.Generic;

#nullable disable

namespace SGR.Models.Models
{
    public partial class Provincium
    {
        public Provincium()
        {
            Dealers = new HashSet<Dealer>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public string Nombre { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }
        public Guid? PaisId { get; set; }

        public virtual Pai Pais { get; set; }
        public virtual ICollection<Dealer> Dealers { get; set; }
    }
}
