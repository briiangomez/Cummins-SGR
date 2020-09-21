<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Survey/Form.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%="Survey Joined".Localize()%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        <%="You had joined this survey.".Localize()%></h3>
    <%if (ViewData["ResultUrl"] != null)
      { %>
    <br />
    <div>
        <a class="button" href="<%=ViewData["ResultUrl"]%>">
            <%="View Statistics".Localize()%></a>
    </div>
    <%} %>
</asp:Content>
