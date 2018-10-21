tinymce.PluginManager.add("link", function (t) {
    function e(e) {
        return function () {
            var n = t.settings.link_list;
            "string" == typeof n ? tinymce.util.XHR.send({
                url: n,
                success: function (t) {
                    e(tinymce.util.JSON.parse(t))
                }
            }) : "function" == typeof n ? n(e) : e(n)
        }
    }

    function n(t, e, n) {
        function i(t, n) {
            return n = n || [], tinymce.each(t, function (t) {
                var l = {
                    text: t.text || t.title
                };
                t.menu ? l.menu = i(t.menu) : (l.value = t.value, e && e(l)), n.push(l)
            }), n
        }
        return i(t, n || [])
    }

    function i(e) {
        function i(t) {
            var e = f.find("#text");
            (!e.value() || t.lastControl && e.value() == t.lastControl.text()) && e.value(t.control.text()), f.find("#href").value(t.control.value())
        }

        function l(e) {
            var n = [];
            return tinymce.each(t.dom.select("a:not([href])"), function (t) {
                var i = t.name || t.id;
                i && n.push({
                    text: i,
                    value: "#" + i,
                    selected: -1 != e.indexOf("#" + i)
                })
            }), n.length ? (n.unshift({
                text: "None",
                value: ""
            }), {
                name: "anchor",
                type: "listbox",
                label: "Anchors",
                values: n,
                onselect: i
            }) : void 0
        }

        function a() {
            !c && 0 === y.text.length && d && this.parent().parent().find("#text")[0].value(this.value())
        }

        function o(e) {
            var n = e.meta || {};
            x && x.value(t.convertURL(this.value(), "href")), tinymce.each(e.meta, function (t, e) {
                f.find("#" + e).value(t)
            }), n.text || a.call(this)
        }

        function r(t) {
            var e = b.getContent();
            if (/</.test(e) && (!/^<a [^>]+>[^<]+<\/a>$/.test(e) || -1 == e.indexOf("href="))) return !1;
            if (t) {
                var n, i = t.childNodes;
                if (0 === i.length) return !1;
                for (n = i.length - 1; n >= 0; n--)
                    if (3 != i[n].nodeType) return !1
            }
            return !0
        }
        var s, u, c, f, d, g, x, v, h, m, p, k, y = {},
            b = t.selection,
            _ = t.dom;
        s = b.getNode(), u = _.getParent(s, "a[href]"), d = r(), y.text = c = u ? u.innerText || u.textContent : b.getContent({
            format: "text"
        }), y.href = u ? _.getAttrib(u, "href") : "", (k = _.getAttrib(u, "target")) ? y.target = k : t.settings.default_link_target && (y.target = t.settings.default_link_target), (k = _.getAttrib(u, "rel")) && (y.rel = k), (k = _.getAttrib(u, "class")) && (y["class"] = k), (k = _.getAttrib(u, "title")) && (y.title = k), d && (g = {
            name: "text",
            type: "textbox",
            size: 40,
            label: "Text to display",
            onchange: function () {
                y.text = this.value()
            }
        }), e && (x = {
            type: "listbox",
            label: "Link list",
            values: n(e, function (e) {
                e.value = t.convertURL(e.value || e.url, "href")
            }, [{
                text: "None",
                value: ""
            }]),
            onselect: i,
            value: t.convertURL(y.href, "href"),
            onPostRender: function () {
                x = this
            }
        }), t.settings.target_list !== !1 && (t.settings.target_list || (t.settings.target_list = [{
            text: "None",
            value: ""
        }, {
            text: "New window",
            value: "_blank"
        }]), h = {
            name: "target",
            type: "listbox",
            label: "Target",
            values: n(t.settings.target_list)
        }), t.settings.rel_list && (v = {
            name: "rel",
            type: "listbox",
            label: "Rel",
            values: n(t.settings.rel_list)
        }), t.settings.link_class_list && (m = {
            name: "class",
            type: "listbox",
            label: "Class",
            values: n(t.settings.link_class_list, function (e) {
                e.value && (e.textStyle = function () {
                    return t.formatter.getCssText({
                        inline: "a",
                        classes: [e.value]
                    })
                })
            })
        }), t.settings.link_title !== !1 && (p = {
            name: "title",
            type: "textbox",
            label: "Title",
            value: y.title
        }), f = t.windowManager.open({
            title: "Insert link",
            data: y,
            body: [{
                name: "href",
                type: "filepicker",
                filetype: "file",
                size: 40,
                autofocus: !0,
                label: "Url",
                onchange: o,
                onkeyup: a
            }, g, p, l(y.href), x, v, h, m],
            onSubmit: function (e) {
                function n(e, n) {
                    var i = t.selection.getRng();
                    window.setTimeout(function () {
                        t.windowManager.confirm(e, function (e) {
                            t.selection.setRng(i), n(e)
                        })
                    }, 0)
                }

                function i() {
                    var e = {
                        href: l,
                        target: y.target ? y.target : null,
                        rel: y.rel ? y.rel : null,
                        "class": y["class"] ? y["class"] : null,
                        title: y.title ? y.title : null
                    };
                    u ? (t.focus(), d && y.text != c && ("innerText" in u ? u.innerText = y.text : u.textContent = y.text), _.setAttribs(u, e), b.select(u), t.undoManager.add()) : d ? t.insertContent(_.createHTML("a", e, _.encode(y.text))) : t.execCommand("mceInsertLink", !1, e)
                }
                var l;
                return y = tinymce.extend(y, e.data), (l = y.href) ? l.indexOf("@") > 0 && -1 == l.indexOf("//") && -1 == l.indexOf("mailto:") ? void n("The URL you entered seems to be an email address. Do you want to add the required mailto: prefix?", function (t) {
                    t && (l = "mailto:" + l), i()
                }) : /^\s*www\./i.test(l) ? void n("The URL you entered seems to be an external link. Do you want to add the required http:// prefix?", function (t) {
                    t && (l = "http://" + l), i()
                }) : void i() : void t.execCommand("unlink")
            }
        })
    }
    t.addButton("link", {
        icon: "link",
        tooltip: "Insert/edit link",
        shortcut: "Ctrl+K",
        onclick: e(i),
        stateSelector: "a[href]"
    }), t.addButton("unlink", {
        icon: "unlink",
        tooltip: "Remove link",
        cmd: "unlink",
        stateSelector: "a[href]"
    }), t.addShortcut("Ctrl+K", "", e(i)), t.addCommand("mceLink", e(i)), this.showDialog = i, t.addMenuItem("link", {
        icon: "link",
        text: "Insert link",
        shortcut: "Ctrl+K",
        onclick: e(i),
        stateSelector: "a[href]",
        context: "insert",
        prependToContext: !0
    })
});