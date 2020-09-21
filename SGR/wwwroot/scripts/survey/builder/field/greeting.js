/*
*
* greeting field
* author: ronglin
* create date: 2011.01.24
*
*/

(function ($) {

    var greeting = function (config) {
        greeting.superclass.constructor.call(this, config);
    };

    yardi.extend(greeting, yardi.baseField, {

        showGreeting: false,

        serialize: function () {
            greeting.superclass.serialize.call(this);
            this.el.attr('showGreeting', this.showGreeting);
        },

        deserialize: function () {
            greeting.superclass.deserialize.call(this);
            this.showGreeting = (this.el.attr('showGreeting') === 'true');
        },

        disable: function (disabled) {
            greeting.superclass.disable.call(this, disabled);
            this.el[disabled === true ? 'addClass' : 'removeClass']('s-field-nomove');
        },

        buildHtml: function () {
            var html = [];
            html.push('<div class="s-field s-greeting">');
            html.push('<div #{sync}="message" class="wrap">Favor de tomarse el tiempo prudente para completar la información.</div>');
            html.push('</div>');
            return html.join('');
        },

        buildProHtml: function () {
            var html = [];
            html.push('<div class="s-field-pro s-greeting-pro">');
            html.push('<div class="baserow">');
            html.push('<input #{sync}="showgreeting" id="#{uuid}_showgreeting" type="checkbox" class="basecheck" /><label for="#{uuid}_showgreeting" class="basechecklabel"> Mostrar recordatorio</label>');
            html.push('</div>');
            html.push('<div class="baserow">');
            html.push('<label class="baselabel">Recordatorio #{messageTip}</label>');
            html.push('<textarea #{sync}="message" class="message" maxlength="200"></textarea>');
            html.push('</div>');
            html.push('</div>');
            return html.join('');
        },

        initPro: function () {
            greeting.superclass.initPro.call(this);
            var self = this;
            this.pItems('showgreeting').attr('checked', this.showGreeting).change(function () {
                self.showGreeting = $(this).attr('checked');
                self.el.attr('showGreeting', self.showGreeting);
            });
        },

        getTips: function () {
            var tipsData = greeting.superclass.getTips.call(this);
            return $.extend(tipsData, {
                messageTip: {
                    title: 'Greeting Message',
                    message: ''
                }
            });
        }
    });

    // register
    yardi.baseField.register('greeting', greeting);

})(jQuery);