/*
*
* nest
* author: ronglin
* create date: 2011.01.18
*
*/

(function ($) {

    var options = {
        iframeSelector: '#surveybuilder',
        iframeLoading: '#iframeloading',

        submitForm: 'form[submitform]',
        actionButtons: 'input[type=submit], .survey-action',

        titleEmptyMessage: 'Survey title can not be empty!',
        nameInput: 'input[name=SurveyName]',
        htmlInput: 'input[name=SurveyHtml]',

        redoBtn: '#_btnRedo',
        undoBtn: '#_btnUndo',

        previewBtn: '#_btnPreview',
        previewName: '#previewName',
        previewHtml: '#previewHtml',
        previewForm: '#previewform'
    };

    // closure fields
    var builderCtx;

    window.iframeLoaded = function (builder, iframeWindow) {
        builderCtx = builder;
        bindRedoundo(builder.getRedoundo());

        // preview event
        $(options.previewBtn).unbind().click(function () {
            try {
                $(options.previewName).val(builder.getFormTitle());
                $(options.previewHtml).val(encodeURIComponent(builder.getHtml()));
                $(options.previewForm).get(0).submit();
            } catch (ex) { }
        }).addClass('survey-actionEnable');

        // scroll event
        $(window).unbind('scroll', fireScroll).scroll(fireScroll);

        // iframe loading
        $(options.iframeLoading).hide();
        $(options.actionButtons).removeAttr('disabled');
    };

    window.iframeUnload = function () {
        builderCtx = undefined;
        $(options.iframeLoading).show();
        $(options.actionButtons).attr('disabled', true);
    };

    function fireScroll() {
        try {
            builderCtx.fireScroll();
        } catch (ex) { }
    }

    function bindRedoundo(redoundo) {
        var btnUndo = $(options.undoBtn).unbind().click(function () { try { redoundo.undo(); } catch (ex) { } });
        var btnRedo = $(options.redoBtn).unbind().click(function () { try { redoundo.redo(); } catch (ex) { } });
        var disableFunc = function () {
            btnUndo[redoundo.canUndo() ? 'addClass' : 'removeClass']('survey-actionEnable');
            btnRedo[redoundo.canRedo() ? 'addClass' : 'removeClass']('survey-actionEnable');
        };
        redoundo.onUndo.add(disableFunc);
        redoundo.onRedo.add(disableFunc);
        redoundo.onCommit.add(disableFunc);
    }


    $(function () {

        $(options.submitForm).submit(function () {
            if (!builderCtx) { return false; }
            var name = builderCtx.getFormTitle();
            var html = builderCtx.getHtml();
            if (name && html) {
                $(options.nameInput).val(encodeURIComponent(name));
                $(options.htmlInput).val(encodeURIComponent(html));
                return true;
            }
            if (!name) {
                alert(options.titleEmptyMessage);
                builderCtx.focusFormTitle();
            }
            return false;
        });

        $(window).bind('unload', function () {
            // break leaks
            builderCtx = undefined;
            window.iframeLoaded = undefined;
            window.iframeUnload = undefined;
            // remove mail editor iframe
            var iframe = $(options.iframeSelector).get(0);
            if (iframe) {
                iframe.src = 'javascript:false;';
                iframe.parentNode.removeChild(iframe);
            }
        });
    });

})(jQuery);