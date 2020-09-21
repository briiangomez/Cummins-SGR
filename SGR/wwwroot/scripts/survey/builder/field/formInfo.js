(function ($) {

    var formInfo = function (config) {
        formInfo.superclass.constructor.call(this, config);
    };

    yardi.extend(formInfo, yardi.baseField, {

        numbering: true,

        separator: 0,

        serialize: function () {
            formInfo.superclass.serialize.call(this);
            this.el.attr('numbering', this.numbering);
            this.el.attr('separator', this.separator);
        },

        deserialize: function () {
            formInfo.superclass.deserialize.call(this);
            this.numbering = (this.el.attr('numbering') === 'true');
            this.separator = parseInt(this.el.attr('separator') || this.separator, 10);
        },

        disable: function (disabled) {
            formInfo.superclass.disable.call(this, disabled);
            this.el[disabled === true ? 'addClass' : 'removeClass']('s-field-nomove');
        },

        // public
        getFormTitle: function () {
            return $('h2', this.el).html();
        },

        // public
        setFormTitle: function (content) {
            $('h2', this.el).html(content);
            this.updatePropertyValue();
        },

        // public
        customSheet: null,
        customCssId: 'customcss',
        getCustomCssText: function () {
            var cssText = '<style type="text/css" id="' + this.customCssId + '">\n';
            cssText += this.customSheet.getRuleTextAll();
            return cssText + '</style>';
        },

        buildHtml: function () {
            var html = [];
            html.push('<div class="s-field s-forminfo">');
            html.push('<h2 #{sync}="formtitle" class="formtitle">Encuesta sin título</h2>');
            html.push('<p #{sync}="description" class="description">Esta es la descripción de su encuesta. Haga clic aquí para editar.</p>');
            html.push('</div>');
            return html.join('');
        },

        buildProHtml: function () {
            var html = [];
            html.push('<div class="s-field-pro s-forminfo-pro">');
            html.push('<div class="baserow">');
            html.push('<label class="baselabel">Código de la encuesta #{titleTip}</label>');
            html.push('<input #{sync}="formtitle" class="formtitle" type="text" maxlength="250" />');
            html.push('</div>');
            html.push('<div class="baserow">');
            html.push('<label class="baselabel">Descripción #{descTip}</label>');
            html.push('<textarea #{sync}="description" class="description"></textarea>');
            html.push('</div>');
            html.push('<div class="baserow">');
            html.push('<fieldset class="fontandcolor">');
            html.push('<legend class="baselegend"><span groupfor="fontscolors">Fuentes & Colores</span> #{fontAndColorsTip}</legend>');
            html.push('<table cellpadding="0" cellspacing="0" border="0" group="fontscolors">');
            html.push('<tr>');
            html.push('<td class="borderbottom" colspan="2">Cambiar el estilo por defecto</td>');
            html.push('<td class="borderbottom" _resetstyle="1"></td>');
            html.push('</tr><tr>');
            html.push('<td class="borderbottom" colspan="2">Cuerpo de la encuesta ~ Color de fondo</td>');
            html.push('<td class="borderbottom" _bgcolor="1"></td>');
            html.push('</tr><tr>');
            html.push('<td colspan="3">Titulo de la encuesta</td>');
            html.push('</tr><tr>');
            html.push('<td class="borderbottom" _formtitlefamily="1"></td>');
            html.push('<td class="borderbottom" _formtitlefontsize="1"></td>');
            html.push('<td class="borderbottom" _formtitlefontcolor="1"></td>');
            html.push('</tr><tr>');
            html.push('<td colspan="3">Descripción de la encuesta</td>');
            html.push('</tr><tr>');
            html.push('<td class="borderbottom" _formdescriptionfamily="1"></td>');
            html.push('<td class="borderbottom" _formdescriptionfontsize="1"></td>');
            html.push('<td class="borderbottom" _formdescriptionfontcolor="1"></td>');
            html.push('</tr><tr>');
            html.push('<td colspan="3">Texto del título</td>');
            html.push('</tr><tr>');
            html.push('<td class="borderbottom" _fieldtitlefamily="1"></td>');
            html.push('<td class="borderbottom" _fieldtitlefontsize="1"></td>');
            html.push('<td class="borderbottom" _fieldtitlefontcolor="1"></td>');
            html.push('</tr><tr>');
            html.push('<td colspan="3">Texto de entrada | opciones</td>');
            html.push('</tr><tr>');
            html.push('<td class="borderbottom" _inputtitlefamily="1"></td>');
            html.push('<td class="borderbottom" _inputtitlefontsize="1"></td>');
            html.push('<td class="borderbottom" _inputtitlefontcolor="1"></td>');
            html.push('</tr>');
            html.push('</table></fieldset>');
            html.push('</div>');
            html.push('<div class="baserow">');
            html.push('<fieldset class="fieldseparator">');
            html.push('<legend>Separador de campos #{fieldSepTip}</legend>');
            html.push('<div class="separator-content">');
            html.push('<input #{sync}="separator" type="radio" name="separator" id="separator_normal" value="6" checked="checked" /><label for="separator_normal">Normal</label>');
            html.push('<input #{sync}="separator" type="radio" name="separator" id="separator_thin" value="2" /><label for="separator_thin">Fino</label>');
            html.push('<input #{sync}="separator" type="radio" name="separator" id="separator_none" value="0" /><label for="separator_none">Ninguno</label>');
            html.push('</div>');
            html.push('</fieldset>');
            html.push('</div>');
            html.push('<div class="baserow">');
            html.push('<input #{sync}="numbering" id="#{uuid}_numbering" type="checkbox" class="basecheck" checked="checked" /><label for="#{uuid}_numbering" class="basechecklabel"> Numerador de campos.</label>#{numberingTip}');
            html.push('</div>');
            html.push('</div>');
            return html.join('');
        },

        initPro: function () {
            // call base
            formInfo.superclass.initPro.call(this);
            var self = this;

            // custom style
            var rulesMgr = new yardi.pageRulesManager(document);
            var sheetsMgr = new yardi.sheetsHelper(document, rulesMgr);
            this.customSheet = yardi.styleSheetClass.resolveNode(document.getElementById(this.customCssId) || sheetsMgr.createStyleSheet('', this.customCssId));
            var setCustomCss = function (selector, name, value) {
                if (self.customSheet.indexOf(selector) == -1) {
                    self.customSheet.addRule(selector, name + ':' + value + ';');
                } else {
                    self.customSheet.setRuleStyle(selector, name, value);
                }
            };
            var getCustomCss = function (selector, name) {
                var r = rulesMgr.getRule(selector);
                if (r) {
                    name = self.customSheet.cameName(name);
                    return r.style[name];
                }
            };

            // separator 
            this.pItems('separator').each(function () {
                $(this).attr('checked', ($(this).val() == self.separator));
            }).change(function () {
                self.separator = parseInt($(this).val(), 10);
                self.el.attr('separator', self.separator);
            });

            // numbering
            this.pItems('numbering').attr('checked', this.numbering).change(function () {
                self.numbering = $(this).attr('checked');
                self.el.attr('numbering', self.numbering);
            });

            // reset style
            var resetBtn = new yardi.imageButton({
                title: 'reset style',
                renderTo: $('td[_resetstyle=1]', this.proEl),
                imageUrl: 'field/images/reset.png',
                onClick: function (ev) {
                    if (confirm('¿Está seguro de que desea restablecer el estilo?')) {
                        self.customSheet.clearRules();
                        window.setTimeout(updateControls, 300);
                    }
                }
            });

            // body background
            var bgColorPicker = new yardi.colorPickerButton({
                renderTo: $('td[_bgcolor=1]', this.proEl),
                onSelect: function (value) {
                    setCustomCss('.cusbody', 'background-color', value);
                    //setCustomCss('.s-field', 'background-color', value);
                },
                syncValue: function () {
                    var bgcolor = getCustomCss('.cusbody', 'background-color');
                    if (!bgcolor) bgcolor = yardi.currentStyle(self.el.get(0), 'background-color');
                    this.setColor(bgcolor);
                }
            });

            // form title text
            var formTitleFamily = new yardi.fontFamilyCombo({
                width: 160,
                renderTo: $('td[_formtitlefamily=1]', this.proEl),
                onSelect: function (item) {
                    setCustomCss('.s-forminfo .formtitle', 'font-family', item.text);
                },
                syncValue: function () {
                    var family = getCustomCss('.s-forminfo .formtitle', 'font-family');
                    if (!family) family = yardi.currentStyle(self.el.children().get(0), 'font-family');
                    this.val(family);
                }
            });
            var formTitleFontSize = new yardi.fontSizeCombo({
                width: 46,
                renderTo: $('td[_formtitlefontsize=1]', this.proEl),
                onSelect: function (item) {
                    var parser = new yardi.sizeUnitParser(item.text);
                    setCustomCss('.s-forminfo .formtitle', 'font-size', parser.toPx());
                },
                syncValue: function () {
                    var size = getCustomCss('.s-forminfo .formtitle', 'font-size');
                    if (!size) size = yardi.currentStyle(self.el.children().get(0), 'font-size');
                    this.val(size);
                }
            });
            var formTitleColorPicker = new yardi.colorPickerButton({
                iconType: 'fontcolor',
                renderTo: $('td[_formtitlefontcolor=1]', this.proEl),
                onSelect: function (value) {
                    setCustomCss('.s-forminfo .formtitle', 'color', value);
                },
                syncValue: function () {
                    var color = getCustomCss('.s-forminfo .formtitle', 'color');
                    if (!color) color = yardi.currentStyle(self.el.children().get(0), 'color');
                    this.setColor(color);
                }
            });

            // form description text
            var formDescriptionFamily = new yardi.fontFamilyCombo({
                width: 160,
                renderTo: $('td[_formdescriptionfamily=1]', this.proEl),
                onSelect: function (item) {
                    setCustomCss('.s-forminfo .description', 'font-family', item.text);
                },
                syncValue: function () {
                    var family = getCustomCss('.s-forminfo .description', 'font-family');
                    if (!family) family = yardi.currentStyle(self.el.children().get(1), 'font-family');
                    this.val(family);
                }
            });
            var formDescriptionFontSize = new yardi.fontSizeCombo({
                width: 46,
                renderTo: $('td[_formdescriptionfontsize=1]', this.proEl),
                onSelect: function (item) {
                    var parser = new yardi.sizeUnitParser(item.text);
                    setCustomCss('.s-forminfo .description', 'font-size', parser.toPx());
                },
                syncValue: function () {
                    var size = getCustomCss('.s-forminfo .description', 'font-size');
                    if (!size) size = yardi.currentStyle(self.el.children().get(1), 'font-size');
                    this.val(size);
                }
            });
            var formDescriptionColorPicker = new yardi.colorPickerButton({
                iconType: 'fontcolor',
                renderTo: $('td[_formdescriptionfontcolor=1]', this.proEl),
                onSelect: function (value) {
                    setCustomCss('.s-forminfo .description', 'color', value);
                },
                syncValue: function () {
                    var color = getCustomCss('.s-forminfo .description', 'color');
                    if (!color) color = yardi.currentStyle(self.el.children().get(1), 'color');
                    this.setColor(color);
                }
            });

            // field title text
            var fieldTitleFamily = new yardi.fontFamilyCombo({
                width: 160,
                renderTo: $('td[_fieldtitlefamily=1]', this.proEl),
                onSelect: function (item) {
                    setCustomCss('.s-field .custitle', 'font-family', item.text);
                },
                syncValue: function () {
                    var family = getCustomCss('.s-field .custitle', 'font-family');
                    if (!family) family = getCustomCss('.s-field .fieldtitle', 'font-family');
                    this.val(family);
                }
            });
            var fieldTitleFontSize = new yardi.fontSizeCombo({
                width: 46,
                renderTo: $('td[_fieldtitlefontsize=1]', this.proEl),
                onSelect: function (item) {
                    var value = new yardi.sizeUnitParser(item.text).toPx();
                    setCustomCss('.s-field .custitle', 'font-size', value);
                },
                syncValue: function () {
                    var size = getCustomCss('.s-field .custitle', 'font-size');
                    if (!size) size = getCustomCss('.s-field .fieldtitle', 'font-size');
                    this.val(size);
                }
            });
            var fieldTitleColorPicker = new yardi.colorPickerButton({
                iconType: 'fontcolor',
                renderTo: $('td[_fieldtitlefontcolor=1]', this.proEl),
                onSelect: function (value) {
                    setCustomCss('.s-field .custitle', 'color', value);
                },
                syncValue: function () {
                    var color = getCustomCss('.s-field .custitle', 'color');
                    if (!color) color = getCustomCss('.s-field .fieldtitle', 'color');
                    this.setColor(color);
                }
            });

            // input or options text
            var inputTitleFamily = new yardi.fontFamilyCombo({
                width: 160,
                renderTo: $('td[_inputtitlefamily=1]', this.proEl),
                onSelect: function (item) {
                    setCustomCss('.s-field .custext', 'font-family', item.text);
                },
                syncValue: function () {
                    var family = getCustomCss('.s-field .custext', 'font-family');
                    if (!family) family = getCustomCss('.s-field', 'font-family');
                    this.val(family);
                }
            });
            var inputTitleFontSize = new yardi.fontSizeCombo({
                width: 46,
                renderTo: $('td[_inputtitlefontsize=1]', this.proEl),
                onSelect: function (item) {
                    var parser = new yardi.sizeUnitParser(item.text);
                    setCustomCss('.s-field .custext', 'font-size', parser.toPx());
                },
                syncValue: function () {
                    var size = getCustomCss('.s-field .custext', 'font-size');
                    if (!size) size = getCustomCss('.s-field', 'font-size');
                    this.val(size);
                }
            });
            var inputTitleColorPicker = new yardi.colorPickerButton({
                iconType: 'fontcolor',
                renderTo: $('td[_inputtitlefontcolor=1]', this.proEl),
                onSelect: function (value) {
                    setCustomCss('.s-field .custext', 'color', value);
                },
                syncValue: function () {
                    var color = getCustomCss('.s-field .custext', 'color');
                    if (!color) color = getCustomCss('.s-field', 'color');
                    this.setColor(color);
                }
            });

            var updateControls = function () {
                rulesMgr.refreshCache();
                $.each([bgColorPicker, formTitleFamily, formTitleFontSize, formTitleColorPicker,
                                       formDescriptionFamily, formDescriptionFontSize, formDescriptionColorPicker,
                                       fieldTitleFamily, fieldTitleFontSize, fieldTitleColorPicker,
                                       inputTitleFamily, inputTitleFontSize, inputTitleColorPicker],
                function (index, item) {
                    if (item.syncValue) item.syncValue.call(item);
                });
            };

            window.setTimeout(updateControls, 300);
        },

        getTips: function () {
            var tipsData = formInfo.superclass.getTips.call(this);
            return $.extend(tipsData, {
                titleTip: {
                    title: 'Title',
                    message: 'Nombre de su Encuesta.'
                },
                descTip: {
                    title: 'Description',
                    message: 'Información adicional sobre la encuesta.'
                },
                fieldSepTip: {
                    title: 'Field Separator',
                    message: 'Establece la separación con respecto a los otros componentes'
                },
                fontAndColorsTip: {
                    title: 'Fonts & Colors',
                    message: 'Personalice su encuesta'
                },
                numberingTip: {
                    title: 'Numbering',
                    message: 'Numeración cronológica de cada campo'
                }
            });
        }

    });

    // register
    yardi.baseField.register('formInfo', formInfo);

})(jQuery);
