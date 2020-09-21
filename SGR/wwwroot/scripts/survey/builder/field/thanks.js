/*
*
* thanks field
* author: ronglin
* create date: 2010.07.14
*
*/

(function () {

    var thanks = function (config) {
        thanks.superclass.constructor.call(this, config);
    };

    yardi.extend(thanks, yardi.baseField, {

        valueField: false,

        disable: function (disabled) {
            thanks.superclass.disable.call(this, disabled);
            this.el[disabled === true ? 'addClass' : 'removeClass']('s-field-nomove');
        },

        buildHtml: function () {
            var html = [];
            html.push('<div class="s-field s-thanks">');
            html.push('<div #{sync}="message" class="wrap">Muchas gracias por responder la encuesta.</div>');
            html.push('</div>');
            return html.join('');
        },

        buildProHtml: function () {
            var html = [];
            html.push('<div class="s-field-pro s-thanks-pro">');
            html.push('<div class="baserow">');
            html.push('<label class="baselabel">Mensaje #{messageTip}</label>');
            html.push('<textarea #{sync}="message" class="fieldlabel"></textarea>');
            html.push('</div>');
            html.push('</div>');
            return html.join('');
        },

        getTips: function () {
            var tipsData = thanks.superclass.getTips.call(this);
            return $.extend(tipsData, {
                messageTip: {
                    title: 'Message',
                    message: ''
                }
            });
        }
    });

    // register
    yardi.baseField.register('thanks', thanks);

})();