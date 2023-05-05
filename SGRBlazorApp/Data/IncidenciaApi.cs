﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  SGRBlazorApp.Data
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
        public double latitudGps { get; set; }
        public double longitudGps { get; set; }
        public string PathImagenes { get; set; }
        public string Equipo { get; set; }
        public string ModeloEquipo { get; set; }
        public string ModeloMotor { get; set; }
        public string numeroChasis { get; set; }
        public string numeroMotor { get; set; }
        public DateTime? fechaCompra { get; set; }
        public DateTime? fechaInicioGarantia { get; set; }
        public DateTime? fechaFalla { get; set; }
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

        public string nombreContactoCliente { get; set; }
        public string emailContactoCliente { get; set; }
        public string telefonoContactoCliente { get; set; }
        public string domicilioContacto { get; set; }
        public string localidadContacto { get; set; }
        public string provinciaContacto { get; set; }
        public string telefonoFijoContacto { get; set; }
        public string telefonoCelularContacto { get; set; }
        public string emailContacto { get; set; }

        public double latitudGpsContacto { get; set; }
        public double longitudGpsContacto { get; set; }
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
        public string ObservacionesIncidencia { get; set; }
        public string Sintoma { get; set; }
        public string ImagenComprobante { get; set; }
        public bool EsGarantia { get; set; }
        public string Garantia { get; set; }
        public int mostrarEnTv { get; set; }
        public int codigoSintoma { get; set; }

        public Guid IdMotor { get; set; }
        public Guid IdOem { get; set; }

        public string observacionesAnulada { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }
    }
}