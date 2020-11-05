using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sgi.Models.SgiCore
{
  [Table("Incidencias", Schema = "dbo")]
  public partial class Incidencia
  {
    [Key]
    public Guid Id
    {
      get;
      set;
    }

    public ICollection<EstadoGarantium> EstadoGarantia { get; set; }
    public ICollection<Falla> Fallas { get; set; }
    public ICollection<ClienteIncidencium> ClienteIncidencia { get; set; }
    public ICollection<MotorIncidencium> MotorIncidencia { get; set; }
    public ICollection<EstadoIncidencium> EstadoIncidencia { get; set; }
    public DateTime Created
    {
      get;
      set;
    }
    public DateTime? Modified
    {
      get;
      set;
    }
    public DateTime? Deleted
    {
      get;
      set;
    }
    public Int64 NumeroIncidencia
    {
      get;
      set;
    }
    public Int64 NumeroOperacion
    {
      get;
      set;
    }
    public string ConfiguracionCorta
    {
      get;
      set;
    }
    public DateTime? FechaPreEntrega
    {
      get;
      set;
    }
    public DateTime? FechaIncidencia
    {
      get;
      set;
    }
    public DateTime? FechaRegistro
    {
      get;
      set;
    }
    public DateTime? FechaCierre
    {
      get;
      set;
    }
    public int? NroReclamoConcesionario
    {
      get;
      set;
    }
    public int? NroReclamoCummins
    {
      get;
      set;
    }
    public string Equipo
    {
      get;
      set;
    }
    public string ModeloEquipo
    {
      get;
      set;
    }
    public string Descripcion
    {
      get;
      set;
    }
    public string DireccionInspeccion
    {
      get;
      set;
    }
    public double? LatitudGps
    {
      get;
      set;
    }
    public double? LongitudGps
    {
      get;
      set;
    }
    public string PathImagenes
    {
      get;
      set;
    }
    public int? MostrarEnTv
    {
      get;
      set;
    }
    public string Aux1
    {
      get;
      set;
    }
    public string Aux2
    {
      get;
      set;
    }
    public string Aux3
    {
      get;
      set;
    }
    public string Sintoma
    {
      get;
      set;
    }
    public string ImagenComprobante
    {
      get;
      set;
    }
    public bool? EsGarantia
    {
      get;
      set;
    }
    public string Aux4
    {
      get;
      set;
    }
    public string Aux5
    {
      get;
      set;
    }
    public Guid? IdDealer
    {
      get;
      set;
    }
    public Dealer Dealer { get; set; }
  }
}
