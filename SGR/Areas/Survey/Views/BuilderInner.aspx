<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BuilderInner</title>
    <style>
        @media screen (width: 768px) and (height: 1024px) {
            .designer-leftwrap {
                float: left;
                width: 80%!important;
            }
        }
    </style>
    <script type="text/javascript">
        var _vpath = '<%=Url.Content("~/")%>';
    </script>
    <%= Html.ExternalResources("builderjs")%>
    <%= Html.ExternalResources("buildercss")%>
    <script type="text/javascript">
        function surveyAjax(url, postData, callback) {
            $.ajax({
                url: url,
                type: 'POST',
                data: postData,
                dataType: 'json',
                timeout: 30000,
                beforeSend: function (request) { },
                complete: function (request, state) { },
                error: function (response, state) { alert('error'); },
                success: function (data, state) {
                    callback(data, state);
                }
            });
        }

        $(function () {
            // onload
            var IsNew = $('input[name=IsNew]').val();
            var formId = $('input[name=formId]').val();
            if (formId) {
                var obj = { name: null, html: null, seted: false };
                var set = function (name, html) {
                    builderObject.setHtml(decodeURIComponent(html));
                    if (name) { builderObject.setFormTitle(decodeURIComponent(name)); }
                };
                surveyAjax('<%=Url.Action("LoadHtml", "Survey")%>', { id: formId, IsNew: IsNew }, function (data, state) {
                    obj.name = data.name || '';
                    obj.html = data.html || '';
                    if (obj.seted) { set(obj.name, obj.html); }
                });
                setTimeout(function () {
                    obj.seted = true;
                    if (obj.name !== null && obj.html !== null) { set(obj.name, obj.html); }
                }, 800);
            }
        });
    </script>
</head>
<body class="pagebody">
    <input type="hidden" name="IsNew" value="<%=ViewData["IsNew"]%>" />
    <input type="hidden" name="formId" value="<%=ViewData["Surveyid"]%>" />
    <table border="0" cellpadding="0" cellspacing="0" class="mainlayout">
        <tr>
            <td class="colleft">
                <div class="designer-leftwrap cusbody" style="width:80%!important">
                    <div class="designer-head">
                    </div>
                    <div class="designer-main">
                    </div>
                    <div class="designer-foot">
                    </div>
                    <div class="designer-mesg">
                    </div>
                </div>
            </td>
            <td class="colright">
                <div class="designer-sidebar">
                </div>
            </td>
        </tr>
    </table>
</body>
</html>
