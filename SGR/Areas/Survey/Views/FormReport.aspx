﻿<%@ Page MasterPageFile="~/Views/Survey/Form.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage<List<SurveyReportViewModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%="Survey Report".Localize()%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        /* reset */
        body, h1, h2, h3, h4, h5, h6, p, th, td
        {
            margin: 0;
            padding: 0;
        }
        table
        {
            border-collapse: collapse;
            border-spacing: 0;
        }
        caption, th
        {
            text-align: left;
        }
        caption
        {
            color: #005c9c;
        }
        /* body */
        body
        {
            line-height: 1;
            font: 12px/1.5 Arial, Helvetica, sans-serif;
            color: #333;
            width: 100%;
            text-align: center;
        }
        .body-wrap
        {
            background-color: #fff;
            width: 700px;
            text-align: left;
            margin: 0px auto;
            margin-top: 20px;
            margin-bottom: 20px;
            padding: 20px;
            border: 1px solid #ccc;
        }
        /* common form */
        .common-form table
        {
            width: 100%;
        }
        .common-form tr
        {
            border-bottom: 1px solid #EEE;
        }
        .common-form th
        {
            padding: 5px;
            vertical-align: top;
            width: 140px;
        }
        .common-form td
        {
            padding: 5px;
        }
        /* tabel container */
        .table-container
        {
            margin-top: 15px;
        }
        .table-container table
        {
            background: #FFF;
            border: 1px solid #CCC;
            clear: right;
            width: 100%;
        }
        .table-container caption
        {
            font-size: 14px;
            font-weight: bold;
        }
        .table-container th
        {
            background: #E5E5E5;
            border: 1px solid #CCC;
            padding: 5px;
        }
        .table-container td
        {
            border-left: 1px solid #CCC;
            padding: 3px 6px;
            vertical-align: text-top;
            word-break: break-all;
            word-wrap: break-word;
        }
        .table-container td a
        {
            color: #333;
            margin: 0 5px;
        }
        /* precent bar */
        .precent-bar
        {
            text-align: left;
            width: 152px;
            height: 16px;
            float: left;
            background: url(/images/vote_cl_bar.png);
            background-repeat: no-repeat;
            background-position: 0px 0px;
        }
        .precent-bar .precent
        {
            overflow: hidden;
        }
        
        .breakall
        {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%if (Model.Count > 0)
      {%>
    <div style="text-align: center;">
        <h2 class="breakall">
            <%=ViewData["SurveyTitle"]%>
        </h2>
    </div>
    <%foreach (var item in Model)
      { %>
    <div class="table-container clearfix">
        <%Html.RenderPartial("QuestionStatistics", item);%>
    </div>
    <%}%>
    <%}
      else
      { %>
    <div style="float: none; font-weight: bold;">
        <p>
            <%="This survey have no questions.".Localize()%>
        </p>
    </div>
    <%} %>
</asp:Content>
