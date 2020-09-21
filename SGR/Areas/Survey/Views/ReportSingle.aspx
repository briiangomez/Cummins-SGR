<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SurveyReportViewModel>" %>

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
            <h3 class="text_left mTop20"><%="Report: ".Localize() + Model.Survey.FormName%></h3>

            <%=Html.Tab("single", SurveyReportViewModel.Tabs(Model.Survey.Id))%>
            <div class="tab-content" style="overflow: hidden">
                <%if (Model.Survey.Survey_Question.Count > 0)
                  { %>
                <div class="common-form" >
                    <form id="singleform" method="get" action="<%=Url.Action("ReportSingle")%>">
                    <input type="hidden" name="Id" value="<%=Model.Survey.Id%>" />
                    <table >
                        <tbody>
                            <tr>
                                <th><label><%="Choose a question".Localize()%>:</label></th>
                                <td>
                                    <select name="Qid" style="float: none;">
                                        <%foreach (var q in Model.Survey.Survey_Question)
                                          {
                                              var selected = (q.Id == Model.SelectedQuestion.Id) ? "selected=\"selected\"" : string.Empty;
                                        %>
                                        <option value="<%=q.Id%>" <%=selected%>>
                                            <%=SurveyInfoViewModel.FormatQuestionTitle(q)%>
                                        </option>
                                        <%} %>
                                    </select>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    </form>
                </div>
                <%Html.RenderPartial("ReportChart");%>
                <div class="table-container clearfix">
                    <%Html.RenderPartial("QuestionStatistics");%>
                </div>
                <%}
                  else
                  { %>
                <%Html.RenderPartial("QuestionRequired");%>
                <%} %>
            </div>
        </div>
    </div>
</div>
</asp:Content>
