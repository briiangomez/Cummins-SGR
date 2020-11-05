using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sgi.Models.SgiCore
{
  [Table("EstadoGarantia", Schema = "dbo")]
  public partial class EstadoGarantium
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo
    {
      get;
      set;
    }
    public string Nombre
    {
      get;
      set;
    }
    public string ObservacionesGarantia
    {
      get;
      set;
    }
    public string ObservacionesProveedor
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
