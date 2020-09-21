<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ListModel<CMM.Survey.ModelsDb.Survey_Form>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<h1 class="text_center text_black"><%= "Survey List".Localize()%></h1>
<div class="container text_center" id="encuestas">        
    <div class="col-md-12 align_center noFloat contentBox">
        <div class="col-md-12 noPadding">
            <div class="innerBox">
                <% if (Model.Items.Count() == 0)
                    { %>

                        <div class="row emptystate">
                            <img src="../../images/EmptyStates/no-survey.jpg" alt="Crea una nueva campaña" height="250" />
                            <h4>Parece que aún no has creado ninguna encuesta.</h4>
                            <h5 class="text_grey mLeft0">¿Deseas hacerlo ahora?</h5>
                            <p class="pBottom0">
                                <a class="btn-primary show-form" href="/Survey/Detail?IsNew=True">Crear una encuesta</a>
                            </p>
                        </div>

                        <% }
                        else
                        { %>
                    <form method="post" action="<%=Url.Action("Index")%>">
                        <%Html.RenderPartial("IndexAction");%>
                        <div class="table-container clearfix">
                            <table class="table table-striped table-hover">
                                <thead>
                                    <tr>
                                        <th class="common" style="width: 20px;">
                                            <input type="checkbox" class="select-all" title='<%= "Select All".Localize() %>' />
                                        </th>
                                        <th class="common">
                                            <%= "Name".Localize() %>
                                        </th>
                                        <th class="common">
                                            <%= "Status".Localize()%>
                                        </th>
                                        <th class="common" style="min-width: 130px;">
                                            <%= "Create time".Localize()%>
                                        </th>
                                        <th class="common" style="min-width: 140px;">
                                            <%= "Available period".Localize()%>
                                        </th>
                                        <th class="action"><%="Edit".Localize()%></th>
                                        <th class="action"><%="Publish".Localize()%></th>
                                        <th class="action"><%="Data".Localize()%></th>
                                        <th class="action"><%="Report".Localize()%></th>
                                        <th class="action"><%="Copy".Localize()%></th>
                                        <th class="action"><%="Delete".Localize()%></th>
                                    </tr>
                                </thead>
                                <tfoot class="table-command">
                                    <tr>
                                        <th colspan="4">
                                            <div class="floatLeft text_left">
                                                <%= Html.Partial("PageSize") %>
                                            </div>
                                        </th>
                                    </tr>
                                </tfoot>
                                <tbody>
                                    <% foreach (var item in Model.Items)
                                        {
                                            var Published = (item.PublishTime != null);
                                    %>
                                    <tr>
                                        <td>
                                            <input type="checkbox" name="Selected" value="<%=item.Id %>" />
                                        </td>
                                        <td>
                                            <a href="<%=Url.Action("Detail", new { Id = item.Id })%>">
                                                <%=item.FormName%></a>
                                        </td>
                                        <td>
                                            <%=SurveyInfoViewModel.Status(Published, (item.Paused == true), item.StartTime, item.EndTime)%>
                                        </td>
                                        <td>
                                            <%=item.CreateTime%>
                                        </td>
                                        <td>
                                            <%=SurveyInfoViewModel.FormatPeriod(item.StartTime, item.EndTime)%>
                                        </td>
                                        <td class="action">
                                            <a href="<%=Url.Action("Builder", new { Id = item.Id })%>">
                                                <%--<img src='<%= Url.Content("~/images/icon_edit.png") %>' alt="<%= "Edit".Localize() %>" title="<%="Edit".Localize()%>" />--%>
                                                <i class="icon-edit"></i></a>

                                        </td>
                                        <td class="action">
                                            <%if (Published)
                                                {%>
                                            <a href="<%=Url.Action("Distribute", new { Id = item.Id })%>">
                                                <%--<img src='<%= Url.Content("~/images/ok.png") %>' alt="<%="Published".Localize()%>" title="<%="Published".Localize()%>" /></a>--%>
                                                <i class="icon-ok"></i>
                                            </a>
                                            <%}
                                                else
                                                { %>
                                            <a href="<%=Url.Action("Distribute", new { Id = item.Id })%>">
                                                <%--<img src='<%= Url.Content("~/images/forward.png") %>' style="height: 21px" alt="<%= "Publish".Localize() %>" title="<%="Click to publish".Localize()%>" />--%>
                                                <i class="icon-share"></i>
                                            </a>
                                            <%} %>
                                        </td>
                                        <td class="action">
                                            <a href="<%=Url.Action("Data", new { Id = item.Id })%>">
                                                <%--<img src='<%= Url.Content("~/images/preferences.png") %>' style="height: 21px" alt="<%= "Submitted data".Localize() %>" title="<%="Data".Localize()%>" />--%>
                                                <i class="icon-tasks"></i>
                                            </a>
                                        </td>
                                        <td class="action">
                                            <a href="<%=Url.Action("ReportOverview", new { Id = item.Id })%>">
                                                <%--<img src='<%= Url.Content("~/images/icon_preview.png") %>' alt="<%= "Report".Localize() %>" title="<%="Report".Localize()%>" />--%>
                                                <i class="icon-eye-open"></i>
                                            </a>
                                        </td>
                                        <td class="action">
                                            <a href="<%=Url.Action("Copy", new { Id = item.Id })%>">
                                                <%--<img src='<%= Url.Content("~/images/icon_copy.png") %>' alt="<%= "Copy".Localize() %>" title="<%="Copy".Localize()%>" />--%>
                                                <i class="icon-file"></i>
                                            </a>
                                        </td>
                                        <td class="action">
                                            <a href="<%=Url.Action("Deletion", new { Id = item.Id })%>" title="<%="Delete".Localize()%>">
                                                <%--<img src='<%= Url.Content("~/images/icon_delete.png") %>' alt="<%= "Delete".Localize() %>" title="<%="Delete".Localize()%>" />--%>
                                                <i class="icon-trash"></i>
                                            </a>
                                        </td>
                                    </tr>
                                    <%} %>
                                </tbody>
                            </table>
                            <%= Html.Partial("Pager") %>
                        </div>
                        <%if (Model.Items.Count() >= 50)
                            {%>
                        <%Html.RenderPartial("IndexAction");%>
                        <%} %>
                    </form>
                </div>
            <% } %>
        </div>
    </div>
</div>
</asp:Content>
