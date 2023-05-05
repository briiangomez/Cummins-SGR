using System;
using System.Collections.Generic;

#nullable disable

namespace SGR.Models.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Incidencia = new HashSet<Incidencia>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Dni { get; set; }
        public string TipoDni { get; set; }
        public string Celular { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public double? LatitudGpsContacto { get; set; }
        public double? LongitudGpsContacto { get; set; }
        public string Contacto { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }

        public virtual ICollection<Incidencia> Incidencia { get; set; }
    }
}
