<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
     <script type="text/javascript">
        document.getElementById("nav6").className = "active";
        document.getElementById("nav35").className = "dropdown active";
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $('textarea').focus(function () {
                $(this).select();
            });
        });</script>
<h1 class="text_center text_black"><%="Distribute your survey".Localize()%></h1>
<div class="container" id="encuestas">        
    <div class="col-md-12 align_center noFloat contentBox">
        <div class="col-md-12 noPadding">
            <div class="innerBox">
                <div class="tab-content">
                    <div class="common-form">
                        <h3><%= ViewData["SurveyName"]%></h3>
                        <p>
                            <%="1.You can visit this".Localize()%>
                            <strong><a target="_blank" href="<%=ViewData["NormalUrl"]%>">
                                <%="Link".Localize()%></a></strong>
                            <%="to join this survey or copy the link url below to distribute it".Localize()%>
                            <br />
                            <textarea id="txtUrl" class="survey-distributeUrl"><%:ViewData["NormalUrl"]%></textarea>
                        </p>
                        <br />
                        <p>
                            <%="2.You can also use this html code within your html page".Localize()%>
                            <br />
                            <textarea id="txtIframe" class="survey-distributeIframe"><%:ViewData["IframeHtml"]%></textarea>
                        </p>
                        <br />
                        <p>
                            <%="3.The html code for the entire survey page".Localize()%>
                            <br />
                            <textarea id="txtHtml" class="survey-distributeHtml"><%:ViewData["SurveyHtml"]%></textarea>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
