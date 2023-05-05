using System;
using System.Collections.Generic;

namespace SGI_WebApi_Pauny.Models
{
    public partial class SurveyItem
    {
        public SurveyItem()
        {
            SurveyAnswers = new HashSet<SurveyAnswer>();
            SurveyItemOptions = new HashSet<SurveyItemOption>();
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid Survey { get; set; }
        public string ItemLabel { get; set; }
        public string ItemType { get; set; }
        public string ItemValue { get; set; }
        public int Position { get; set; }
        public int Required { get; set; }
        public int? SurveyChoiceId { get; set; }

        public virtual Survey SurveyNavigation { get; set; }
        public virtual ICollection<SurveyAnswer> SurveyAnswers { get; set; }
        public virtual ICollection<SurveyItemOption> SurveyItemOptions { get; set; }
    }
}
