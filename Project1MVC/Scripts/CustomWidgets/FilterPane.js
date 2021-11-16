define(['dojo/_base/declare',
    'dojo/_base/lang',
    "dijit/registry",
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
    "CustomWidgets/FilterControlWidget",
    'dojox/mvc/at',
    'dojo/text!StaticViews/FilterPane.html'
],
    function (dojo_declare, lang, registry,_Widget, _TemplatedMixin, _WidgetsInTemplateMixin, ItemFileWriteStore, request, query, array, domConstruct, dom, on, TextBox, FilterControlWidget, at, template) {

        var proto = {
            templateString: template,

            filtersPaneControls: [],
            customFilters: [],
            gridToFilter: null,

            postCreate: function () {
                this.inherited(arguments);
            },

            startup: function () {
                this.inherited(arguments);
                this._addfilters();
            },

            _addfilters: function () {
                if (this.filters.length > 0) {
                    this.filters.forEach(filter => {
                        if (filter.type == 'textbox') {

                            var filterControlWidget = new FilterControlWidget({
                                name: filter.label,
                                value: "",
                                placeHolder: "type the " + filter.label,
                                type: filter.type
                            });

                            var filterContainer = dom.byId('containerNode');
                            domConstruct.place(filterControlWidget.domNode, filterContainer, "last");
                            filterControlWidget.startup();

                            this.filtersPaneControls.push({ attachPoint: filterControlWidget.attachPoint, name: filter.label, type: filter.type, columnToFilter: filter.label});
                        }
                    });
                }
            },

            _getWidgetValues: function () {

                this.filtersPaneControls.forEach(control => {
                    var widget = registry.byId(control.attachPoint);

                    if (control.type == 'textbox') {
                        if (widget) {
                            if (widget.value != '')
                                this.customFilters.push({ columnToFilter: control.columnToFilter, value: widget.value });
                        }
                    }
                });
            },

            _onbtnApplyFilter: function () {
                this.customFilters = []; //clear filters each time.
                this._getWidgetValues();

                var queryOptions = {};
                this.customFilters.forEach(filter => {
                    queryOptions[filter.columnToFilter] = filter.value;
                });

                this.gridToFilter.setQuery(queryOptions);
            }
        }

        return dojo_declare([_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], proto);
    });
