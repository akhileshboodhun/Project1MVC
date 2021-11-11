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
    'dojox/mvc/Repeat',
    'dojo/text!StaticViews/Tiles.html',
    "dojox/grid/DataGrid",
    'dojox/mvc/at',
    'dijit/form/Button',
    "dijit/form/TextBox"
],
    function (dojo_declare, lang, _Widget, _TemplatedMixin, _WidgetsInTemplateMixin, ItemFileWriteStore, request, query, array, domConstruct, dom, on, Repeat, template) {

        var proto = {

            templateString: template,
            data: [1, 2, 3, 4, 5, 6, 7, 8, 9],
            grid: null,
            btnChangeColor: null,
            countClicked: 1,

            constructor: function () {

            },

            postCreate: function () {
                this.inherited(arguments);
                console.log(this.data);
            },

            startup: function () {
                this.inherited(arguments);
            },

            _onClick: function (e) {
                console.log('you clicked me: ' + this.countClicked++);
            },

            _getRequestData: function () {
            },

            _processJsonData: function (data) {

                var store = new ItemFileWriteStore();
                this.set('store', store);
            },

            _requestDataLayout: function () {
                var layout = [[
                ]];

                this.set('layout', layout);
            },

            _buildData: function () {
                var store = new ItemFileWriteStore();

                this.set('store', store);
            },

            _buildLayout: function () {
                var layout = [[
                ]];

                this.set('layout', layout);
            }
        }

        return dojo_declare([_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], proto);
    });
