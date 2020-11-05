using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sgi.Models.SgiCore
{
  [Table("MotorIncidencia", Schema = "dbo")]
  public partial class MotorIncidencium
  {
    [Key]
    public Guid Id
    {
      get;
      set;
    }
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
    public DateTime? FechaCompra
    {
      get;
      set;
    }
    public DateTime? FechaInicioGarantia
    {
      get;
      set;
    }
    public DateTime? FechaFalla
    {
      get;
      set;
    }
    public Guid MotorId
    {
      get;
      set;
    }
    public Motor Motor { get; set; }
    public Guid IncidenciaId
    {
      get;
      set;
    }
    public Incidencia Incidencia { get; set; }
  }
}
