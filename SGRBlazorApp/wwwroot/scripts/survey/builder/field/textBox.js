/*
*
* text field
* author: ronglin
* create date: 2010.06.29
*
*/

(function ($) {

    var textBox = function (config) {
        textBox.superclass.constructor.call(this, config);
    };

    yardi.extend(textBox, yardi.baseField, {

        buildHtml: function () {
            var html = [];
            html.push('<div class="s-field s-textbox">');
            yardi.baseSnippets.fieldCommon(html, this, 'Texto');
            html.push('<div class="wrap"><input id="#{n}" name="#{n}" #{sync}="defaultvalue" type="text" /></div>');
            html.push('</div>');
            return html.join('');
        },

        buildProHtml: function () {
            var html = [];
            html.push('<div class="s-field-pro s-textbox-pro">');
            yardi.baseSnippets.fieldtitle.p(html, this);
            html.push('<div class="baserow">');
            html.push('<label class="baselabel">Valor predeterminado #{valueTip}</label>');
            html.push('<input #{sync}="defaultvalue" class="defaultvalue" type="text" />');
            html.push('</div>');
            yardi.baseSnippets.guideline.p(html, this);
            yardi.baseSnippets.required.p(html, this);
            html.push('</div>');
            return html.join('');
        },

        getTips: function () {
            var tipsData = textBox.superclass.getTips.call(this);
            return $.extend(tipsData, {
                valueTip: {
                    title: 'Valor predeterminado',
                    message: 'El texto colocado aquí aparecerá al operador para  reemplazar, Ej.: Responda aquí'
                }
            });
        }

    });

    // register
    yardi.baseField.register('textBox', textBox);

})(jQuery);
