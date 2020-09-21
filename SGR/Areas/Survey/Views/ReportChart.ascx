<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SurveyReportViewModel>" %>
<script type="text/javascript">
    function onchartload(img) {
        $(img).show();
        if ($(img).height() > 0) {
            $('#spanLoading').remove();
            $('#survey_reportChartDiv').height($(img).height());
        }
    }
    $(function () {
        $('select[name=Qid]').change(function () {
            $('#singleform').get(0).submit();
        });
        var src = '<%=ViewData["ChartSrc"]%>'
        var request = function (type) {
            var url = src;
            if (type) {
                url += '&Type=' + type;
                url += '&rid=' + Math.random();
            }
            $('#reportchart,#spanLoading').remove();
            $('#survey_reportChartDiv').append('<span id="spanLoading">Chart Generating ...</span>')
                                       .append('<img onload="onchartload(this)" style="display:none;" id="reportchart" src="' + url + '" alt="chart" />');
        };
        request($('input[name=picType]').val());
        $('input[name=picType]').change(function () {
            request($(this).val());
        });
    });
</script>
<div class="common-form">
    <table width="100%">
        <tbody>
            <tr>
                <th width="10%">
                    <label><%="Chart type:".Localize()%></label>
                    <p style="padding-bottom: 2px">
                        <label for="rdoColumn" class="radio-label"><%="Column".Localize()%> <input type="radio" name="picType" id="rdoColumn" value="Column" checked="checked" style="margin:0" /></label>
                    </p>
                    <p>
                        <label for="rdoPie" class="radio-label"><%="Pie".Localize()%> <input type="radio" name="picType" id="rdoPie" value="Pie" style="margin:0" /></label>
                    </p>
                </th>
                <td width="90%" style="text-align: center">
                    <div id="survey_reportChartDiv" style="margin-bottom: 10px;">
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</div>

