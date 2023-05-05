using System;
using System.Collections.Generic;

#nullable disable

namespace SGR.Models.Models
{
    public partial class Incidencia
    {
        public Incidencia()
        {
            EstadoGarantia = new HashSet<EstadoGarantium>();
            EstadoIncidencia = new HashSet<EstadoIncidencium>();
            Fallas = new HashSet<Falla>();
            ImagenesIncidencia = new HashSet<ImagenesIncidencium>();
            IncidenciaSurveys = new HashSet<IncidenciaSurvey>();
            MotorIncidencia = new HashSet<MotorIncidencium>();
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
        public long NroIncidencia { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Dealer IdDealerNavigation { get; set; }
        public virtual ICollection<EstadoGarantium> EstadoGarantia { get; set; }
        public virtual ICollection<EstadoIncidencium> EstadoIncidencia { get; set; }
        public virtual ICollection<Falla> Fallas { get; set; }
        public virtual ICollection<ImagenesIncidencium> ImagenesIncidencia { get; set; }
        public virtual ICollection<IncidenciaSurvey> IncidenciaSurveys { get; set; }
        public virtual ICollection<MotorIncidencium> MotorIncidencia { get; set; }
    }
}
