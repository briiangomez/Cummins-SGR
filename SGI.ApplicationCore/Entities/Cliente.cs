using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.ApplicationCore.Entities
{
    public class Cliente : BaseEntity
    {
        public Guid IdIncidencia { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string NombreContacto { get; set; }
        public string DNIContacto { get; set; }
        public string TipoDNIContacto { get; set; }
        public string TelContacto { get; set; }
        public string CelContacto { get; set; }
        public string EmailContacto { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public float LatitudGpsContacto { get; set; }
        public float LongitudGpsContacto { get; set; }
        public Incidencia Incidencia { get; set; }
    }
}
