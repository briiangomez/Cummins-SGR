using System;
using System.Collections.Generic;

#nullable disable

namespace SGR.Models.Models
{
    public partial class Survey
    {
        public Survey()
        {
            IncidenciaSurveys = new HashSet<IncidenciaSurvey>();
            SurveyItems = new HashSet<SurveyItem>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public string SurveyName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<IncidenciaSurvey> IncidenciaSurveys { get; set; }
        public virtual ICollection<SurveyItem> SurveyItems { get; set; }
    }
}
