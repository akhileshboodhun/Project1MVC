/// <amd-dependency path="dojo/_base/declare" name="dojo_declare" />
/// <amd-dependency path="dojo/_base/lang" name="lang"/>
/// <amd-dependency path="dijit/_Widget" name="_Widget"/>
/// <amd-dependency path="dijit/_TemplatedMixin" name="_TemplatedMixin"/>
/// <amd-dependency path="dijit/_WidgetsInTemplateMixin" name="_WidgetsInTemplateMixin"/>

/// <amd-dependency path="dojo/text!StaticViews/DashboardControl.html" name="template"/>
/// <amd-dependency path="dojox/mvc/WidgetList"/>
/// <amd-dependency path="dojox/mvc/at"/>
/// <amd-dependency path="dojox/mvc/Group"/>
/// <amd-dependency path="js/Tiles"/>

declare let dojo_declare;
declare let lang;
declare let _Widget;
declare let _TemplatedMixin;
declare let _WidgetsInTemplateMixin;

declare let template;

class DashboardControl {
    public set: (property: any, value: any) => void;
    public templateString = template;

    constructor(args) {
        this.set('searchRecords', args.searchRecords);
    }
}

export = dojo_declare("", [_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], DashboardControl.prototype);