<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SurveyInfoViewModel>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/survey/builder/nest.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
     <script type="text/javascript">
        document.getElementById("nav6").className = "active";
        document.getElementById("nav35").className = "dropdown active";
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<%if (Model.IsNew)
                { %>
            <h1 class="text_center text_black"><%= "Create Survey".Localize()%></h1>
            <%}
                else
                { %>
            <h1 class="text_center text_black"><%=Model.SurveyName%></h1>
            <%} %>
<div class="container text_center" id="encuestas">
    <div class="col-md-12 align_center noFloat contentBox"  style="height:auto!important">
        <div class="col-md-12 noPadding">
            <div class="innerBox">
                <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all" style="width:fit-content;">
                    <li class="ui-state-default ui-corner-top" style="background-color:#fff!important;padding:8px 14px!important;">
                        <div id="_btnPreview" class="survey-action btn-tertiary" disabled="disabled" style="margin-left: 12px;">
                            <%="Preview".Localize()%>
                        </div>
                    </li>
                </ul>
              <%--<%Html.RenderPartial("Header");%> --%>
                <%--<%=Html.Crumb("survey", SurveyInfoViewModel.Tabs(Model.SurveyId))%>--%>
                <form id="previewform" method="post" target="_blank" action="<%=Url.Action("SavePreview")%>">
                    <input id="previewName" type="hidden" name="name" />
                    <input id="previewHtml" type="hidden" name="html" />
                </form>
                <div class="step-content">
                    <div id="mailbody" class="ui-tabs ui-widget ui-widget-content ui-corner-all">
                        <div id="htmltab" class="clearfix ui-tabs-panel ui-widget-content ui-corner-bottom" style="position: relative;">
                            <div id="iframeloading" style="position: absolute;">
                            </div>
                            <iframe id="surveybuilder" width="100%" frameborder="0"
                                scrolling="no" src="<%=Url.Action("BuilderInner", new { Id = Model.SurveyId, IsNew = Model.IsNew })%>"></iframe>
                        </div>
                    </div>
                </div>
                <%Html.RenderPartial("BuilderSubmit");%>
            </div>
        </div>
    </div>
</div>
</asp:Content>
