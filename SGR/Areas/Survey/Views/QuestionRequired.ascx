<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SurveyReportViewModel>" %>
<div class="survey-noQuestion">
    <%="No questions.".Localize()%>
    <%--<p>
        <%="This survey have no questions.".Localize()%>
    </p>
    <p>
        You can <a href="<%=Url.Action("Builder", new { Id = Model.Survey.Id })%>">Edit this
            survey</a> to add some questions.
    </p>--%>
</div>
