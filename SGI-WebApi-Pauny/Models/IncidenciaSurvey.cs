using System;
using System.Collections.Generic;

namespace SGI_WebApi_Pauny.Models
{
    public partial class IncidenciaSurvey
    {
        public Guid Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid? IdSurvey { get; set; }
        public Guid? IdIncidencia { get; set; }
        public DateTime? Fecha { get; set; }
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }

        public virtual Incidencia IdIncidenciaNavigation { get; set; }
        public virtual Survey IdSurveyNavigation { get; set; }
    }
}
