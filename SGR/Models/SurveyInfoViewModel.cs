namespace SGR.Communicator.Models
{
    using CMM.Globalization;
    using CMM.Survey.Models;
    using CMM.Survey.ModelsDb;
    using System;
    using System.Collections.Generic;
    public class SurveyInfoViewModel
    {
        public static string FormatOption(int index)
        {
            return (ViewUtility.Int2Excel(index + 1) + ". ");
        }

        public static string FormatPeriod(DateTime? startTime, DateTime? endTime)
        {
            string str = string.Empty;
            if (startTime.HasValue && endTime.HasValue)
            {
                return (startTime.Value.ToShortDateString() + " -- " + endTime.Value.ToShortDateString());
            }
            if (startTime.HasValue)
            {
                return ("Start At: ".Localize("") + startTime.Value.ToShortDateString());
            }
            if (endTime.HasValue)
            {
                str = "End At: ".Localize("") + endTime.Value.ToShortDateString();
            }
            return str;
        }

        public static string FormatPostSourceType(SurveyPostSource t)
        {
            if (t == SurveyPostSource.Beyond)
            {
                return "Out Page".Localize("");
            }
            if (t == SurveyPostSource.Nested)
            {
                return "Iframe Nested".Localize("");
            }
            return "Normal Form".Localize("");
        }

        public static string FormatQuestionTitle(Survey_Question q)
        {
            return FormatQuestionTitle(q.QuestionIndex, q.QuestionText);
        }

        public static string FormatQuestionTitle(int questionIndex, string questionText)
        {
            int num = questionIndex + 1;
            return (num.ToString() + ". " + questionText);
        }

        public static string Status(bool published, bool paused, DateTime? startTime, DateTime? endTime)
        {
            DateTime? nullable;
            DateTime time2;
            DateTime now = DateTime.Now;
            if (!published)
            {
                return "Unpublished".Localize("");
            }
            if (startTime.HasValue && ((nullable = startTime).HasValue ? (nullable.GetValueOrDefault() > (time2 = now)) : false))
            {
                return "Not start yet".Localize("");
            }
            if (endTime.HasValue && ((nullable = endTime).HasValue ? (nullable.GetValueOrDefault() < (time2 = now)) : false))
            {
                return "Expired".Localize("");
            }
            return "Available".Localize("");
        }

        public static IEnumerable<TabItem> Tabs(Guid surveyId)
        {
            //UrlHelper helper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            //TabItem[] itemArray = new TabItem[2];
            //TabItem item = new TabItem {
            //    Value = "survey",
            //    Text = "Survey".Localize(""),
            //    Href = (surveyId == Guid.Empty) ? string.Empty : helper.Action("Builder", "Survey", new { id = surveyId })
            //};
            //itemArray[0] = item;
            //TabItem item2 = new TabItem {
            //    Value = "publish",
            //    Text = "Publish".Localize(""),
            //    Href = (surveyId == Guid.Empty) ? string.Empty : helper.Action("Publish", "Survey", new { id = surveyId })
            //};
            //itemArray[1] = item2;
            //return itemArray;

            return null;
        }

        public DateTime? EndTime { get; set; }

        public bool IsNew { get; set; }

        public string JoinPassword { get; set; }

        public bool? Paused { get; set; }

        public bool Published { get; set; }

        public bool RespondResult { get; set; }

        public DateTime? StartTime { get; set; }

        public Guid SurveyId { get; set; }

        public string SurveyName { get; set; }

        public string ValidatorType { get; set; }
    }
}

