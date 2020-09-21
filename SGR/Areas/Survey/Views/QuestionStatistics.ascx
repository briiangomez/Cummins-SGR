<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SurveyReportViewModel>" %>
<table>
    <%var title = string.Empty;
      if (Model.SelectedQuestion != null)
      {
          title = SurveyInfoViewModel.FormatQuestionTitle(Model.SelectedQuestion);
      }%>
    <caption class="breakall">
        <%if (ViewData["ShowLink"] == "true" && Model.SelectedQuestion != null)
          { %>
        <a href="<%=Url.Action("ReportSingle", new { Id = Model.Survey.Id, Qid = Model.SelectedQuestion.Id })%>">
            <%=title%></a>
        <%}
          else
          { %>
        <%=title%>
        <%} %>
    </caption>
    <thead>
        <tr>
            <th>
                <%="Options".Localize()%>
            </th>
            <th style="width: 100px;">
                <%="Subtotal".Localize()%>
            </th>
            <th style="width: 220px;">
                <%="Percentage".Localize()%>
            </th>
        </tr>
    </thead>
    <tbody>
        <%var totalCount = Model.Items.Sum(o => o.Subtotal);
          for (var i = 0; i < Model.Items.Count; i++)
          {
              var prefix = (ViewData["ShowPrefix"] == "true") ? SurveyInfoViewModel.FormatOption(i) : string.Empty;
              var item = Model.Items[i];
        %>
        <tr style="text-align: center;">
            <td >
                <%=prefix + item.OptionText%>
            </td>
            <td>
                <%=item.Subtotal%>
            </td>
            <td>
                <%var p = ViewUtility.PercentOf(item.Subtotal, totalCount).ToString("0.00").Replace(',', '.');%>
                <div class="precent-bar" title="<%=p%>%">
                    <div style="width: <%=p%>%; display: block" class="precent">
                        <img src='<%= Url.Content("~/images/vote_cl_v2.png") %>' alt="precent" width="149" style="height:12px" />
                    </div>
                </div>
                <div style="float: left;">
                    <%=ViewUtility.Percent(item.Subtotal, totalCount)%>%
                </div>
                <div style="clear: both;">
                </div>
            </td>
        </tr>
        <%} %>
    </tbody>
    <tfoot>
        <tr>
            <th>
                <%="Answer Count".Localize()%>:
            </th>
            <th>
                <%=totalCount%>
            </th>
            <th></th>
        </tr>
    </tfoot>
</table>
