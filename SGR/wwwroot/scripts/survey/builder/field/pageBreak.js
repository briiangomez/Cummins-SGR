/*
*
* page break field
* author: ronglin
* create date: 2010.07.01
*
*/

(function () {

    var pageBreak = function (config) {
        pageBreak.superclass.constructor.call(this, config);
    };

    yardi.extend(pageBreak, yardi.baseField, {

        buildHtml: function () {
            var html = [];
            html.push('<div class="s-field s-pagebreak">');
            html.push('<label #{sync}="breaktitle" class="title">-----------------------------------Page Break-----------------------------------</label>');
            html.push('<div #{sync}="guideline" class="wrap"></div>');
            html.push('</div>');
            return html.join('');
        },

        buildProHtml: function () {
            var html = [];
            html.push('<div class="s-field-pro s-pagebreak-pro">');
            //html.push('<div class="baserow">');
            //html.push('<label class="baselabel">Field Label #{labelTip}</label>');
            //html.push('<textarea #{sync}="breaktitle" class="fieldlabel"></textarea>');
            //html.push('</div>');
            html.push('<div class="baserow">');
            html.push('<label class="baselabel">Guidelines #{guidelineTip}</label>');
            html.push('<textarea #{sync}="guideline" class="guideline" maxlength="50"></textarea>');
            html.push('</div>');
            html.push('</div>');
            return html.join('');
        },

        getTips: function () {
            var tipsData = pageBreak.superclass.getTips.call(this);
            return $.extend(tipsData, {
                //labelTip: {
                //    title: 'Field Label',
                //    message: 'Field Label is one or two words placed directly above the field.'
                //},
                guidelineTip: {
                    title: 'Guidelines',
                    message: 'This text will be displayed to your users while they are filling out this section of the survey. '
                }
            });
        }
    });

    // register
    yardi.baseField.register('pageBreak', pageBreak);

})();