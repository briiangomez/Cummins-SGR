using SGI.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGI.ApplicationCore.Entities
{
    public class IncidenciaApi : BaseEntity
    {
        public Guid? IdCliente { get; set; }
        public Guid? IdConcesionario { get; set; }
        public long numeroIncidencia { get; set; }
        public long numeroOperacion { get; set; }
        public DateTime? fechaIncidencia { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public DateTime? fechaCierre { get; set; }
        public int NroReclamoConcesionario { get; set; }
        public int NroReclamoCummins { get; set; }
        public string Descripcion { get; set; }
        public string DireccionInspeccion { get; set; }
        public long latitudGps { get; set; }
        public long longitudGps { get; set; }
        public string PathImagenes { get; set; }
        public string numeroChasis { get; set; }
        public string numeroMotor { get; set; }
        public string configuracionCorta { get; set; }
        public DateTime? fechaPreEntrega { get; set; }
        public int horasTractor { get; set; }
        public string codigoConcesionario { get; set; }
        public string nombreConcesionario { get; set; }
        public string emailConcesionario { get; set; }
        public string telefonoConcesionario { get; set; }
        public string tipoDniContacto { get; set; }
        public string numeroDocumento { get; set; }
        public string nombreContacto { get; set; }
        public string domicilioContacto { get; set; }
        public string localidadContacto { get; set; }
        public string provinciaContacto { get; set; }
        public string telefonoFijoContacto { get; set; }
        public string telefonoCelularContacto { get; set; }
        public string emailContacto { get; set; }
        public float latitudGpsContacto { get; set; }
        public float longitudGpsContacto { get; set; }
        public long idFalla { get; set; }
        public string nombreFalla { get; set; }
        public string observacionesFalla { get; set; }
        public int idEstadoIncidencia { get; set; }
        public string nombreEstadoIncidencia { get; set; }
        public DateTime fechaEstadoIncidencia { get; set; }
        public int idEstadoGarantia { get; set; }
        public string nombreEstadoGarantia { get; set; }
        public string observacionesGarantia { get; set; }
        public string observacionesProveedor { get; set; }
        public int mostrarEnTv { get; set; }
    }
}
