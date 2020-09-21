using CMM.Globalization;
using CMM.Survey.ModelsDb;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Http;

namespace SGR.Communicator.Models
{
    public class SurveyReportViewModel
    {
        //public static IEnumerable<TabItem> Tabs(Guid surveyId, Guid? questionId = new Guid?())
        //{            
        //    UrlHelper helper = new UrlHelper(HttpContext.Current.Request.RequestContext);

        //    List<TabItem> list = new List<TabItem>();
        //    TabItem item = new TabItem {
        //        Value = "overview",
        //        Text = "Overview".Localize(""),
        //        Href = helper.Action("ReportOverview", "Survey", new { id = surveyId })
        //    };
        //    list.Add(item);
        //    string str = helper.Action("ReportSingle", "Survey", new { id = surveyId });
        //    if (questionId.HasValue)
        //    {
        //        str = str + "&Qid=" + questionId.Value.ToString();
        //    }
        //    TabItem item2 = new TabItem {
        //        Value = "single",
        //        Text = "Single question".Localize(""),
        //        Href = str
        //    };
        //    list.Add(item2);
        //    return list;
        //}

        public List<Controllers.SurveyReportOption> Items { get; set; }

        public int JoinCount { get; set; }

        public Survey_Question SelectedQuestion { get; set; }

        public Survey_Form Survey { get; set; }

        //public static implicit operator SurveyReportViewModel(SurveyReportViewModel v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

