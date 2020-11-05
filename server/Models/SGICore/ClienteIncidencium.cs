using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sgi.Models.SgiCore
{
  [Table("ClienteIncidencia", Schema = "dbo")]
  public partial class ClienteIncidencium
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
    public Guid ClienteId
    {
      get;
      set;
    }
    public Cliente Cliente { get; set; }
    public Guid IncidenciaId
    {
      get;
      set;
    }
    public Incidencia Incidencia { get; set; }
  }
}
