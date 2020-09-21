<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SurveyInfoViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<h1 class="text_center text_black">Detalle de Encuesta</h1>
<div class="container text_center" id="encuestas">        
    <div class="col-md-12 align_center noFloat contentBox text_left">
        
    
        <div class="tab-content padding20 text-left">
            <h3 class="text_black text_left"><%=Model.SurveyName%></h3>
            <form method="post" class="mBottom0" action="<%=Url.Action("Publish")%>">
            
                <div class="common-form">
                    <%if (!Model.Published)
                        {%>
                    <div class="survey-attention">
                        <p>
                            <img src="../../images/businnes/warning.png" style="margin-right:10px;" alt="warning" class="floatLeft" /><%="This survey has not been published yet. <br />Publishing your survey means it is ready to be taken.".Localize(), new { @class = "floatLeft" }%><br />
                        </p>
                    </div>
                    <%}
                        else
                        { %>
                    <p class="survey-published">
                        &nbsp;<%="This survey has been published.".Localize()%>&nbsp;&nbsp; <a href="<%=Url.Action("Distribute", new { Id = Model.SurveyId })%>">
                            <%="Click to view distribution data".Localize()%></a>
                    </p>
                    <%} %>
                    <%Html.RenderPartial("SurveyInfo", Model);%>
                </div>
                <div class="row mTop20">
                    <input type="submit" class="btn btn-secondary floatLeft" style="margin-top:0!important" name="btnSave" value="<%="Save".Localize()%>" />
                    <%if (!Model.Published)
                    {%>
                    <input type="submit" class="btn btn-primary floatRight mLeft10" name="btnPublish" value="<%="OK, Publish My Survey".Localize()%>" />
                    <%}%>
                </div>
            </form>
        </div>
    </div>
</div>
</asp:Content>
