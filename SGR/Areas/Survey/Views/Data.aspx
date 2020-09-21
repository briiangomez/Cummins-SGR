<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ListModel<CMM.Survey.ModelsDb.Survey_PostData>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
     <script type="text/javascript">
        document.getElementById("nav6").className = "active";
        document.getElementById("nav35").className = "dropdown active";
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
       <script type="text/javascript">
        $(function () {
            $('a[_btndel=1]').click(function () {
                return confirm('Está seguro de eliminar?');
            });
        });
        function doexport(id) {
            document.getElementById('exportid').value = id;
            document.getElementById('exportform').submit();
        }
    </script>

<div class="container text_center">
    <h1 class="text_black"><%= "Survey".Localize()%></h1>
        
    <div class="col-md-10 align_center noFloat contentBox padding20">
        <div class="col-md-12 noPadding text_left">
            <div class="row">
                <%Html.RenderPartial("Header");%>
                <a class="btn btn-primary floatRight mLeft10" href="javascript:;" onclick="doexport('<%=ViewData["SurveyId"]%>');return false;"><%="Export to CSV".Localize()%></a>
                <a class="btn btn-secondary floatRight " href="<%=Url.Action("ReportOverview", new { Id = ViewData["SurveyId"] })%>"><%="View report".Localize()%></a> 
            </div>
            <h3 class="text_left mTop20"><%="Data: ".Localize() + ViewData["SurveyName"].ToString()%></h3>
            <form id="exportform" method="post" action="<%=Url.Action("ExportCsv")%>">
            <input type="hidden" name="Id" id="exportid" />
            </form>

            <br />
            <div class="table-container clearfix" style="text-align:center">
                <table>
                    <thead>
                        <tr>
                            <th>
                                <%= "Post Date".Localize()%>
                            </th>
                            <%--<th class="common">
                                <%="Contact".Localize()%>
                            </th>
                            <th class="common">
                                <%="Is Anonymity".Localize()%>
                            </th>--%>
                            <th class="common">
                                <%="Post IP".Localize()%>
                            </th>
                            <th class="common">
                                <%="Post Source".Localize()%>
                            </th>
                            <th class="action">
                                <%="Review".Localize()%>
                            </th>
                            <th class="action">
                                <%="Detail".Localize()%>
                            </th>
                            <th class="action">
                                <%="Delete".Localize()%>
                            </th>
                        </tr>
                    </thead>
                    <tfoot class="table-command">
                        <tr>
                            <th colspan="7">
                                <div class="left">
                                </div>
                                <div class="right">
                                    <%= Html.Partial("PageSize") %>
                                </div>
                            </th>
                        </tr>
                    </tfoot>
                    <tbody>
                        <% foreach (var item in Model.Items)
                           { %>
                        <tr>
                            <td>
                                <%=item.PostDate%>
                            </td>
                            <td>
                                <%=item.ClientIP%>
                            </td>
                            <td>
                                <%=SurveyInfoViewModel.FormatPostSourceType(item.SourceTypeEnum)%>
                            </td>
                            <td>
                                <a href="<%=Url.Action("DataReview", new { FormId = item.FormId, DataId = item.Id })%>"
                                    target="_blank">
                                    <img src="<%=Url.Content("~/images/icon_preview.png")%>" alt="<%= "Review".Localize() %>" /></a>
                            </td>
                            <td class="action">
                                <a href="<%=Url.Action("DataDetail", new { Id = item.Id })%>">
                                    <%= Html.EditImage()%></a>
                            </td>
                            <td class="action">
                                <a href="<%=Url.Action("DeleteData", new { FormId = item.FormId, DataId = item.Id })%>"
                                    _btndel="1">
                                    <%= Html.DeleteImage()%></a>
                            </td>
                        </tr>
                        <%} %>
                    </tbody>
                </table>
                <%= Html.Partial("Pager") %>
            </div>
        </div>
    </div>
</asp:Content>
