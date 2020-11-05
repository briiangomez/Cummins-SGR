using System;
using System.Collections.Generic;

namespace SGRBlazorApp.Data
{
    public partial class Incidencia
    {
        public Incidencia()
        {
            EstadoGarantias = new HashSet<EstadoGarantia>();
            EstadoIncidencias = new HashSet<EstadoIncidencia>();
            Fallas = new HashSet<Falla>();
            MotorIncidencias = new HashSet<MotorIncidencia>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public long NumeroIncidencia { get; set; }
        public long NumeroOperacion { get; set; }
        public string ConfiguracionCorta { get; set; }
        public DateTime? FechaPreEntrega { get; set; }
        public DateTime? FechaIncidencia { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaCierre { get; set; }
        public int? NroReclamoConcesionario { get; set; }
        public int? NroReclamoCummins { get; set; }
        public string Equipo { get; set; }
        public string ModeloEquipo { get; set; }
        public string Descripcion { get; set; }
        public string DireccionInspeccion { get; set; }
        public double? LatitudGps { get; set; }
        public double? LongitudGps { get; set; }
        public string PathImagenes { get; set; }
        public int? MostrarEnTv { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }
        public string Sintoma { get; set; }
        public string ImagenComprobante { get; set; }
        public bool? EsGarantia { get; set; }
        public string Aux4 { get; set; }
        public string Aux5 { get; set; }
        public Guid? IdDealer { get; set; }
        public string NroIncidenciaPauny { get; set; }
        public Guid? IdCliente { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Dealer IdDealerNavigation { get; set; }
        public virtual ICollection<EstadoGarantia> EstadoGarantias { get; set; }
        public virtual ICollection<EstadoIncidencia> EstadoIncidencias { get; set; }
        public virtual ICollection<Falla> Fallas { get; set; }
        public virtual ICollection<MotorIncidencia> MotorIncidencias { get; set; }
    }
}
