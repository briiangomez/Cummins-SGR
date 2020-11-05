using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.ApplicationCore.Entities
{
    public class Cliente : BaseEntity
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string DNI { get; set; }
        public string TipoDNI { get; set; }
        public string Celular { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public float LatitudGpsContacto { get; set; }
        public float LongitudGpsContacto { get; set; }
        public IEnumerable<ClienteIncidencia> ClienteIncidencia { get; set; }
    }
}
