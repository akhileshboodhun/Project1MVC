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

/// <amd-dependency path="./DashboardControl" name="DashboardControl" />
/// <amd-dependency path="dojo/dom-construct" name="domConstruct"/>
/// <amd-dependency path="dojo/dom" name="dom"/>

import { AsyncKeyword } from "../../node_modules/typescript/lib/typescript";
import { ITile } from "./ITile";

declare let dojo_declare;
declare let lang;
declare let _Widget;
declare let _TemplatedMixin;
declare let _WidgetsInTemplateMixin;
declare let request;
declare let getStateful;
declare let ItemFileWriteStore;
declare let template;
declare let DashboardControl;
declare let domConstruct;
declare let dom;

class Dashboard {
    private inherited;
    private set: any;
    private _data: any;
    private _items: ITile[];

    public templateString = template;
    public searchRecords: any;

    startup() {
        this.inherited(arguments);
    }

    buildRendering() {
        this.inherited(arguments);

        this._getRequestData().then((data) => {

            this._data = {
                identifier: "label",
                items: data
            };

            this.searchRecords = getStateful(this._data);

            var domNode = dom.byId("dashboardNode");

            var dashboardCtrl = new DashboardControl({
                searchRecords : this.searchRecords
            });
            domConstruct.place(dashboardCtrl.domNode, domNode, "last");
            dashboardCtrl.startup();

        });
    }

    postCreate() {
        this.inherited(arguments);
    }

    _processJsonData(data: any) {
        var rows = data.length;
        var items = [];

        for (var i = 0, l = data.length; i < rows; i++) {
            var _tile: ITile = {
                label: data[i].label,
                icon: data[i].icon,
                desc: data[i].desc,
                location: data[i].location
            };
            items.push(_tile);
        }
        return items;
    }

    _getRequestData(): Promise<any> {
        var url = 'https://localhost:44362/Dashboard/Tiles';
        var processedData: any;
        let me = this;
        return request.get(url, {
            handleAs: "json"
        }).then(function (data: any) {
            processedData = me._processJsonData(data);
            console.log(processedData);
            return processedData;
        });

    }
}

export = dojo_declare("", [_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], Dashboard.prototype);