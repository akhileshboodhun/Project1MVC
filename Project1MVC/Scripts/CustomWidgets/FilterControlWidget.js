define(['dojo/_base/declare',
    'dojo/_base/lang',
    'dijit/_Widget',
    'dijit/_TemplatedMixin',
    'dijit/_WidgetsInTemplateMixin',
    'dojo/data/ItemFileWriteStore',
    'dojo/request',
    'dojo/query',
    "dojo/_base/array",
    "dojo/dom-construct",
    "dojo/dom",
    "dojo/on",
    "dijit/form/TextBox",
    'dojo/text!StaticViews/FilterControlWidget.html',
    'dojox/mvc/at',
    
],
    function (dojo_declare, lang, _Widget, _TemplatedMixin, _WidgetsInTemplateMixin, ItemFileWriteStore, request, query, array, domConstruct, dom, on, TextBox, template) {

        var proto = {
            templateString: template,
            filterProperties: null,

            createdControls: [],
            attachPoint: null,

            constructor: function (args) {
                if (args) {
                    this.set('filterProperties', args);
                }
            },

            postCreate: function () {
                this.inherited(arguments);
            },

            startup: function () {
                this.inherited(arguments);
                this._addFilterControl();
            },

            _addFilterControl: function () {
                if (this.filterProperties) {
                    var type = this.filterProperties.type;
                    var label = this.filterProperties.name;
                    var control;
                    if (type == 'textbox') {
                        this.attachPoint = 'txt' + label;

                        control = new TextBox({
                            name: label,
                            'data-dojo-attach-point': this.attachPoint,
                            id: this.attachPoint,
                            value: "",
                            placeHolder: 'type your ' + label
                        });
                    }

                    if (type == 'dropdown') {
                        // implement for drop down
                    }


                    var labelControl = query('.label', this.domNode)[0];
                    var domControl = query('.control', this.domNode)[0];
                    labelControl.innerHTML = label;
                    domConstruct.place(control.domNode, domControl, "last");

                    control.startup();
                }
            }
        }

        return dojo_declare([_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], proto);
});
