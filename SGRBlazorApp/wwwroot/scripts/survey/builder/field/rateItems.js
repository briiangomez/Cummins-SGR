(function () {

    var rateItems = function (config) {
        rateItems.superclass.constructor.call(this, config);
    };

    yardi.extend(rateItems, yardi.baseField, {

        itemCount: -1,

        groupCount: -1,

        identityCount: -1,

        inputIdentity: function () {
            this.identityCount++;
            return this.fieldName + '_group' + this.groupCount.toString() + '_' + this.identityCount.toString();
        },

        groupInputName: function () {
            this.groupCount++;
            return this.fieldName + '_group' + this.groupCount.toString();
        },

        serialize: function (base) {
            if (base != false) rateItems.superclass.serialize.call(this);
            this.el.attr('itemCount', this.itemCount);
            this.el.attr('groupCount', this.groupCount);
            this.el.attr('identityCount', this.identityCount);
        },

        deserialize: function () {
            rateItems.superclass.deserialize.call(this);
            this.itemCount = parseInt(this.el.attr('itemCount'), 10);
            this.groupCount = parseInt(this.el.attr('groupCount'), 10);
            this.identityCount = parseInt(this.el.attr('identityCount'), 10);
        },

        buildHtml: function () {
            var html = [], radioName = '';
            html.push('<div class="s-field s-rateitems">');
            yardi.baseSnippets.fieldCommon(html, this, 'Atributos a clasificar');
            html.push('<table cellpadding="0" cellspacing="0" border="0">');
            html.push('<tr>');
            html.push('<th class="attrs">&nbsp;</th>');
            this.itemCount++;
            html.push('<th #{sync}="' + this.itemCount + '" class="center custext">Bajo</th>');
            this.itemCount++;
            html.push('<th #{sync}="' + this.itemCount + '" class="center custext">Medio</th>');
            this.itemCount++;
            html.push('<th #{sync}="' + this.itemCount + '" class="center custext">Alto</th>');
            html.push('</tr>');
            html.push('<tr>');
            this.itemCount++;
            html.push('<td #{sync}="' + this.itemCount + '" empty="false" class="attrs custext">Attribute1</td>');
            radioName = this.groupInputName();
            html.push('<td class="center"><input id="' + this.inputIdentity() + '" name="' + radioName + '" type="radio" value="Bajo" /></td>');
            html.push('<td class="center"><input id="' + this.inputIdentity() + '" name="' + radioName + '" type="radio" value="Medio" /></td>');
            html.push('<td class="center"><input id="' + this.inputIdentity() + '" name="' + radioName + '" type="radio" value="Alto" /></td>');
            html.push('</tr>');
            html.push('<tr>');
            this.itemCount++;
            html.push('<td #{sync}="' + this.itemCount + '" empty="false" class="attrs custext">Attribute2</td>');
            radioName = this.groupInputName();
            html.push('<td class="center"><input id="' + this.inputIdentity() + '" name="' + radioName + '" type="radio" value="Bajo" /></td>');
            html.push('<td class="center"><input id="' + this.inputIdentity() + '" name="' + radioName + '" type="radio" value="Medio" /></td>');
            html.push('<td class="center"><input id="' + this.inputIdentity() + '" name="' + radioName + '" type="radio" value="Alto" /></td>');
            html.push('</tr>');
            html.push('<tr>');
            this.itemCount++;
            html.push('<td #{sync}="' + this.itemCount + '" empty="false" class="attrs custext">Attribute3</td>');
            radioName = this.groupInputName();
            html.push('<td class="center"><input id="' + this.inputIdentity() + '" name="' + radioName + '" type="radio" value="Bajo" /></td>');
            html.push('<td class="center"><input id="' + this.inputIdentity() + '" name="' + radioName + '" type="radio" value="Medio" /></td>');
            html.push('<td class="center"><input id="' + this.inputIdentity() + '" name="' + radioName + '" type="radio" value="Alto" /></td>');
            html.push('</tr>');
            html.push('</table>');
            yardi.baseSnippets.comment.f(html, this);
            html.push('</div>');
            return html.join('');
        },

        buildProHtml: function () {
            var html = [], self = this;;
            html.push('<div class="s-field-pro s-rateitems-pro">');
            yardi.baseSnippets.fieldtitle.p(html, this);
            html.push('<div class="baserow">');
            html.push('<fieldset class="choices">');
            html.push('<legend><span groupfor="cols">Clasificador</span> #{ratingScaleTip}</legend>');
            html.push('<ul group="cols">');
            $('th.center', this.el).each(function () {
                var name = $(this).attr(self.fAttr());
                html.push('<li>');
                html.push('<input #{sync}="' + name + '" type="text" maxlength="50" autocomplete="off" />');
                html.push('<img title="Add" alt="Add" src="#{img}/add.png" />');
                html.push('<img title="Delete" alt="Delete" src="#{img}/delete.gif" />');
                html.push('<img title="Move" style="cursor: move;" alt="Move" src="#{img}/move.gif" />');
                html.push('</li>');
            });
            html.push('</ul>');
            html.push('</fieldset>');
            html.push('</div>');
            html.push('<div class="baserow">');
            html.push('<fieldset class="choices">');
            html.push('<legend><span groupfor="rows">Ítems a clasificar</span> #{beRatedTip}</legend>');
            html.push('<ul group="rows">');
            $('td.attrs', this.el).each(function () {
                var name = $(this).attr(self.fAttr());
                html.push('<li>');
                html.push('<input #{sync}="' + name + '" type="text" maxlength="50" autocomplete="off" />');
                html.push('<img title="Add" alt="Add" src="#{img}/add.png" />');
                html.push('<img title="Delete" alt="Delete" src="#{img}/delete.gif" />');
                html.push('<img title="Move" style="cursor: move;" alt="Move" src="#{img}/move.gif" />');
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

        _addItem: function (source) {
            this.itemCount++;
            // add property
            var propertyParent = source.parent();
            var propertyCloned = propertyParent.clone(false).empty().insertAfter(propertyParent)
            propertyParent.children().clone(true).appendTo(propertyCloned);
            propertyCloned.children('input').attr(this.pAttr(), this.itemCount).val('').focus();
            // add field
            var self = this;
            var refName = source.siblings('input').attr(this.pAttr());
            var refItem = this.fItems(refName);
            if (refItem.attr('nodeName') == 'TH') {
                var cellIndex = refItem.attr('cellIndex');
                var tableRows = refItem.parent().parent().children();
                tableRows.each(function (index) {
                    var target = $(this).children().eq(cellIndex);
                    var cloned = target.clone(true);
                    if (index == 0) cloned.html('&nbsp;').attr(self.fAttr(), self.itemCount);
                    else cloned.val('');
                    cloned.insertAfter(target);
                    cloned.find('input').each(function () {
                        $(this).attr('id', self.inputIdentity());
                    });
                });
            }
            if (refItem.attr('nodeName') == 'TD') {
                var refParent = refItem.parent();
                var fieldCloned = refParent.clone(true);
                fieldCloned.insertAfter(refParent);
                fieldCloned.children().first().html('&nbsp;').attr(this.fAttr(), this.itemCount);
                // set group name
                var groupName = this.groupInputName();
                fieldCloned.find('input').each(function () {
                    $(this).attr('name', groupName).attr('id', self.inputIdentity());
                });
            }
            this.serialize(false);
        },

        _deleteItem: function (source) {
            // remove field
            var refName = source.siblings('input').attr(this.pAttr());
            var refItem = this.fItems(refName);
            if (refItem.attr('nodeName') == 'TH') {
                if (refItem.parent().children().length == 2) {
                    alert('Can not delete all rating scale.');
                } else {
                    // remove field
                    var cellIndex = refItem.attr('cellIndex');
                    var tableRows = refItem.parent().parent().children();
                    tableRows.each(function () {
                        $(this).children().eq(cellIndex).remove();
                    });
                    // remove property
                    source.parent().remove();
                }
            }
            if (refItem.attr('nodeName') == 'TD') {
                if (refItem.parent().parent().children().length == 2) {
                    alert('Can not delete all attribute items.');
                } else {
                    // remove field
                    refItem.parent().remove();
                    // remove property
                    source.parent().remove();
                }
            }
        },

        initPro: function () {
            // call base
            rateItems.superclass.initPro.call(this);
            // bind events
            var self = this, _dragging = false, _refName;
            $('img[alt=Add]', this.proEl).click(function () {
                if (!_dragging) { self._addItem($(this)); }
            });
            $('img[alt=Delete]', this.proEl).click(function () {
                if (!_dragging) { self._deleteItem($(this)); }
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
                    var refItem = self.fItems(_refName);
                    var cellIndex = refItem.attr('cellIndex');
                    var tableRows = refItem.parent().parent().children();
                    var pNext = ui.item.get(0).nextSibling;
                    if (refItem.attr('nodeName') == 'TH') {
                        if (pNext) {
                            var nextName = $(pNext).children('input').attr(self.pAttr());
                            var refCellIndex = self.fItems(nextName).attr('cellIndex');
                            tableRows.each(function (index) {
                                var target = $(this).children().eq(cellIndex);
                                var refTarget = $(this).children().eq(refCellIndex);
                                target.insertBefore(refTarget);
                            });
                        } else {
                            tableRows.each(function (index) {
                                var target = $(this).children().eq(cellIndex);
                                target.parent().append(target);
                            });
                        }
                    }
                    if (refItem.attr('nodeName') == 'TD') {
                        var row = refItem.parent();
                        if (pNext) {
                            var nextName = $(pNext).children('input').attr(self.pAttr());
                            var refRow = self.fItems(nextName).parent();
                            row.insertBefore(refRow);
                        } else {
                            row.parent().append(row);
                        }
                    }
                    _dragging = false;
                }
            });
        },

        onUpdateValue: function (sender, name) {
            rateItems.superclass.onUpdateValue.call(this, sender, name);
            var f = this.fItems(name).eq(0);
            if (f.attr('nodeName') == 'TH') {
                var colIndex = f[0].cellIndex;
                f.parent().siblings().each(function () {
                    $(this).children().eq(colIndex).children('input').val(sender.val());
                });
            }
        },

        getTips: function () {
            var tipsData = rateItems.superclass.getTips.call(this);
            return $.extend(tipsData, {
                ratingScaleTip: {
                    title: 'Escala de valores',
                    message: 'Escriba los nombres de los atributos a calificar en las siguientes cajas de texto. Estos nombres se convertirán en los títulos de las columnas para su clasificación.'
                },
                beRatedTip: {
                    title: 'Atributo del item',
                    message: 'Atributo del item a calsificar'
                }
            });
        }

    });

    // register
    yardi.baseField.register('rateItems', rateItems);

})();