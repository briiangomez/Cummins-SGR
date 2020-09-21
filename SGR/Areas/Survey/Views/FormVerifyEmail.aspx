<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Survey/Form.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%="Join Email".Localize()%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%="Please input you email address.".Localize()%></h2>
    <br />
    <form method="post" action="<%=Url.Action("FormVerifyEmail") %>">
    <input type="text" name="Email" />
    <input type="hidden" name="Id" value="<%=ViewData["SurveyId"]%>" />
    <input type="submit" value="<%="Continue".Localize()%>" />
    <%if (ViewData["Message"] != null)
      { %>
    <span style="color: Red;">
        <%=ViewData["Message"]%></span>
    <%} %>
    </form>
</asp:Content>
