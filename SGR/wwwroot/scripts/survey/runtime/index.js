(function () {

    /// <summary>
    /// 
    /// </summary>
    var core = {
        ie: function () {
            return ! -[1, ];
        } (),
        on: function (o, type, fn) {
            if (o.addEventListener) { o.addEventListener(type, fn, false); }
            else if (o.attachEvent) { o.attachEvent('on' + type, fn); }
            else { o['on' + type] = fn; }
        },
        unon: function (o, type, fn) {
            if (o.removeEventListener) { o.removeEventListener(type, fn); }
            else if (o.detachEvent) { o.detachEvent('on' + type, fn); }
            else { o['on' + type] = null; }
        },
        prevent: function (ev) {
            if (ev.preventDefault) { ev.preventDefault(); }
            else { ev.returnValue = false; }
        },
        css: function (el, p, v) {
            if (v === undefined) {
                var s = el.currentStyle || el.ownerDocument.defaultView.getComputedStyle(el, null);
                return (p === undefined) ? s : s[p];
            } else {
                el.style[p] = v;
            }
        },
        each: function (o, cb) {
            if (undefined === o.length) {
                for (var k in o) {
                    if (cb(o[k], k, o) === false) { break; }
                }
            } else {
                for (var i = 0, len = o.length; i < len; i++) {
                    if (i in o) { if (cb(o[i], i, o) === false) break; }
                }
            }
        },
        apply: function (o, c) {
            if (o && c && typeof c == 'object') { for (var p in c) o[p] = c[p]; }
            return o;
        },
        contains: function (p, c) {
            var ret = false;
            if (p && c) {
                if (p.contains) {
                    return p.contains(c);
                } else if (p.compareDocumentPosition) {
                    return !!(p.compareDocumentPosition(c) & 16);
                } else {
                    while (c = c.parentNode) { ret = c == p || ret; }
                }
            }
            return ret;
        },
        getBy: function (tag, fit, ctx) {
            var list = (ctx || document).getElementsByTagName(tag), els = [], len = 0;
            core.each(list, function (o) {
                if (!fit || fit(o))
                    els[len++] = o;
            });
            return els;
        }
    };

    var $ = function (fn) { $.on(window, 'load', fn); };
    core.apply($, core);

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Work Item</returns>
    var work = function () {
        var guide;
        var guideCss = function () {
            if (!guide) { return; }
            var ref = guide._refElement;
            $.css(guide, 'top', (ref.offsetTop) + 'px');
            $.css(guide, 'left', (ref.offsetLeft + ref.offsetWidth + 2) + 'px');
        };
        $.on(window, 'resize', guideCss);
        // ret
        return {
            showGuide: function (el, message) {
                message = message || el.guideline.innerHTML;
                if (!message) return;
                work.hideGuide();
                guide = document.createElement('div');
                guide.className = 'runtime-guideline';
                guide.appendChild(document.createTextNode(message));
                guide._refElement = el;
                document.body.appendChild(guide);
                guideCss();
            },
            hideGuide: function () {
                if (guide) {
                    guide.parentNode.removeChild(guide);
                    guide = null;
                }
            },
            scrollIntoView: function (el) {
                el.scrollIntoView(false);
                el._oldbg = el.style.backgroundColor;
                el.style.backgroundColor = '#E8F2FF';
                $.on(el, ($.ie ? 'focusin' : 'click'), function () {
                    el.style.backgroundColor = el._oldbg;
                    work.hideGuide();
                });
            }
        };
    } ();

    // Vinculo => guideline
    $(function () {
        var timeoutId, fields = [];
        var elems = $.getBy('label', function (item) {
            return (item.className == 'guideline');
        });
        $.each(elems, function (o) {
            o.parentNode.guideline = o;
            fields.push(o.parentNode);
        });
        $.on(document, 'mousemove', function (ev) {
            var target = ev.srcElement || ev.target;
            window.clearTimeout(timeoutId);
            timeoutId = setTimeout(function () {
                var field;
                $.each(fields, function (f) {
                    if (f == target || $.contains(f, target)) {
                        field = f;
                        return false;
                    }
                });
                if (field) { work.showGuide(field); }
            }, 50);
        });
    });

    // Vinculo => Cerrar
    $(function () {
        var btnClose = document.getElementById("btnClose");
        if (btnClose) {
            $.on(btnClose, 'click', function () {
                window.opener = null;
                window.open('', '_self');
                window.close();
            });
        }
    });

    // Vinculo => Valores requeridod
    $(function () {
        // collect verification
        var form = document.forms[0];
        if (!form) { return; }
        var validates = {}, parents = [];
        var labels = $.getBy('label', function (item) {
            return (item.className == 'require');
        });
        $.each(labels, function (lbl) {
            if ($.css(lbl, 'display') == 'inline') {
                var els = [], p = lbl.parentNode;
                $.each(['input', 'textarea', 'select'], function (tag) {
                    var os = $.getBy(tag, null, p);
                    if (os.length > 0) { els = els.concat(os); }
                });
                var key = p.getAttribute('fieldName');
                validates[key] = els;
                parents[key] = p;
            }
        });
        // Vinculo => form submit
        $.on(form, 'submit', function (ev) {
            $.each(validates, function (els, key) {
                var empty = true;
                $.each(els, function (el) {
                    if (el.parentNode.className == 'comment' && $.css(el.parentNode, 'display') == 'none')
                        return true; // continue
                    if ((el.type == 'checkbox' || el.type == 'radio') && el.checked === true) {
                        empty = false;
                    } else if ((el.tagName == 'TEXTAREA' || el.type == 'text') && el.value != '') {
                        empty = false;
                    } else if (el.tagName == 'SELECT' && el.selectedIndex > -1 && el.childNodes[el.selectedIndex].innerText != '') {
                        empty = false;
                    }
                });
                if (empty) {
                    $.prevent(ev);
                    work.scrollIntoView(parents[key]);
                    work.showGuide(parents[key], 'Se requiere introducir este campo.');
                    return false; // break
                }
            });
        });
    });

})();