using System;
using System.Collections.Generic;

#nullable disable

namespace SGR.Models.Models
{
    public partial class SurveyItemOption
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid SurveyItem { get; set; }
        public string OptionLabel { get; set; }

        public virtual SurveyItem SurveyItemNavigation { get; set; }
    }
}
