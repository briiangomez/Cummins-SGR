<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<SurveyReportViewModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    <script type="text/javascript">
        document.getElementById("nav6").className = "active";
        document.getElementById("nav35").className = "dropdown active";
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<div class="container text_center">
    <h1 class="text_black"><%= "Report".Localize()%></h1>
        
    <div class="col-md-10 align_center noFloat contentBox padding20">
        <div class="col-md-12 noPadding text_left">
            <div class="row">
                <%Html.RenderPartial("Header");%>
            </div>
        <h3 class="text_left mTop20"><%="Report: ".Localize() + ((CMM.Survey.ModelsDb.Survey_Form)ViewData["Survey"]).FormName%></h3>
        <%var survey = (CMM.Survey.ModelsDb.Survey_Form)ViewData["Survey"]; %>
        <%=Html.Tab("overview", SurveyReportViewModel.Tabs(survey.Id))%>
        <div class="tab-content" style="overflow: hidden">
            <div class="common-form">
                <table cellpadding ="3">
                    <tr>
                        <th>
                            <label>
                                <%="Survey".Localize()%>:</label>
                        </th>
                        <td>
                            <label>
                                <a href="<%=Url.Action("Detail", "Survey", new { Id = survey.Id })%>">
                                    <%=survey.FormName%></a></label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>
                                <%="Survey status".Localize()%>:</label>
                        </th>
                        <td>
                            <label>
                                <%=SurveyInfoViewModel.Status((survey.PublishTime != null), (survey.Paused == true), survey.StartTime, survey.EndTime)%>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>
                                <%="Open count".Localize()%>:</label>
                        </th>
                        <td>
                            <label>
                                <%=survey.VisitCount%>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>
                                <%="Join count".Localize()%>:</label>
                        </th>
                        <td>
                            <label>
                                <%var joinCount = (int)ViewData["JoinCount"];%>
                                <%=joinCount%>
                            (<%=ViewUtility.Percent(joinCount, survey.VisitCount)%>%)
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>
                                <%="Export report".Localize()%></label>
                        </th>
                        <td>
                            <label>
                                <form method="post" action="<%=Url.Action("ReportExport")%>">
                                    <input type="hidden" name="Id" value="<%=survey.Id%>" />
                                    <input type="submit" class="btn btn-primary" value="Exportar" />
                                </form>
                            </label>
                        </td>
                    </tr>
                </table>
            </div>
            <hr />
            <%if (Model.Count > 0)
                {%>
            <div style="text-align: center;">
                <h3 class="breakall">
                    <%=ViewData["SurveyTitle"]%>
                </h3>
            </div>
            <%foreach (var item in Model)
                { %>
            <div class="table-container clearfix">
                <%Html.RenderPartial("QuestionStatistics", item);%>
            </div>
            <%}%>
            <%}
                else
                { %>
            <%Html.RenderPartial("QuestionRequired", new SurveyReportViewModel() { Survey = survey });%>
            <%} %>
        </div>
    </div>
</div>
</asp:Content>
