/// <amd-dependency path="dojo/_base/declare" name="dojo_declare" />
/// <amd-dependency path="dojo/_base/lang" name="lang"/>
/// <amd-dependency path="dijit/_Widget" name="_Widget"/>
/// <amd-dependency path="dijit/_TemplatedMixin" name="_TemplatedMixin"/>
/// <amd-dependency path="dijit/_WidgetsInTemplateMixin" name="_WidgetsInTemplateMixin"/>
/// <amd-dependency path="dojo/text!StaticViews/Tiles.html" name="template"/>
/// <amd-dependency path="dojox/mvc/at"/>
define(["require", "exports", "dojo/_base/declare", "dojo/_base/lang", "dijit/_Widget", "dijit/_TemplatedMixin", "dijit/_WidgetsInTemplateMixin", "dojo/text!StaticViews/Tiles.html", "dojox/mvc/at"], function (require, exports, dojo_declare, lang, _Widget, _TemplatedMixin, _WidgetsInTemplateMixin, template) {
    var Tiles = /** @class */ (function () {
        function Tiles() {
            this.templateString = template;
        }
        Tiles.prototype.postCreate = function () {
            this.inherited(arguments);
        };
        Tiles.prototype.startup = function () {
            this.inherited(arguments);
        };
        Tiles.prototype._onClick = function () {
            window.location.href = this.location;
        };
        return Tiles;
    }());
    return dojo_declare("", [_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], Tiles.prototype);
});
