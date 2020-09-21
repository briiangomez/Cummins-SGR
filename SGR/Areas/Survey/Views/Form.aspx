<%@ Page MasterPageFile="~/Views/Survey/Form.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<SurveyFormViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Model.FormTitle%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%=Model.FormHtml%>
</asp:Content>
