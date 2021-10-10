    var mylist = [];
    var inputs = $('input[type=hidden]');
    for (var i = 0; i < inputs.length; i++) {mylist.push($(input[i]).val()); };
    function AddToList() {
        var obj = $('.equipment-form option:selected');
    var id = obj.val();
    var text = obj.text();
    mylist.push({id: id, text: text });
    populateEquipments();
    }

    function populateEquipments() {
        var html_list = "";
    for (var i = 0; i < mylist.length; i++) {
        html_list += "<tr><td>" + mylist[i].text + "</td><td><input type=\"button\" name=\"delete-button\" class=\"btn btn-danger\" value=\"Delete\" onclick=\"DeleteFromList(this)\"/></td><input type=\"hidden\" name=\"EquipmentID[" + i + "]\" value=\"" + mylist[i].id + "\"></tr>";
        }
    $('#equipments-assigned').html(html_list);
    }

    function DeleteFromList(obj) {
        var parent = $(obj).parent();
    var sibling2 = parent.siblings()[1];
    var equipmentId = $(sibling2).val();
    console.log('EquipmentID:' + equipmentId);
        mylist = mylist.filter(el => el.id != equipmentId);
    populateEquipments();
    }