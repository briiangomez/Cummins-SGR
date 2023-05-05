using System;
using System.Collections.Generic;

#nullable disable

namespace SGR.Models.Models
{
    public partial class Oem
    {
        public Oem()
        {
            Equipos = new HashSet<Equipo>();
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }

        public virtual ICollection<Equipo> Equipos { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
