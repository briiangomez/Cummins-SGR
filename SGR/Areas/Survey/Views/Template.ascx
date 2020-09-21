﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<h4>
    Select how to create a survey
</h4>
<table>
    <tr>
        <td>
            <%=Html.RadioButton("UseTemplate", "blank", true, new { id = "use_blank" })%>
            <label for="use_blank">
                Start with a blank survey</label>
        </td>
    </tr>
    <tr>
        <td>
            <%=Html.RadioButton("UseTemplate", "template", false, new { id = "use_template" })%>
            <label for="use_template">
                Choose a customizable survey template that includes sample questions</label>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <input type="hidden" id="TemplateId" name="TemplateId" />
            TODO: Choose Template
        </td>
    </tr>
</table>
