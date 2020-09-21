<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SurveyInfoViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    <%if (Model.IsNew)
      { %><%= "Create Survey".Localize()%><%}
      else
      { %>
    <%=Model.SurveyName%>
    <%} %>
    <%Html.RenderPartial("Header");%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%if (Model.IsNew)
      { %>
    <%=Html.Crumb("basic", SurveyInfoViewModel.Tabs(Model.SurveyId))%>
    <%}
      else
      { %>
    <%=Html.Tab("basic", SurveyInfoViewModel.Tabs(Model.SurveyId))%>
    <%} %>
    <div class="tab-content">
        <div class="common-form">
            <form method="post" action="<%=Url.Action("Detail")%>">
            <%Html.RenderPartial("SurveyInfo", Model);%>
            <%--<%if (Model.SurveyId == Guid.Empty)
              { %>
            <hr />
            <%Html.RenderPartial("Template");%>
            <%} %>--%>
            <hr />
            <input type="hidden" name="IsCopy" value="<%=ViewData["IsCopy"]%>" />
            <input class="button" type="submit" name="Submit" value="<%="Save".Localize()%>" />
            <input class="button" type="submit" name="SubmitNext" value="<%="Next >".Localize()%>" />
            </form>
        </div>
    </div>
</asp:Content>
