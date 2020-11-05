/*
*
* colorPicker
* author: ronglin
* create date: 2010.06.03
*
*/

(function () {

    var colorPickerCore = function (config) {
        config.width = 370;
        config.height = null;
        config.buildFn = function () {
            return '<div class="kb-colorpicker">' +
            '<div class="kb-head"></div>' +
            '<div class="kb-bar">' +
            '<strong>Click on a color chip or enter a hex code value.</strong>' +
            '<div class="kb-row"><div class="kb-left">Original:</div><div class="kb-right" ori="1"></div></div>' +
            '<div class="kb-row"><div class="kb-left">New:</div><div class="kb-right" new="1"></div></div>' +
            '<div class="kb-row"><div class="kb-left">Hex Value:</div><input type="text" maxlength="7" class="kb-text" /></div>' +
            '<div class="kb-row"><input class="kb-btn" type="button" value="Use New Color" /><input class="kb-btn" type="button" value="Cancel" /></div>' +
            '</div>' +
            '<table cellspacing="0" cellpadding="0" border="0" class="kb-grid" ' +
            // disabled selection
            'unselectable="on" onselectstart="return false;" style="-moz-user-select: none;"><tbody>' +
            '<tr>' +
                '<td style="background-color: rgb(0, 0, 0);" cval="#000000">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 0, 51);" cval="#000033">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 0, 102);" cval="#000066">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 0, 153);" cval="#000099">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 0, 204);" cval="#0000CC">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 0, 255);" cval="#0000FF">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 0, 0);" cval="#330000">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 0, 51);" cval="#330033">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 0, 102);" cval="#330066">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 0, 153);" cval="#330099">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 0, 204);" cval="#3300CC">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 0, 255);" cval="#3300FF">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 0, 0);" cval="#660000">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 0, 51);" cval="#660033">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 0, 102);" cval="#660066">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 0, 153);" cval="#660099">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 0, 204);" cval="#6600CC">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 0, 255);" cval="#6600FF">&nbsp;</td>' +
            '</tr>' +
            '<tr>' +
                '<td style="background-color: rgb(153, 0, 0);" cval="#990000">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 0, 51);" cval="#990033">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 0, 102);" cval="#990066">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 0, 153);" cval="#990099">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 0, 204);" cval="#9900CC">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 0, 255);" cval="#9900FF">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 0, 0);" cval="#CC0000">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 0, 51);" cval="#CC0033">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 0, 102);" cval="#CC0066">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 0, 153);" cval="#CC0099">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 0, 204);" cval="#CC00CC">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 0, 255);" cval="#CC00FF">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 0, 0);" cval="#FF0000">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 0, 51);" cval="#FF0033">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 0, 102);" cval="#FF0066">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 0, 153);" cval="#FF0099">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 0, 204);" cval="#FF00CC">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 0, 255);" cval="#FF00FF">&nbsp;</td>' +
            '</tr>' +
            '<tr>' +
                '<td style="background-color: rgb(0, 51, 0);" cval="#003300">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 51, 51);" cval="#003333">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 51, 102);" cval="#003366">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 51, 153);" cval="#003399">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 51, 204);" cval="#0033CC">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 51, 255);" cval="#0033FF">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 51, 0);" cval="#333300">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 51, 51);" cval="#333333">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 51, 102);" cval="#333366">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 51, 153);" cval="#333399">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 51, 204);" cval="#3333CC">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 51, 255);" cval="#3333FF">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 51, 0);" cval="#663300">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 51, 51);" cval="#663333">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 51, 102);" cval="#663366">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 51, 153);" cval="#663399">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 51, 204);" cval="#6633CC">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 51, 255);" cval="#6633FF">&nbsp;</td>' +
            '</tr>' +
            '<tr>' +
                '<td style="background-color: rgb(153, 51, 0);" cval="#993300">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 51, 51);" cval="#993333">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 51, 102);" cval="#993366">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 51, 153);" cval="#993399">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 51, 204);" cval="#9933CC">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 51, 255);" cval="#9933FF">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 51, 0);" cval="#CC3300">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 51, 51);" cval="#CC3333">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 51, 102);" cval="#CC3366">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 51, 153);" cval="#CC3399">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 51, 204);" cval="#CC33CC">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 51, 255);" cval="#CC33FF">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 51, 0);" cval="#FF3300">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 51, 51);" cval="#FF3333">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 51, 102);" cval="#FF3366">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 51, 153);" cval="#FF3399">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 51, 204);" cval="#FF33CC">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 51, 255);" cval="#FF33FF">&nbsp;</td>' +
            '</tr>' +
            '<tr>' +
                '<td style="background-color: rgb(0, 102, 0);" cval="#006600">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 102, 51);" cval="#006633">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 102, 102);" cval="#006666">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 102, 153);" cval="#006699">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 102, 204);" cval="#0066CC">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 102, 255);" cval="#0066FF">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 102, 0);" cval="#336600">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 102, 51);" cval="#336633">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 102, 102);" cval="#336666">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 102, 153);" cval="#336699">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 102, 204);" cval="#3366CC">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 102, 255);" cval="#3366FF">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 102, 0);" cval="#666600">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 102, 51);" cval="#666633">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 102, 102);" cval="#666666">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 102, 153);" cval="#666699">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 102, 204);" cval="#6666CC">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 102, 255);" cval="#6666FF">&nbsp;</td>' +
            '</tr>' +
            '<tr>' +
                '<td style="background-color: rgb(153, 102, 0);" cval="#996600">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 102, 51);" cval="#996633">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 102, 102);" cval="#996666">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 102, 153);" cval="#996699">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 102, 204);" cval="#9966CC">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 102, 255);" cval="#9966FF">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 102, 0);" cval="#CC6600">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 102, 51);" cval="#CC6633">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 102, 102);" cval="#CC6666">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 102, 153);" cval="#CC6699">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 102, 204);" cval="#CC66CC">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 102, 255);" cval="#CC66FF">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 102, 0);" cval="#FF6600">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 102, 51);" cval="#FF6633">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 102, 102);" cval="#FF6666">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 102, 153);" cval="#FF6699">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 102, 204);" cval="#FF66CC">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 102, 255);" cval="#FF66FF">&nbsp;</td>' +
            '</tr>' +
            '<tr>' +
                '<td style="background-color: rgb(0, 153, 0);" cval="#009900">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 153, 51);" cval="#009933">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 153, 102);" cval="#009966">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 153, 153);" cval="#009999">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 153, 204);" cval="#0099CC">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 153, 255);" cval="#0099FF">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 153, 0);" cval="#339900">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 153, 51);" cval="#339933">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 153, 102);" cval="#339966">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 153, 153);" cval="#339999">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 153, 204);" cval="#3399CC">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 153, 255);" cval="#3399FF">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 153, 0);" cval="#669900">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 153, 51);" cval="#669933">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 153, 102);" cval="#669966">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 153, 153);" cval="#669999">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 153, 204);" cval="#6699CC">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 153, 255);" cval="#6699FF">&nbsp;</td>' +
            '</tr>' +
            '<tr>' +
                '<td style="background-color: rgb(153, 153, 0);" cval="#999900">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 153, 51);" cval="#999933">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 153, 102);" cval="#999966">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 153, 153);" cval="#999999">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 153, 204);" cval="#9999CC">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 153, 255);" cval="#9999FF">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 153, 0);" cval="#CC9900">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 153, 51);" cval="#CC9933">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 153, 102);" cval="#CC9966">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 153, 153);" cval="#CC9999">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 153, 204);" cval="#CC99CC">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 153, 255);" cval="#CC99FF">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 153, 0);" cval="#FF9900">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 153, 51);" cval="#FF9933">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 153, 102);" cval="#FF9966">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 153, 153);" cval="#FF9999">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 153, 204);" cval="#FF99CC">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 153, 255);" cval="#FF99FF">&nbsp;</td>' +
            '</tr>' +
            '<tr>' +
                '<td style="background-color: rgb(0, 204, 0);" cval="#00CC00">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 204, 51);" cval="#00CC33">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 204, 102);" cval="#00CC66">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 204, 153);" cval="#00CC99">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 204, 204);" cval="#00CCCC">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 204, 255);" cval="#00CCFF">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 204, 0);" cval="#33CC00">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 204, 51);" cval="#33CC33">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 204, 102);" cval="#33CC66">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 204, 153);" cval="#33CC99">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 204, 204);" cval="#33CCCC">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 204, 255);" cval="#33CCFF">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 204, 0);" cval="#66CC00">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 204, 51);" cval="#66CC33">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 204, 102);" cval="#66CC66">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 204, 153);" cval="#66CC99">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 204, 204);" cval="#66CCCC">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 204, 255);" cval="#66CCFF">&nbsp;</td>' +
            '</tr>' +
            '<tr>' +
                '<td style="background-color: rgb(153, 204, 0);" cval="#99CC00">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 204, 51);" cval="#99CC33">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 204, 102);" cval="#99CC66">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 204, 153);" cval="#99CC99">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 204, 204);" cval="#99CCCC">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 204, 255);" cval="#99CCFF">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 204, 0);" cval="#CCCC00">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 204, 51);" cval="#CCCC33">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 204, 102);" cval="#CCCC66">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 204, 153);" cval="#CCCC99">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 204, 204);" cval="#CCCCCC">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 204, 255);" cval="#CCCCFF">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 204, 0);" cval="#FFCC00">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 204, 51);" cval="#FFCC33">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 204, 102);" cval="#FFCC66">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 204, 153);" cval="#FFCC99">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 204, 204);" cval="#FFCCCC">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 204, 255);" cval="#FFCCFF">&nbsp;</td>' +
            '</tr>' +
            '<tr>' +
                '<td style="background-color: rgb(0, 255, 0);" cval="#00FF00">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 255, 51);" cval="#00FF33">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 255, 102);" cval="#00FF66">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 255, 153);" cval="#00FF99">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 255, 204);" cval="#00FFCC">&nbsp;</td>' +
                '<td style="background-color: rgb(0, 255, 255);" cval="#00FFFF">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 255, 0);" cval="#33FF00">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 255, 51);" cval="#33FF33">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 255, 102);" cval="#33FF66">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 255, 153);" cval="#33FF99">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 255, 204);" cval="#33FFCC">&nbsp;</td>' +
                '<td style="background-color: rgb(51, 255, 255);" cval="#33FFFF">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 255, 0);" cval="#66FF00">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 255, 51);" cval="#66FF33">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 255, 102);" cval="#66FF66">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 255, 153);" cval="#66FF99">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 255, 204);" cval="#66FFCC">&nbsp;</td>' +
                '<td style="background-color: rgb(102, 255, 255);" cval="#66FFFF">&nbsp;</td>' +
            '</tr>' +
            '<tr>' +
                '<td style="background-color: rgb(153, 255, 0);" cval="#99FF00">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 255, 51);" cval="#99FF33">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 255, 102);" cval="#99FF66">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 255, 153);" cval="#99FF99">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 255, 204);" cval="#99FFCC">&nbsp;</td>' +
                '<td style="background-color: rgb(153, 255, 255);" cval="#99FFFF">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 255, 0);" cval="#CCFF00">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 255, 51);" cval="#CCFF33">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 255, 102);" cval="#CCFF66">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 255, 153);" cval="#CCFF99">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 255, 204);" cval="#CCFFCC">&nbsp;</td>' +
                '<td style="background-color: rgb(204, 255, 255);" cval="#CCFFFF">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 255, 0);" cval="#FFFF00">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 255, 51);" cval="#FFFF33">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 255, 102);" cval="#FFFF66">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 255, 153);" cval="#FFFF99">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 255, 204);" cval="#FFFFCC">&nbsp;</td>' +
                '<td style="background-color: rgb(255, 255, 255);" cval="#FFFFFF">&nbsp;</td>' +
            '</tr>' +
            '</tbody></table>' +
            '</div>';
        };
        colorPickerCore.superclass.constructor.call(this, config);
    };

    yardi.extend(colorPickerCore, yardi.pickerPanel, {

        title: 'Color Picker',

        defaultColor: 'transparent',

        onSelect: null,

        onPreview: null,

        onCancel: null,

        bindEvents: function () {
            colorPickerCore.superclass.bindEvents.call(this);
            var self = this;
            // title
            var head = this.el.find('.kb-head');
            if (this.title) {
                head.html(this.title);
            } else {
                head.remove();
            }
            // text
            var text = this.el.find('input[type=text]');
            var newColor = this.el.find('div[new=1]');
            var original = this.el.find('div[ori=1]');
            var last_hl_td;
            this.el.find('.kb-grid').click(function (ev) {
                if (ev.target.nodeName == 'TD') {
                    var cval = $(ev.target).attr('cval');
                    text.val(cval);
                    newColor.css('backgroundColor', cval).attr('cval', cval);
                    if (self.onPreview) { self.onPreview(cval, ev); }
                }
            }).dblclick(function (ev) {
                if (ev.target.nodeName == 'TD') {
                    btnOk.click();
                }
            }).mousemove(function (ev) {
                window.setTimeout(function () {
                    if (ev.target.nodeName == 'TD') {
                        if (last_hl_td != ev.target) {
                            $(ev.target).addClass('hl');
                            $(last_hl_td).removeClass('hl');
                            last_hl_td = ev.target;
                        }
                    } else {
                        $(last_hl_td).removeClass('hl');
                    }
                }, yardi.isIE ? 50 : 100);
            });
            original.css('cursor', 'pointer').click(function (ev) {
                var val = $(this).attr('cval');
                self.setColor(val);
                self.onPreview && self.onPreview(val, ev);
            });
            text.keyup(function (ev) {
                if (this.value.length == 7 || this.value.length == 4) {
                    try {
                        newColor.css('backgroundColor', this.value).attr('cval', this.value);
                        self.onPreview && self.onPreview(this.value, ev);
                    } catch (ex) { }
                }
            });
            // btns
            var btnOk = this.el.find('input[type=button]').eq(0);
            var btnCancel = this.el.find('input[type=button]').eq(1);
            btnOk.click(function (ev) {
                self.onSelect(newColor.attr('cval'), ev);
                colorPickerCore.superclass.hide.call(self);
            });
            btnCancel.click(function (ev) {
                colorPickerCore.superclass.hide.call(self);
                self.onCancel && self.onCancel(original.attr('cval'), ev);
            });
        },

        setColor: function (cval) {
            if (cval) {
                cval = yardi.colorHex(cval);
                this.el.find('input[type=text]').val(cval);
                this.el.find('div[new=1]').css('backgroundColor', cval).attr('cval', cval);
                this.el.find('div[ori=1]').css('backgroundColor', cval).attr('cval', cval);
            }
        },

        hide: function () {
            colorPickerCore.superclass.hide.call(this);
            if (this.onCancel) {
                var original = this.el.find('div[ori=1]');
                this.onCancel(original.attr('cval'), null);
            }
        },

        show: function (refEl, color) {
            colorPickerCore.superclass.show.call(this, refEl);
            this.setColor(color || this.defaultColor);
        }
    });

    var colorPicker = function () {
        var _upper = function (c) { return (c || '').toUpperCase(); };
        var select, preview, cancel;
        var picker = new colorPickerCore({
            onSelect: function (color, ev) { if (select) select(_upper(color), ev); },
            onPreview: function (color, ev) { if (preview) preview(_upper(color), ev); },
            onCancel: function (color, ev) { if (cancel) cancel(_upper(color), ev); }
        });
        return {
            show: function (refEl, color, refSelect, refPreview, refCancel) {
                picker.show(refEl, _upper(color));
                select = refSelect;
                preview = refPreview;
                cancel = refCancel;
            },
            hide: function () {
                picker.hide();
                select = null;
                preview = null;
                cancel = null;
            }
        };
    } ();

    // register
    yardi.colorPicker = colorPicker;

})();