using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sgi.Models.SgiCore
{
  [Table("Falla", Schema = "dbo")]
  public partial class Falla
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
    public int? IdFalla
    {
      get;
      set;
    }
    public string Codigo
    {
      get;
      set;
    }
    public string Observaciones
    {
      get;
      set;
    }
    public Guid? IdIncidencia
    {
      get;
      set;
    }
    public Incidencia Incidencia { get; set; }
  }
}
