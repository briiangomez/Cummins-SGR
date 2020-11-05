using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sgi.Models.SgiCore
{
  [Table("Dealers", Schema = "dbo")]
  public partial class Dealer
  {
    [Key]
    public Guid Id
    {
      get;
      set;
    }

    public ICollection<Incidencia> Incidencia { get; set; }
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
    public string Name
    {
      get;
      set;
    }
    public string LocationCode
    {
      get;
      set;
    }
    public string DistributorCode
    {
      get;
      set;
    }
    public string Country
    {
      get;
      set;
    }
    public string State
    {
      get;
      set;
    }
    public string City
    {
      get;
      set;
    }
    public string Address
    {
      get;
      set;
    }
    public string Phone
    {
      get;
      set;
    }
    public string Fax
    {
      get;
      set;
    }
    public string Zip
    {
      get;
      set;
    }
    public string Email
    {
      get;
      set;
    }
    public string Website
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
    public string SkipLongitude
    {
      get;
      set;
    }
  }
}
