using System;
using System.Collections.Generic;

namespace SGI_WebApi_Pauny.Models
{
    public partial class SurveyAnswer
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid SurveyItemId { get; set; }
        public string AnswerValue { get; set; }
        public DateTime? AnswerValueDateTime { get; set; }
        public Guid UserId { get; set; }

        public virtual SurveyItem SurveyItem { get; set; }
        public virtual User User { get; set; }
    }
}
