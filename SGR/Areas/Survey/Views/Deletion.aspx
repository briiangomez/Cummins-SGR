<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#Sure').change(function () {
                $('#btnSubmit').attr('disabled', !this.checked);
            });
        });
    </script>
    <style>
        .contentBox{
            width:calc(100% - 40px)!important;
        }
    </style>

<div class="container text_center">
<h1 class="text_black">Eliminar encuesta</h1>
        
<div class="col-md-10 align_center noFloat contentBox padding20">
    <div class="col-md-12 noPadding text_left">
            <h3 class="text_left mTop20 "><%= ("Deleting Your Survey: ".Localize() + ViewData["SurveyName"].ToString()).Localize()%></h3>
            <div class="common-form">
                <form method="post" action="<%=Url.Action("Deletion")%>">
                <div class="survey-attentions">
                    <p class="text_red text_left">
                        <img src="../../images/businnes/warning.png" style="margin-right:10px;" alt="warning" class="floatLeft" /><%="Attention:".Localize()%>
                    </p>
                    <p class="pBottom0">
                        <%="Delete the survey will delete the data belong to this survey, and so as to the reports.".Localize()%>
                    </p>
                </div>
                <div class="survey-confirm">
                    <label for="Sure">
                        <%="Are you sure you want to delete this survey?".Localize()%>  <input type="checkbox" name="Sure" id="Sure" value="True" style="margin: 8px;"/></label>
                </div>
                    <input type="hidden" name="Id" value="<%=ViewData["SurveyId"]%>" />
                    <input type="submit" class="btn btn-destroy floatRight" id="btnSubmit" name="Submit" disabled="disabled" value="<%="OK, Delete This Survey".Localize()%>" />
                </form>
            </div>
        </div>
    </div>
</div>
</asp:Content>
