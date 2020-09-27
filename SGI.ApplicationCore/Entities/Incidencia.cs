using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.ApplicationCore.Entities
{
    public class Incidencia : BaseEntity
    {
        public Guid IdDealer { get; set; }
        public long NumeroIncidencia { get; set; }
        public long NumeroOperacion { get; set; }
        public string ConfiguracionCorta { get; set; }
        public DateTime FechaPreEntrega { get; set; }
        public DateTime FechaIncidencia { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaCierre { get; set; }
        public int NroReclamoConcesionario { get; set; }
        public int NroReclamoCummins { get; set; }
        public string Descripcion { get; set; }
        public string DireccionInspeccion { get; set; }
        public float LatitudGps { get; set; }
        public float LongitudGps { get; set; }
        public string PathImagenes { get; set; }
        public int MostrarEnTv { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }
        public string Aux4 { get; set; }
        public string Aux5 { get; set; }
        public IEnumerable<Cliente> Clientes { get; set; }

        public IEnumerable<Estado> Estados { get; set; }
        public IEnumerable<EstadoGarantia> EstadoGarantias { get; set; }
        public IEnumerable<Motor> Motores { get; set; }
        public IEnumerable<Falla> Fallas { get; set; }
        public Dealer Dealer { get; set; }
    }
}
