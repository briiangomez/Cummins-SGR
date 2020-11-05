using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sgi.Models.SgiCore
{
  [Table("Clientes", Schema = "dbo")]
  public partial class Cliente
  {
    [Key]
    public Guid Id
    {
      get;
      set;
    }

    public ICollection<ClienteIncidencium> ClienteIncidencia { get; set; }
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
    public string Nombre
    {
      get;
      set;
    }
    public string Telefono
    {
      get;
      set;
    }
    public string Email
    {
      get;
      set;
    }
    public string Direccion
    {
      get;
      set;
    }
    public string DNI
    {
      get;
      set;
    }
    public string TipoDNI
    {
      get;
      set;
    }
    public string Celular
    {
      get;
      set;
    }
    public string Localidad
    {
      get;
      set;
    }
    public string Provincia
    {
      get;
      set;
    }
    public double? LatitudGpsContacto
    {
      get;
      set;
    }
    public double? LongitudGpsContacto
    {
      get;
      set;
    }
  }
}
