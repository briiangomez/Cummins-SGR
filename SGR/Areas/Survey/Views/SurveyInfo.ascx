<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SurveyInfoViewModel>" %>

<script type="text/javascript">
    $(function () {
        $.datepicker.setDefaults({ dateFormat: 'yy/mm/dd' });
        $('#StartTime').datepicker();
        $('#EndTime').datepicker();
        // period
        var StartTime = '<%=Model.StartTime%>', EndTime = '<%=Model.EndTime%>';
        $('#EnablePeriod').attr('checked', StartTime !== '' || EndTime !== '').each(function () {
            var fn = function () { $('#PeriodContainer')[$(this).attr('checked') ? 'show' : 'hide'](); }
            $(this).change(fn);
            fn.call(this);
        });
        // password
        var JoinPassword = '<%=Model.JoinPassword%>';
        $('#EnablePassword').attr('checked', JoinPassword !== '').each(function () {
            var fn = function () { $('#PasswordContainer')[$(this).attr('checked') ? 'show' : 'hide'](); }
            $(this).change(fn);
            fn.call(this);
        });
        // form submit
        $('form').bind('submit', function () {
            if (!$('#EnablePeriod').attr('checked')) {
                $('#StartTime').val('');
                $('#EndTime').val('');
            }
            if (!$('#EnablePassword').attr('checked')) {
                $('#JoinPassword').val('');
            }
        });
    });
</script>
<input type="hidden" name="SurveyName" value="<%=Model.SurveyName%>" />
<input type="hidden" name="SurveyId" value="<%=Model.SurveyId%>" />
<input type="hidden" name="IsNew" value="<%=Model.IsNew%>" />
<input type="hidden" name="Paused" value="<%=Model.Paused%>" />
<table id="surveyInfo" >
    <tr >
        <th class="text_left">
            <label for="EnablePeriod">
                <%="Period".Localize()%></label>
        </th>
        <td class="text_left">
            <input id="EnablePeriod" type="checkbox" /><br />
            <div id="PeriodContainer" style="display: none;">
                <input type="text" name="StartTime" id="StartTime" style="width: 130px;" value="<%=Model.StartTime.HasValue ? Model.StartTime.Value.ToShortDateString() : ""%>" />
                <input type="text" name="EndTime" id="EndTime" style="width: 130px;" value="<%=Model.EndTime.HasValue ? Model.EndTime.Value.ToShortDateString() : ""%>" />
                <%=Html.ValidationMessageFor(o => o.StartTime)%>
            </div>
        </td>
    </tr>
    <tr>
        <th class="text_left">
            <label for="EnablePassword">
                Contraseña</label>
        </th>
        <td class="text_left">
            <input id="EnablePassword" type="checkbox" /><br />
            <div id="PasswordContainer" style="display: none;">
                <input type="text" name="JoinPassword" id="JoinPassword" class="input" style="width: 282px;" value="<%=Model.JoinPassword%>" />
                <%=Html.ValidationMessageFor(o => o.JoinPassword)%>
            </div>
        </td>
    </tr>
    <tr>
        <th class="text_left">
            <label for="RespondResult">
                <%="Respond result".Localize()%></label>
        </th>
        <td class="text_left">
            <%=Html.CheckBoxFor(o => o.RespondResult)%>
            <%=Html.ValidationMessageFor(o => o.RespondResult)%>
        </td>
    </tr>
    <tr>
        <th class="text_left">
            <label for="ValidatorType">
                <%="Validator type".Localize()%></label>
        </th>
        <td class="text_left">
            <%
                    var list = new List<SelectListItem>();
                    //list.Add(new SelectListItem() { Text = string.Empty, Value = string.Empty });
                    var names = Enum.GetNames(typeof(CMM.Communicator.Controllers.ValidatorType));

                    foreach (var n in names)
                    {
                        list.Add(new SelectListItem()
                        {
                            Text = n,
                            Value = n
                    });
                }

            %>
            <%=Html.DropDownListFor(o => o.ValidatorType, list, new { @class = "select" })%>
            <%=Html.ValidationMessageFor(o => o.ValidatorType)%>

            <script>
                document.getElementById('ValidatorType').value = 'Cookie';
            </script>
        </td>
    </tr>
    <tr>
        <th class="text_left"><label>Info</label></th>
        <td style="font-size: 12px" class="text_left">
            <%="Cookie: Una vez respondida la encuesta, se guarda una cookie en el navegador del cliente. Si se detecta que desde la misma PC se quiere volver a responder, el sistema informará que la encuesta ya ha sido completada.".Localize() %>
             <%="Email: Se solicitará el correo electrónico antes de comenzar a completar la encuesta. Si ya fue ingresado previamente, el sistema informará que la encuesta ya ha sido completada.".Localize() %>
        </td>
    </tr>
</table>
