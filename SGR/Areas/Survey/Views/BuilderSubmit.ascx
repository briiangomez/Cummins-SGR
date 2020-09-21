<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SurveyInfoViewModel>" %>
<div class="common-form">
    <form method="post" action="<%=Url.Action("Builder")%>" submitform="1">
    <input type="hidden" name="IsNew" value="<%=Model.IsNew%>" />
    <input type="hidden" name="SurveyId" value="<%=Model.SurveyId%>" />
    <input type="hidden" name="SurveyName" />
    <input type="hidden" name="SurveyHtml" />
    <p class="buttons clearfix pBottom0">
        <input class="btn btn-secondary floatLeft mBottom0" type="submit" name="SubmitSave" disabled="disabled" value="<%="Save".Localize()%>" />
         <input class="btn btn-primary floatRight mBottom0" type="submit" name="SubmitNext" disabled="disabled" value="<%="Next".Localize()%>" />
    </p>
    </form>
</div>
