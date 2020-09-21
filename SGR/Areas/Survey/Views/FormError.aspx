<%@ Page MasterPageFile="~/Views/Survey/Form.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%="FormError".Localize()%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%var message = (Dictionary<string, string>)ViewData["Message"];
      var count = 0;
      foreach (var item in message)
      {
          count++;
    %>
    <p>
        <%=string.Format("Error{0}: ", count) + item.Value%>
    </p>
    <%} %>
</asp:Content>
