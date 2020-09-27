using SGI.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.ApplicationCore.ApiModels
{
    public class IncidenciaApiModel : BaseEntity
    {
        public Guid? IdCliente { get; set; }
        public Guid? IdConcesionario { get; set; }
        public long NumeroIncidencia { get; set; }
        public long NumeroOperacion { get; set; }
        public DateTime? FechaIncidencia { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaCierre { get; set; }
        public int NroReclamoConcesionario { get; set; }
        public int NroReclamoCummins { get; set; }
        public string Descripcion { get; set; }
        public string DireccionInspeccion { get; set; }
        public long LatitudGps { get; set; }
        public long LongitudGps { get; set; }
        public string PathImagenes { get; set; }
        public string NumeroChasis { get; set; }
        public string NumeroMotor { get; set; }
        public string ConfiguracionCorta { get; set; }
        public DateTime? FechaPreEntrega { get; set; }
        public int HorasTractor { get; set; }
        public string CodigoConcesionario { get; set; }
        public string NombreConcesionario { get; set; }
        public string EmailConcesionario { get; set; }
        public string TelefonoConcesionario { get; set; }
        public string TipoDniContacto { get; set; }
        public string NumeroDocumento { get; set; }
        public string NombreContacto { get; set; }
        public string DomicilioContacto { get; set; }
        public string LocalidadContacto { get; set; }
        public string ProvinciaContacto { get; set; }
        public string TelefonoFijoContacto { get; set; }
        public string EmailContacto { get; set; }
        public float LatitudGpsContacto { get; set; }
        public float LongitudGpsContacto { get; set; }
        public string IdFalla { get; set; }
        public string NombreFalla { get; set; }
        public string ObservacionesFalla { get; set; }
        public string IdEstadoIncidencia { get; set; }
        public string NombreEstadoIncidencia { get; set; }
        public DateTime FechaEstadoIncidencia { get; set; }
        public string IdEstadoGarantia { get; set; }
        public string NombreEstadoGarantia { get; set; }
        public string ObservacionesGarantia { get; set; }
        public string ObservacionesProveedor { get; set; }
        public int MostrarEnTv { get; set; }
    }
}
