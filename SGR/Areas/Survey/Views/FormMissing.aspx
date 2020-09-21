<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Survey/Form.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%="Survey Missing".Localize()%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="survey-formMissing">
        <h2>
            <%="Can not find this survey".Localize()%></h2>
        <p>
            <%="this survey might have been removed.".Localize()%>
        </p>
    </div>
</asp:Content>
