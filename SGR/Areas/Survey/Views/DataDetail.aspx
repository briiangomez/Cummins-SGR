<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CMM.Survey.ModelsDb.Survey_PostData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    <script type="text/javascript">
        document.getElementById("nav6").className = "active";
        document.getElementById("nav35").className = "dropdown active";
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3><%="Data: ".Localize() + Model.Survey_Form.FormName%></h3>
    <a class="btn btn-primary" href="<%=Url.Action("Data", new { Id = Model.FormId })%>">
        <%="Back to list".Localize()%></a>
    <br></br>
    <div class="common-form">
        <fieldset>
            <legend></legend>
            <table>
                <tr>
                    <th>
                        <%="Survey name: ".Localize()%>
                    </th>
                    <td>
                        <%=Model.Survey_Form.FormName%>
                    </td>
                </tr>
                <tr>
                    <th>
                        <%="Post date: ".Localize()%>
                    </th>
                    <td>
                        <%=Model.PostDate%>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <%="Answer: ".Localize()%></legend>
            <table width="100%">
                <%foreach (var question in Model.Survey_Form.Survey_Question)
                    {%>
                <tr>
                    <th>
                        <p>

                            <%=SurveyInfoViewModel.FormatQuestionTitle(question)%>
                        </p>
                    </th>
                    <td>
                        <%var writeAnswer = false;
                            foreach (var answer in question.Survey_Answer)
                            {
                                if (answer.AnswerTypeEnum == CMM.Survey.Models.SurveyAnswerType.Empty)
                                    continue;
                                writeAnswer = true;
                        %>
                        <p>
                            <%=answer.AnswerText%>
                        </p>
                        <%}%>
                        <%if (!writeAnswer)
                            {%>
                        <p>
                            &nbsp;
                        </p>
                        <%}%>
                    </td>
                </tr>
                <tr>
                    <th>
                        <hr />
                    </th>
                    <td>
                        <hr />
                    </td>
                </tr>
                <%}%>
            </table>
        </fieldset>
    </div>
</asp:Content>
