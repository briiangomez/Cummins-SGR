using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sgi.Models.SgiCore
{
  [Table("Motor", Schema = "dbo")]
  public partial class Motor
  {
    [Key]
    public Guid Id
    {
      get;
      set;
    }

    public ICollection<MotorIncidencium> MotorIncidencia { get; set; }
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
    public string NumeroMotor
    {
      get;
      set;
    }
    public string NumeroChasis
    {
      get;
      set;
    }
    public string Modelo
    {
      get;
      set;
    }
    public int? HsKm
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
  }
}
