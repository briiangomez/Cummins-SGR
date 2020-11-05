using System;
using System.Collections.Generic;

namespace SGRBlazorApp.Data
{
    public partial class Cliente
    {
        public Cliente()
        {
            Incidencias = new HashSet<Incidencia>();
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

        public virtual ICollection<Incidencia> Incidencias { get; set; }
    }
}
