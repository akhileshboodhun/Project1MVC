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
    'dojo/text!StaticViews/LandingPage.html',
    "dojox/grid/DataGrid",
    'dojox/mvc/at',
    'dijit/form/Button',
    "dijit/form/TextBox"
],
    function (dojo_declare, lang, _Widget, _TemplatedMixin, _WidgetsInTemplateMixin, ItemFileWriteStore, request, query, array, domConstruct,dom, on, template) {

        var proto = {

            templateString: template,
            grid: null,
            btnChangeColor: null,
            countClicked: 1,

            constructor: function () {

            },

            postCreate: function () {
                this.inherited(arguments);

                on(dom.byId('btnChangeColor'), "click", function () {
                    var bgColor = query('#bgColor');
                    if (bgColor) {
                        var randomColor = '#' + Math.floor(Math.random() * 16777215).toString(16);
                        bgColor[0].style.background = randomColor;
                    }
                });
            },

            startup: function () {
                this.inherited(arguments);
                this._getRequestData();
            },

            _onClick: function (e) {
                console.log('you clicked me: ' + this.countClicked++);
            },

            _getRequestData: function () {
                var url = 'https://ghibliapi.herokuapp.com/people';
                //var url = 'Home/GetEmployees';
                var me = this;
                request.get(url, {
                    handleAs: "json"
                }).then(function (data) {
                    debugger;

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
                        name: data[i].name,
                        age: data[i].age,
                        eye_color: data[i].eye_color,
                        hair_color: data[i].hair_color,
                        gender: data[i].gender
                    };
                    result.items.push(emp);
                }

                var store = new ItemFileWriteStore({ data: result });
                this.set('store', store);
            },

            _requestDataLayout: function () {
                var layout = [[
                    {'name': 'User Name', 'field': 'name', 'width': '100px'},
                    {'name': 'Gender', 'field': 'gender', 'width': '100px'},
                    {'name': 'Age', 'field': 'age', 'width': '100px'},
                    { 'name': 'Hair Color', 'field': 'hair_color', 'width': '100px' },
                    { 'name': 'Eye Color', 'field': 'eye_color', 'width': '100px' }
                ]];

                this.set('layout', layout);
            },

            _buildData: function () {
                /*set up data store*/
                var data = {
                    identifier: "id",
                    items: []
                };
                var data_list = [
                    { col1: "normal", col2: false, col3: 'But are not followed by two hexadecimal', col4: 29.91 },
                    { col1: "important", col2: false, col3: 'Because a % sign always indicates', col4: 9.33 },
                    { col1: "important", col2: false, col3: 'Signs can be selectively', col4: 19.34 }
                ];
                var rows = 60;
                for (var i = 0, l = data_list.length; i < rows; i++) {
                    data.items.push(lang.mixin({ id: i + 1 }, data_list[i % l]));
                }
                var store = new ItemFileWriteStore({ data: data });

                this.set('store', store);
            },

            _buildLayout: function () {
                var layout = [[
                    { 'name': 'Column 1', 'field': 'id', 'width': '100px' },
                    { 'name': 'Column 2', 'field': 'col2', 'width': '100px' },
                    { 'name': 'Column 3', 'field': 'col3', 'width': '200px' },
                    { 'name': 'Column 4', 'field': 'col4', 'width': '150px' }
                ]];

                this.set('layout', layout);
            }
        }

        return dojo_declare([_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], proto);
});
