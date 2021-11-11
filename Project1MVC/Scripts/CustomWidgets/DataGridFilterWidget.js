define(
    ['dojo/_base/declare',
        'dojo/_base/lang',
        'dijit/_Widget',
        'dijit/_TemplatedMixin',
        'dijit/_WidgetsInTemplateMixin',
        'dojo/request',
        'dojo/data/ItemFileWriteStore',
        "dojo/store/Memory",
        'dijit/form/Button',
        "dojo/dom-construct",
        "dojo/on",
        "dijit/ConfirmDialog",
        "dojox/widget/Toaster",
        'dojo/text!StaticViews/DataGridFilterWidget.html',
        "dojox/grid/DataGrid",
        "dijit/form/TextBox",
        "dojox/mvc/Group",
        'dojox/mvc/at',
        "CustomWidgets/FilterPane",
        "dijit/form/ComboBox",
        'dijit/form/Button'],
    function (dojo_declare, lang, _Widget, _TemplatedMixin, _WidgetsInTemplateMixin, request, ItemFileWriteStore, Memory, Button, domConstruct, on, ConfirmDialog, Toaster, template) {

        var proto = {

            templateString: template,
            equipmentStore: new ItemFileWriteStore({data:[]}),
            categoryStore: new ItemFileWriteStore({ data: [] }),
            toaster: null,

            constructor: function () {
                this._createEquipmentStore();
            },

            postCreate: function () {
                this.inherited(arguments);
                this.own(this.toaster = new Toaster());
            },

            startup: function () {
                this.inherited(arguments);
                this._requestDataLayout();
            },

            _onbtnApplyFilter: function () {
                var empId = this.txtSearchEmployeeId.value;
                this._getEmployeeById(empId);
            },

            _createEquipmentCategoryStore: function () {
                var url = 'Home/GetEquipmentCategories';
                var me = this;

                request.get(url, {
                    handleAs: "json"
                }).then((data) => {
                    var store = new Memory({ idProperty: 'EquipmentCategoryId', data: data });
                    this.set('categoryStore', store);

                    this.cmbCategory.set('store', this.categoryStore);
                });
            },

            _createEquipmentStore: function () {
                var url = 'Home/GetEquipments';
                var me = this;

                request.get(url, {
                    handleAs: "json"
                }).then((data) => {
                    var store = new Memory({idProperty: 'EquipmentId', data: data });
                    this.set('equipmentStore', store);

                    this.cmbEquipment.set('store', this.equipmentStore);

                    this._createEquipmentCategoryStore();

                });
            },

            _getEmployeeById: function (_employeeId) {
                //var url = 'https://ghibliapi.herokuapp.com/people';
                var url = 'Home/GetEmployeeById';
                var me = this;
                request.get(url, {
                    handleAs: "json",
                    jsonp: "callback",
                    query: {
                        employeeId: _employeeId
                    }
                }).then(function (data) {
                    me.set('employeeData', data);
                    me._buildGridStoreData(data.EmployeeEquipments);
                    me._requestDataLayout();
                });
            },

            _buildGridStoreData: function (EmployeeEquipments) {
                var result = {
                    identifier: "id",
                    items: []
                };

                var rows = EmployeeEquipments.length;

                for (var i = 0, l = EmployeeEquipments.length; i < rows; i++) {
                    var emp = {
                        id: i + 1,
                        EmployeeEquipmentId: EmployeeEquipments[i].EmployeeEquipmentId,
                        EmployeeId: EmployeeEquipments[i].EmployeeId,
                        EquipmentId: EmployeeEquipments[i].EquipmentId
                    };
                    result.items.push(emp);
                }

                var store = new ItemFileWriteStore({ data: result });
                this.set('store', store);
            },

            _getEmployeeEquipment: function (_employeeId) {
                var url = 'Home/GetEmployeeEquipment';
                var me = this;
                request.get(url, {
                    handleAs: "json",
                    jsonp: "callback",
                    query: {
                        employeeId: _employeeId
                    }
                }).then(function (data) {
                    me.set('employeeData', data);
                });
            },

            _requestDataLayout: function () {
                var me = this;

                var layout = [[
                    {
                        'name': 'Equiment Name - Serial Number',
                        'field': 'EquipmentId', 'width': '300px',
                        formatter: function (value)
                        {
                            var item = me.equipmentStore.get(value);
                            return item.SerialNumber;

                            //return me.equipmentStore.get(value).NameSerialNumber;
                        }
                    },
                    {
                        'name': 'Action',
                        'field': 'EmployeeEquipmentId',
                        formatter: function (item) {
                            var btn = new Button({
                                label: "Delete"
                            });

                            on(btn, "click", function (evt) {
                                var confirmDialog = new ConfirmDialog({
                                    content: "Do you want to delete the item?",
                                    title: "Are you sure?",
                                    onExecute: function () {
                                        me._deleteEmployeeEquipment(item);
                                    }
                                });
                                confirmDialog.show();
                            });

                            return btn;
                        }
                    }
                ]];

                this.set('layout', layout);
            },

            _deleteEmployeeEquipment: function (_employeeEquipmentId) {
                var url = 'Home/DeleteEmployeeEquipment';
                var me = this;

                request.get(url, {
                    handleAs: "json",
                    jsonp: "callback",
                    query: {
                        employeeEquipmentId: _employeeEquipmentId
                    }
                }).then(function (data) {
                    if (data == 'Success') {
                        me.toaster.setContent('<div style="height: 50px; width: 150px;background-color: green; color: white">Equipment removed Successfully</div>', 'message', '1000');
                        me.toaster.show()
                        me._getEmployeeById(me.txtEmployeeId.value);
                    }
                });
            },

            _onCategoryChange: function () {
                var id = this.cmbCategory.item.EquipmentCategoryId;
                this.cmbEquipment.set('query', { EquipmentCategoryId: id });
            },

            _onAssignEquipmentToEmployee: function () {
                var url = 'Home/AssignEquipmentToEmployee';
                var me = this;
                request.get(url, {
                    handleAs: "json",
                    jsonp: "callback",
                    query: {
                        employeeId: me.txtEmployeeId.value,
                        equipmentId: me.cmbEquipment.item.EquipmentId
                    }
                }).then(function (data) {
                    if (data == 'Success') {
                        me.toaster.setContent('<div style="height: 50px; width: 150px;background-color: green; color: white">Saved Successfully</div>', 'message','1000');
                        me.toaster.show()
                        me._getEmployeeById(me.txtEmployeeId.value);
                    }
                });
            }
        }

        return dojo_declare([_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], proto);
    });
