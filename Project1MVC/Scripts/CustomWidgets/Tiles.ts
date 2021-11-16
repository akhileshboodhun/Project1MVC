/// <amd-dependency path="dojo/_base/declare" name="dojo_declare" />
/// <amd-dependency path="dojo/_base/lang" name="lang"/>
/// <amd-dependency path="dijit/_Widget" name="_Widget"/>
/// <amd-dependency path="dijit/_TemplatedMixin" name="_TemplatedMixin"/>
/// <amd-dependency path="dijit/_WidgetsInTemplateMixin" name="_WidgetsInTemplateMixin"/>
/// <amd-dependency path="dojo/text!StaticViews/Tiles.html" name="template"/>
/// <amd-dependency path="dojox/mvc/at"/>

declare let dojo_declare;
declare let lang;
declare let _Widget;
declare let _TemplatedMixin;
declare let _WidgetsInTemplateMixin;
declare let template;

class Tiles {
    private inherited;
    private set: any;

    public templateString = template;

    public label: string;
    public icon: string;
    public desc: string;
    public location: string;

    postCreate() {
        this.inherited(arguments);
    }

    startup() {
        this.inherited(arguments);
    }

    _onClick() {
        window.location.href = this.location;
    }

}

export = dojo_declare("", [_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], Tiles.prototype);