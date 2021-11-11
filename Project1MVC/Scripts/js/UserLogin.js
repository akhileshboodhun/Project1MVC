/// <amd-dependency path="dojo/_base/declare" name="dojo_declare" />
/// <amd-dependency path="dojo/_base/lang" name="lang"/>
/// <amd-dependency path="dijit/_Widget" name="_Widget"/>
/// <amd-dependency path="dijit/_TemplatedMixin" name="_TemplatedMixin"/>
/// <amd-dependency path="dijit/_WidgetsInTemplateMixin" name="_WidgetsInTemplateMixin"/>
/// <amd-dependency path="dojo/data/ItemFileWriteStore" name="ItemFileWriteStore"/>
/// <amd-dependency path="dojo/text!StaticViews/LandingPage.html" name="template"/>
/// <amd-dependency path="dijit/form/Button"/>
/// <amd-dependency path="dojox/grid/DataGrid"/>
/// <amd-dependency path="dojox/mvc/at"/>
/// <amd-dependency path='dojo/request' name='request'/>
define(["require", "exports", "dojo/_base/declare", "dojo/_base/lang", "dijit/_Widget", "dijit/_TemplatedMixin", "dijit/_WidgetsInTemplateMixin", "dojo/data/ItemFileWriteStore", "dojo/text!StaticViews/LandingPage.html", "dojo/request", "dijit/form/Button", "dojox/grid/DataGrid", "dojox/mvc/at"], function (require, exports, dojo_declare, lang, _Widget, _TemplatedMixin, _WidgetsInTemplateMixin, ItemFileWriteStore, template, request) {
    var UserLogin = /** @class */ (function () {
        function UserLogin() {
            this.templateString = template;
        }
        UserLogin.prototype.postCreate = function () {
            this.inherited(arguments);
        };
        UserLogin.prototype.startup = function () {
            this.inherited(arguments);
            //this._buildData();
            //this._buildLayout();
            this._getRequestData();
        };
        UserLogin.prototype._onClick = function () {
            console.log('you click me!!!');
        };
        UserLogin.prototype._buildData = function () {
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
        };
        UserLogin.prototype._buildLayout = function () {
            var layout = [[
                    { 'name': 'Column 1', 'field': 'id', 'width': '100px' },
                    { 'name': 'Column 2', 'field': 'col2', 'width': '100px' },
                    { 'name': 'Column 3', 'field': 'col3', 'width': '200px' },
                    { 'name': 'Column 4', 'field': 'col4', 'width': '150px' }
                ]];
            this.set('layout', layout);
        };
        UserLogin.prototype._getRequestData = function () {
            var url = 'https://ghibliapi.herokuapp.com/people';
            var me = this;
            request.get(url, {
                handleAs: "json"
            }).then(function (data) {
                me._processJsonData(data);
                me._requestDataLayout();
            });
        };
        UserLogin.prototype._processJsonData = function (data) {
            var result = {
                identifier: "id",
                items: []
            };
            for (var i = 0; i < data.length; i++) {
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
        };
        UserLogin.prototype._requestDataLayout = function () {
            var layout = [[
                    { 'name': 'User Name', 'field': 'name', 'width': '100px' },
                    { 'name': 'Gender', 'field': 'gender', 'width': '100px' },
                    { 'name': 'Age', 'field': 'age', 'width': '100px' },
                    { 'name': 'Hair Color', 'field': 'hair_color', 'width': '100px' },
                    { 'name': 'Eye Color', 'field': 'eye_color', 'width': '100px' }
                ]];
            this.set('layout', layout);
        };
        return UserLogin;
    }());
    return dojo_declare("", [_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], UserLogin.prototype);
});
