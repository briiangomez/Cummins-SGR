using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sgi.Models.SgiCore
{
  [Table("EstadoIncidencia", Schema = "dbo")]
  public partial class EstadoIncidencium
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
    public Guid IncidenciaId
    {
      get;
      set;
    }
    public Incidencia Incidencia { get; set; }
    public Guid EstadoId
    {
      get;
      set;
    }
    public Estado Estado { get; set; }
  }
}
