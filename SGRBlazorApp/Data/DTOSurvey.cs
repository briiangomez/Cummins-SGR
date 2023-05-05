using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  SGRBlazorApp.Data
{
    public class DTOSurvey
    {
        public Guid Id { get; set; }
        public string SurveyName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Guid UserId { get; set; }
        public List<DTOSurveyItem> SurveyItem { get; set; }
    }
    public class DTOSurveyItem
    {
        public Guid Id { get; set; }
        public string ItemLabel { get; set; }
        public string ItemType { get; set; }
        public string ItemValue { get; set; }
        public int Position { get; set; }
        public int Required { get; set; }
        public int? SurveyChoiceId { get; set; }
        public string AnswerValueString { get; set; }
        public IEnumerable<string> AnswerValueList { get; set; }
        public DateTime? AnswerValueDateTime { get; set; }
        public List<DTOSurveyItemOption> SurveyItemOption { get; set; }
        public List<AnswerResponse> AnswerResponses { get; set; }
    }
    public partial class DTOSurveyItemOption
    {
        public Guid Id { get; set; }
        public string OptionLabel { get; set; }
    }
    public class AnswerResponse
    {
        public string OptionLabel { get; set; }
        public double Responses { get; set; }
    }
}
