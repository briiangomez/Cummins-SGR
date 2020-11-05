using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sgi.Models.SgiCore
{
  [Table("Estados", Schema = "dbo")]
  public partial class Estado
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id
    {
      get;
      set;
    }

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
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo
    {
      get;
      set;
    }
    public string Descripcion
    {
      get;
      set;
    }
  }
}
