/// <amd-dependency path="dojo/_base/declare" name="dojo_declare" />
/// <amd-dependency path="dojo/_base/lang" name="lang"/>
/// <amd-dependency path="dijit/_Widget" name="_Widget"/>
/// <amd-dependency path="dijit/_TemplatedMixin" name="_TemplatedMixin"/>
/// <amd-dependency path="dijit/_WidgetsInTemplateMixin" name="_WidgetsInTemplateMixin"/>
define(["require", "exports", "dojo/_base/declare", "dojo/_base/lang", "dijit/_Widget", "dijit/_TemplatedMixin", "dijit/_WidgetsInTemplateMixin", "dojo/text!StaticViews/DashboardControl.html", "dojox/mvc/WidgetList", "dojox/mvc/at", "dojox/mvc/Group", "js/Tiles"], function (require, exports, dojo_declare, lang, _Widget, _TemplatedMixin, _WidgetsInTemplateMixin, template) {
    /// <amd-dependency path="dojo/text!StaticViews/DashboardControl.html" name="template"/>
    /// <amd-dependency path="dojox/mvc/WidgetList"/>
    /// <amd-dependency path="dojox/mvc/at"/>
    /// <amd-dependency path="dojox/mvc/Group"/>
    /// <amd-dependency path="js/Tiles"/>
    var DashboardControl = /** @class */ (function () {
        function DashboardControl(args) {
            this.templateString = template;
            this.set('searchRecords', args.searchRecords);
        }
        return DashboardControl;
    }());
    return dojo_declare("", [_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], DashboardControl.prototype);
});
