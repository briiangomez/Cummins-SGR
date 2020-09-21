﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<List<SurveyReportViewModel>>" %>

<%var survey = (CMM.Survey.ModelsDb.Survey_Form)ViewData["Survey"]; %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=survey.FormName%></title>
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
        }
        .table-container td a
        {
            color: #333;
            margin: 0 5px;
        }
        /* precent */
        .precent-bar
        {
            text-align: left;
            width: 150px;
            height: 13px;
            float: left;
            border: 1px solid #ccc;
            background-repeat: no-repeat;
            background-position: 0px 0px;
        }
        .precent-bar .precent
        {
            overflow: hidden;
            width: 150px;
            height: 13px;
            background-color: #426C96;
        }
    </style>
</head>
<body>
    <div class="body-wrap">
        <div class="common-form">
            <table>
                <tr>
                    <th>
                        <label>
                            <%="Survey".Localize()%>:</label>
                    </th>
                    <td>
                        <%=survey.FormName%>
                    </td>
                </tr>
                <tr>
                    <th>
                        <label>
                            <%="Open Count".Localize()%>:</label>
                    </th>
                    <td>
                        <%=survey.VisitCount%>
                    </td>
                </tr>
                <tr>
                    <th>
                        <label>
                            <%="Join Count".Localize()%>:</label>
                    </th>
                    <td>
                        <%var joinCount = (int)ViewData["JoinCount"];%>
                        <%=joinCount%>
                        (<%=ViewUtility.Percent(joinCount, survey.VisitCount)%>%)
                    </td>
                </tr>
            </table>
        </div>
        <hr />
        <%if (Model.Count > 0)
          {%>
        <div style="text-align: center;">
            <h3>
                <%=ViewData["SurveyTitle"]%>
            </h3>
        </div>
        <%foreach (var item in Model)
          { %>
        <div class="table-container">
            <table>
                <%var title = string.Empty;
                  if (item.SelectedQuestion != null)
                  {
                      title = SurveyInfoViewModel.FormatQuestionTitle(item.SelectedQuestion) + ":";
                  }%>
                <caption>
                    <%=title%></caption>
                <thead>
                    <tr>
                        <th>
                            <%="Options".Localize()%>
                        </th>
                        <th style="width: 100px;">
                            <%="Subtotal".Localize()%>
                        </th>
                        <th style="width: 220px;">
                            <%="Percentage".Localize()%>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <%var totalCount = item.Items.Sum(o => o.Subtotal);
                      for (var i = 0; i < item.Items.Count; i++)
                      {
                          var item1 = item.Items[i];
                    %>
                    <tr>
                        <td>
                            <%=item1.OptionText%>
                        </td>
                        <td>
                            <%=item1.Subtotal%>
                        </td>
                        <td>
                            <%var p = ViewUtility.PercentOf(item1.Subtotal, totalCount).ToString("0.00").Replace(',', '.');%>
                            <div class="precent-bar">
                                <div style="width: <%=p%>%; display: block" class="precent">
                                </div>
                            </div>
                            <div style="float: left;">
                                <%=ViewUtility.Percent(item1.Subtotal, totalCount)%>%
                            </div>
                            <div style="clear: both;">
                            </div>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
                <tfoot>
                    <tr>
                        <th>
                            <%="Answer Count".Localize()%>:
                        </th>
                        <th colspan="2">
                            <%=totalCount%>
                        </th>
                    </tr>
                </tfoot>
            </table>
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
    </div>
</body>
</html>
