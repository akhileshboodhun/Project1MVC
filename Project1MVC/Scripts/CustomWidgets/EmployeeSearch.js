define(
    ['dojo/_base/declare',
        'dojo/_base/lang',
        'dijit/_Widget',
        'dijit/_TemplatedMixin',
        'dijit/_WidgetsInTemplateMixin',
        'dojo/request',
        'dojo/query',
        "dojo/_base/array",
        'dojo/data/ItemFileWriteStore',
        'dojo/text!StaticViews/EmployeeSearch.html',
        "CustomWidgets/DataGridFilterWidget"
    ],
    function (dojo_declare, lang, _Widget, _TemplatedMixin, _WidgetsInTemplateMixin, request, query, arrayUtil, ItemFileWriteStore, template) {

        var proto = {

            templateString: template,

            filters: [
                { type: 'textbox', label: 'DisplayName', column: 'DisplayName' },
                { type: 'textbox', label: 'XRefCode', column: 'XRefCode' },
                { type: 'textbox', label: 'StateCode', column: 'StateCode' },
                { type: 'textbox', label: 'Gender', column: 'Gender' }            ],

            store: null, //grid store
            gridLayout: null, // grid layout

            postCreate: function () {
                this.inherited(arguments);
                //this._getRequestData();
            },

            startup: function () {
                this.inherited(arguments);
            },

            _getRequestData: function () {
                //var url = 'https://ghibliapi.herokuapp.com/people';
                var url = 'Home/GetEmployees';
                var me = this;
                request.get(url, {
                    handleAs: "json",
                    //jsonp: "callback",
                    //query: {
                    //    employeeId: 1001
                    //}
                }).then(function (data) {
                    me._processJsonData(data);
                    me._requestDataLayout();
                });
            },

            _processJsonData: function (data) {
                var result = {
                    identifier: "id",
                    items: []
                };

                var rows = data.length;

                for (var i = 0, l = data.length; i < rows; i++) {
                    var emp = {
                        id: i + 1,
                        EmployeeId: data[i].EmployeeId,
                        DisplayName: data[i].DisplayName,
                        XRefCode: data[i].XRefCode,
                        StateCode: data[i].StateCode,
                        Gender: data[i].Gender
                    };
                    result.items.push(emp);
                }

                var store = new ItemFileWriteStore({ data: result });
                this.set('store', store);
            },

            _requestDataLayout: function () {

                var layout = [[
                    { 'name': 'Employee Id', 'field': 'EmployeeId', 'width': '100px' },
                    { 'name': 'Employee Name', 'field': 'DisplayName', 'width': '200px' },
                    { 'name': 'XRefCode', 'field': 'XRefCode', 'width': '100px' },
                    { 'name': 'State', 'field': 'StateCode', 'width': '100px' },
                    { 'name': 'Gender', 'field': 'Gender', 'width': '100px' }
                ]];

                this.set('layout', layout);
            },
        };

        return dojo_declare([_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], proto);
    });