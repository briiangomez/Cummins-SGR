<%@ Page MasterPageFile="~/Views/Survey/Form.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%="Join Verify".Localize()%></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .verify
        {
            font-family: tahoma, arial, helvetica, sans-serif;
            font-size: 12px;
            line-height: 2;
        }
        .verify .notice
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="verify">
        <h2>
            <%="This survey is validate required".Localize()%></h2>
        <span class="notice">
            <%="Before join this survey, please enter your validation message first.".Localize()%></span>
        <form method="post" action="<%=Url.Action("FormVerify")%>">
        <input type="hidden" name="Id" value="<%=ViewData["SurveyId"]%>" />
        <label for="Pass">
            <%="Message:".Localize()%>
        </label>
        <input type="text" name="Pass" id="Pass" />
        <input type="submit" value="OK" />
        <%if (ViewData["Incorrect"] == "true")
          { %>
        <span style="color: Red;">
            <%="The validation message is not correct!".Localize()%></span>
        <%} %>
        </form>
    </div>
</asp:Content>
