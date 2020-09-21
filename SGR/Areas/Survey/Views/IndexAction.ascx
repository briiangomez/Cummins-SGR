<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="command clearfix">
    <a class="btn btn-primary mTop0 floatRight " href="<%=Url.Action("Detail", new { IsNew = true })%>"><%= "Create".Localize() %></a>
    <input type="submit" name="CommandUnpublish" class="btn btn-secondary mTop0 mRight10 floatRight " value="<%= "Unpublish".Localize() %>" />
</div>
