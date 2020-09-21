/*
*
* textarea field
* author: ronglin
* create date: 2010.07.01
*
*/

(function () {

    var textArea = function (config) {
        textArea.superclass.constructor.call(this, config);
    };

    yardi.extend(textArea, yardi.baseField, {

        buildHtml: function () {
            var html = [];
            html.push('<div class="s-field s-textarea">');
            yardi.baseSnippets.fieldCommon(html, this, 'Paragraph');
            html.push('<div class="wrap"><textarea id="#{n}" name="#{n}" #{sync}="defaultvalue"></textarea></div>');
            html.push('</div>');
            return html.join('');
        },

        buildProHtml: function () {
            var html = [];
            html.push('<div class="s-field-pro s-textarea-pro">');
            yardi.baseSnippets.fieldtitle.p(html, this);
            html.push('<div class="baserow">');
            html.push('<label class="baselabel">Default Value #{valueTip}</label>');
            html.push('<textarea #{sync}="defaultvalue" class="defaultvalue"></textarea>');
            html.push('</div>');
            yardi.baseSnippets.guideline.p(html, this);
            yardi.baseSnippets.required.p(html, this);
            html.push('</div>');
            return html.join('');
        },

        getTips: function () {
            var tipsData = textArea.superclass.getTips.call(this);
            return $.extend(tipsData, {
                valueTip: {
                    title: 'Default Value',
                    message: 'The text placed in this area will appear for the survey taker to replace, Example: Answer here.'
                }
            });
        }

    });

    // reg
    yardi.baseField.register('textArea', textArea);

})();