<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SurveyReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    <%="Statistics Of Survey: ".Localize() + Model.Survey.FormName%>
    <%Html.RenderPartial("Header");%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Tab("source", SurveyReportViewModel.Tabs(Model.Survey.Id))%>
    <div class="tab-content">
        <div class="common-form">
            <h2>
                <%="Post Source Statistics:".Localize()%>
            </h2>
        </div>
        <hr />
        <%Html.RenderPartial("ReportChart");%>
        <div class="table-container clearfix">
            <table>
                <thead>
                    <tr>
                        <th>
                            <%="Source".Localize()%>
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
                    <tr>
                        <td>
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
                    <%}%>
                </tbody>
                <tfoot>
                    <tr>
                        <th>
                            <%="Total Data Count".Localize()%>:
                        </th>
                        <th colspan="2">
                            <%=totalCount%>
                        </th>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>
