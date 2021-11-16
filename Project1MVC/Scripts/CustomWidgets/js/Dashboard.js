/// <amd-dependency path="dojo/_base/declare" name="dojo_declare" />
/// <amd-dependency path="dojo/_base/lang" name="lang"/>
/// <amd-dependency path="dijit/_Widget" name="_Widget"/>
/// <amd-dependency path="dijit/_TemplatedMixin" name="_TemplatedMixin"/>
/// <amd-dependency path="dijit/_WidgetsInTemplateMixin" name="_WidgetsInTemplateMixin"/>
/// <amd-dependency path="dojo/request" name="request"/>
/// <amd-dependency path="dojox/mvc/getStateful" name="getStateful"/>
/// <amd-dependency path="dojo/data/ItemFileWriteStore" name="ItemFileWriteStore"/>
/// <amd-dependency path="dojo/text!StaticViews/Dashboard.html" name="template"/>
/// <amd-dependency path="dojox/mvc/WidgetList"/>
/// <amd-dependency path="dojox/mvc/at"/>
/// <amd-dependency path="dojox/mvc/Group"/>
/// <amd-dependency path="js/Tiles"/>
define(["require", "exports", "dojo/_base/declare", "dojo/_base/lang", "dijit/_Widget", "dijit/_TemplatedMixin", "dijit/_WidgetsInTemplateMixin", "dojo/request", "dojox/mvc/getStateful", "dojo/data/ItemFileWriteStore", "dojo/text!StaticViews/Dashboard.html", "./DashboardControl", "dojo/dom-construct", "dojo/dom", "dojox/mvc/WidgetList", "dojox/mvc/at", "dojox/mvc/Group", "js/Tiles"], function (require, exports, dojo_declare, lang, _Widget, _TemplatedMixin, _WidgetsInTemplateMixin, request, getStateful, ItemFileWriteStore, template, DashboardControl, domConstruct, dom) {
    var Dashboard = /** @class */ (function () {
        function Dashboard() {
            this.templateString = template;
        }
        Dashboard.prototype.startup = function () {
            this.inherited(arguments);
        };
        Dashboard.prototype.buildRendering = function () {
            var _this = this;
            this.inherited(arguments);
            this._getRequestData().then(function (data) {
                _this._data = {
                    identifier: "label",
                    items: data
                };
                _this.searchRecords = getStateful(_this._data);
                var domNode = dom.byId("dashboardNode");
                var dashboardCtrl = new DashboardControl({
                    searchRecords: _this.searchRecords
                });
                domConstruct.place(dashboardCtrl.domNode, domNode, "last");
                dashboardCtrl.startup();
            });
        };
        Dashboard.prototype.postCreate = function () {
            this.inherited(arguments);
        };
        Dashboard.prototype._processJsonData = function (data) {
            var rows = data.length;
            var items = [];
            for (var i = 0, l = data.length; i < rows; i++) {
                var _tile = {
                    label: data[i].label,
                    icon: data[i].icon,
                    desc: data[i].desc,
                    location: data[i].location
                };
                items.push(_tile);
            }
            return items;
        };
        Dashboard.prototype._getRequestData = function () {
            var url = 'https://localhost:44362/Dashboard/Tiles';
            var processedData;
            var me = this;
            return request.get(url, {
                handleAs: "json"
            }).then(function (data) {
                processedData = me._processJsonData(data);
                console.log(processedData);
                return processedData;
            });
        };
        return Dashboard;
    }());
    return dojo_declare("", [_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], Dashboard.prototype);
});
