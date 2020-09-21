﻿/*
*
* check list field
* author: ronglin
* create date: 2010.07.01
*
*/

(function () {

    var checkList = function (config) {
        checkList.superclass.constructor.call(this, config);
    };

    yardi.extend(checkList, yardi.baseField, {

        indexKey: -1,

        getInputId: function () {
            return this.fieldName + '_' + this.indexKey;
        },

        serialize: function (base) {
            if (base != false) checkList.superclass.serialize.call(this);
            this.el.attr('indexKey', this.indexKey);
        },

        deserialize: function () {
            checkList.superclass.deserialize.call(this);
            this.indexKey = parseInt(this.el.attr('indexKey'), 10);
        },

        buildHtml: function () {
            var html = [];
            html.push('<div class="s-field s-checklist">');
            yardi.baseSnippets.fieldCommon(html, this, 'Opci&oacute;n m&uacute;ltiple');
            html.push('<div class="group">');
            this.indexKey++;
            html.push('<div class="item">');
            html.push('<input #{sync}="' + this.indexKey + '" name="#{n}" id="' + this.getInputId() + '" type="checkbox" value="Opci&oacute;n 1"/>');
            html.push('<label #{sync}="' + this.indexKey + '" for="' + this.getInputId() + '" empty="false" class="custext">Opci&oacute;n 1</label>');
            html.push('</div>');
            this.indexKey++;
            html.push('<div class="item">');
            html.push('<input #{sync}="' + this.indexKey + '" name="#{n}" id="' + this.getInputId() + '" type="checkbox" value="Opci&oacute;n 2"/>');
            html.push('<label #{sync}="' + this.indexKey + '" for="' + this.getInputId() + '" empty="false" class="custext">Opci&oacute;n 2</label>');
            html.push('</div>');
            this.indexKey++;
            html.push('<div class="item">');
            html.push('<input #{sync}="' + this.indexKey + '" name="#{n}" id="' + this.getInputId() + '"  type="checkbox" value="Opci&oacute;n 3"/>');
            html.push('<label #{sync}="' + this.indexKey + '" for="' + this.getInputId() + '" empty="false" class="custext">Opci&oacute;n 3</label>');
            html.push('</div>');
            html.push('</div>');
            yardi.baseSnippets.comment.f(html, this);
            html.push('</div>');
            return html.join('');
        },

        buildProHtml: function () {
            var html = [];
            html.push('<div class="s-field-pro s-checklist-pro">');
            yardi.baseSnippets.fieldtitle.p(html, this);
            html.push('<div class="baserow">');
            html.push('<fieldset class="choices">');
            html.push('<legend><span groupfor="items">Opciones</span> #{choicesTip}</legend>');
            html.push('<ul group="items">');
            var self = this;
            $('input[type=checkbox]', this.el).each(function () {
                var name = $(this).next().attr(self.fAttr());
                var checkedImg = $(this).attr('checked') ? 'star.gif' : 'stardim.gif';
                html.push('<li>');
                html.push('<input #{sync}="' + name + '" type="text" maxlength="250" autocomplete="off" />');
                html.push('<img title="Agregar" alt="Add" src="#{img}/add.png" />');
                html.push('<img title="Eliminar" alt="Delete" src="#{img}/delete.gif" />');
                //html.push('<img title="Make Default" alt="Make Default" src="#{img}/' + checkedImg + '" />');
                html.push('<img title="Mover" style="cursor: move;" alt="Move" src="#{img}/move.gif" />');
                html.push('</li>');
            });
            html.push('</ul>');
            html.push('</fieldset>');
            html.push('</div>');
            yardi.baseSnippets.guideline.p(html, this);
            yardi.baseSnippets.comment.p(html, this);
            yardi.baseSnippets.required.p(html, this);
            html.push('</div>');
            return html.join('');
        },

        _copyItem: function (source) {
            this.indexKey++;
            this.serialize(false);
            // add property
            var propertyParent = source.parent();
            var propertyCloned = propertyParent.clone(false).empty().insertAfter(propertyParent)
            propertyParent.children().clone(true).appendTo(propertyCloned);
            propertyCloned.children('input').attr(this.pAttr(), this.indexKey).val('').focus();
            propertyCloned.children('img[alt=Make Default]').each(function () {
                $(this).attr('src', $(this).attr('src').replace('star.gif', 'stardim.gif'));
            });
            // add field
            var refName = source.siblings('input').attr(this.pAttr());
            var fieldParent = this.fItems(refName).parent();
            var fieldCloned = fieldParent.clone(true).insertAfter(fieldParent);
            fieldCloned.children('input').attr('checked', false).removeAttr('CHECKED').attr(this.fAttr(), this.indexKey).attr('id', this.getInputId());
            fieldCloned.children('label').html('&nbsp;').attr(this.fAttr(), this.indexKey).attr('for', this.getInputId());
        },

        _deleteItem: function (source) {
            if ($('img[alt=Delete]', this.proEl).length == 1) {
                alert('Can not delete all choices.');
            } else {
                // remove field
                var refName = source.siblings('input').attr(this.pAttr());
                this.fItems(refName).parent().remove();
                // remove property
                source.parent().remove();
            }
        },

        _selectItem: function (source) {
            // current src
            var oldsrc = source.attr('src');
            var checked = (oldsrc.indexOf('star.gif') > -1);
            // revert
            source.attr('src', checked ? oldsrc.replace('star.gif', 'stardim.gif') : oldsrc.replace('stardim.gif', 'star.gif'));
            checked = !checked;
            // set
            var refName = source.siblings('input').attr(this.pAttr());
            var checkBtn = this.fItems(refName).prev();
            checkBtn.attr('checked', checked);
            if (checked) {
                checkBtn.attr('CHECKED', 'checked');
            } else {
                checkBtn.removeAttr('CHECKED');
            }
        },

        initPro: function () {
            checkList.superclass.initPro.call(this);
            var self = this, _dragging = false, _refName;
            // buttons
            $('img[alt=Add]', this.proEl).click(function () {
                if (!_dragging) { self._copyItem($(this)); }
            });
            $('img[alt=Delete]', this.proEl).click(function () {
                if (!_dragging) { self._deleteItem($(this)); }
            });
            $('img[alt=Make Default]', this.proEl).click(function () {
                if (!_dragging) { self._selectItem($(this)); }
            });
            // sortable
            $('ul', this.proEl).sortable({
                axis: 'y',
                revert: true,
                distance: 5,
                containment: this.proEl,
                forcePlaceholderSize: true,
                placeholder: 'holder',
                start: function (event, ui) {
                    _dragging = true;
                    _refName = ui.helper.children('input').attr(self.pAttr());
                    // fix revert position bug
                    ui.helper.css('left', ui.originalPosition.left);
                },
                stop: function (event, ui) {
                    var fTarget = self.fItems(_refName).parent();
                    var pNext = ui.item.get(0).nextSibling;
                    if (pNext) {
                        var nextName = $(pNext).children('input').attr(self.pAttr());
                        var nextTarget = self.fItems(nextName).parent();
                        fTarget.insertBefore(nextTarget);
                    } else {
                        fTarget.parent().append(fTarget);
                    }
                    _dragging = false;
                }
            });
        },

        getTips: function () {
            var tipsData = checkList.superclass.getTips.call(this);
            return $.extend(tipsData, {
                choicesTip: {
                    title: 'Opciones',
                    //message: 'Utilice los iconos de más o menos para añadir o eliminar opciones. Haga clic en la estrella para hacer una elección de la selección por defecto.'
                    message: 'Utilice los iconos de más o menos para añadir o eliminar opciones.'
                }
            });
        }

    });

    // register
    yardi.baseField.register('checkList', checkList);

})();