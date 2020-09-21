(function () {

    var copyright = function (config) {
        copyright.superclass.constructor.call(this, config);
    };

    yardi.extend(copyright, yardi.baseField, {

        disable: function (disabled) {
            copyright.superclass.disable.call(this, disabled);
            this.el[disabled === true ? 'addClass' : 'removeClass']('s-field-nomove');
        },

        buildHtml: function () {
            var html = [];
            html.push('<div class="s-field s-copyright">');
            html.push('<div class="wrap">');
            html.push('<span #{sync}="title" class="title">Footer</span>');
            html.push('<a #{sync}="linktext" class="link" href="#">Descripción de URL</a>');
            html.push('</div>');
            html.push('</div>');
            return html.join('');
        },

        buildProHtml: function () {
            var html = [];
            html.push('<div class="s-field-pro s-copyright-pro">');
            html.push('<div class="baserow">');
            html.push('<label class="baselabel">Titulo #{titleTip}</label>');
            html.push('<textarea #{sync}="title" class="textarea"></textarea>');
            html.push('</div>');
            html.push('<div class="baserow">');
            html.push('<label class="baselabel">Url #{linkUrlTip}</label>');
            html.push('<input #{sync}="linkhref" type="text" class="textinput" />');
            html.push('</div>');
            html.push('<div class="baserow">');
            html.push('<label class="baselabel">Texto Footer #{linkTextTip}</label>');
            html.push('<textarea #{sync}="linktext" class="linktext"></textarea>');
            html.push('</div>');
            html.push('</div>');
            return html.join('');
        },

        initPro: function () {
            // call base
            copyright.superclass.initPro.call(this);
            // href kit
            var href = this.fItems('linktext').attr('href');
            this.pItems('linkhref').val(href);
        },

        onUpdateValue: function (sender, name) {
            copyright.superclass.onUpdateValue.call(this, sender, name);
            if (name == 'linkhref') {
                this.fItems('linktext').attr('href', sender.val());
            }
        },

        getTips: function () {
            var tipsData = copyright.superclass.getTips.call(this);
            return $.extend(tipsData, {
                titleTip: {
                    title: 'Field Title',
                    message: 'Introducir información sobre el footer'
                },
                linkUrlTip: {
                    title: 'Link Url',
                    message: 'Ingrese una URL'
                },
                linkTextTip: {
                    title: 'Link Text',
                    message: 'El texto introducido aquí será un hipervínculo a la URL introducida'
                }
            });
        }

    });

    // register
    yardi.baseField.register('copyright', copyright);

})();