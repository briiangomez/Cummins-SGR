/*
*
* base snippets
* author: ronglin
* create date: 2010.08.27
*
*/

yardi.baseSnippets = {

    fieldCommon: function (html, ctx, title) {
        html.push('<div class="fieldtop">&nbsp;</div>');
        html.push('<label class="fieldindex custitle"></label>');
        this.fieldtitle.f(html, ctx, title);
        this.required.f(html, ctx);
        this.guideline.f(html, ctx);
    },

    fieldtitle: {
        f: function (html, ctx, title) { html.push('<label #{sync}="fieldtitle" class="fieldtitle custitle">' + title + '</label>'); },
        p: function (html, ctx) {
            html.push('<div class="baserow">');
            html.push('<label class="baselabel">T&iacute;tulo #{titleTip}</label>');
            html.push('<textarea #{sync}="fieldtitle" class="fieldtitle" maxlength="50"></textarea>');
            html.push('</div>');
            // tips
            ctx.setTips({
                titleTip: {
                    title: 'Texto de la Sección',
                    message: 'Esta es la etiqueta que aparece en cada sección de la encuesta.'
                }
            });
        }
    },

    required: {
        f: function (html, ctx) { html.push('<label #{sync}="required" class="require">*</label>'); },
        p: function (html, ctx) {
            html.push('<div class="baserow">');
            html.push('<input #{sync}="required" id="#{uuid}_required" type="checkbox" class="basecheck" /><label for="#{uuid}_required" class="basechecklabel"> Este campo es requerido.</label>');
            html.push('</div>');
            // tips
            ctx.setTips({
                requiredTip: {
                    title: 'Requerido',
                    message: ''
                }
            });
        }
    },

    guideline: {
        f: function (html, ctx) { html.push('<label #{sync}="guideline" class="guideline"></label>'); },
        p: function (html, ctx) {
            html.push('<div class="baserow">');
            html.push('<label class="baselabel">Reglas de uso #{guidelineTip}</label>');
            html.push('<textarea #{sync}="guideline" class="guideline" maxlength="50"></textarea>');
            html.push('</div>');
            // tips
            ctx.setTips({
                guidelineTip: {
                    title: 'Reglas de uso',
                    message: 'Este texto se mostrará a quienes carguen la información. '
                }
            });
        }
    },

    comment: {
        f: function (html, ctx) {
            html.push('<div #{sync}="comment" class="comment custext">');
            html.push('<label>Observaciones:</label>');
            html.push('<textarea id="#{n}_comment" name="#{n}_comment"></textarea>');
            html.push('</div>');
        },
        p: function (html, ctx) {
            html.push('<div class="baserow">');
            html.push('<input #{sync}="comment" id="#{uuid}_comment" type="checkbox" class="basecheck" /><label for="#{uuid}_comment" class="basechecklabel"> Permitir guardar observaciones.</label>');
            html.push('</div>');
            // tips
            ctx.setTips({
                commentTip: {
                    title: 'Observaciones',
                    message: ''
                }
            });
        }
    }

};