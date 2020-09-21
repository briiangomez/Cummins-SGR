/*
*
* listbox field
* author: ronglin
* create date: 2010.10.20
*
*/

(function () {

    var listBox = function (config) {
        listBox.superclass.constructor.call(this, config);
    };

    yardi.extend(listBox, yardi.baseField, {

        buildHtml: function () {
        }

    });

    // register
    yardi.baseField.register('listBox', listBox);

})();