    var mylist = [];
var inputs = $('tr input[type=hidden]');
var tds
for (var i = 0; i < inputs.length; i++) { mylist.push({ id: $(inputs[i]).val(), text: $($($(inputs[i]).parent()).children()[0]).text()  }); };
    function AddToList() {
        var obj = $('.equipment-form option:selected');
        var id = obj.val();
        var text = obj.text();
        mylist.push({ id: id, text: text });
        $('#equipment-form')[0].selectedIndex = 0
        DisableButton($('.equipment-form'));
        populateEquipments();
    }

    function populateEquipments() {
        var html_list = "";
    for (var i = 0; i < mylist.length; i++) {
        html_list += "<tr><td>" + mylist[i].text + "</td><td><input type=\"button\" name=\"delete-button\" class=\"btn btn-danger\" value=\"Return\" onclick=\"ReturnFromList(this)\"/></td><input type=\"hidden\" name=\"EquipmentID[" + i + "]\" value=\"" + mylist[i].id + "\"></tr>";
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

function ReturnFromList(obj) {
    var parent = $(obj).parent();
    var sibling2 = parent.siblings()[1];
    var equipmentId = $(sibling2).val();
    var userId = $('#UserId').val();
    console.log('EquipmentID:' + equipmentId);
    console.log('UserID:' + userId);
    $.ajax({
        type: "POST",
        url: "/EquipmentAssignment/Return",
        data: JSON.stringify({ UserId: userId, EquipmentId: equipmentId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Returned Equipment Successfully.',
                showConfirmButton: false,
                timer: 1000
            }).then((result) => {
                window.location.href = '/EquipmentAssignment/Index/' + userId;
            });
        },
        failure: function (response) {
            Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: 'Returned Equipment Failed.',
                showConfirmButton: false,
                timer: 1000
            }).then((result) => {
                window.location.href = '/EquipmentAssignment/Index/' + userId;
            });
        },
        error: function (response) {
            Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: 'Error: Returned Equipment Failed.',
                showConfirmButton: false,
                timer: 1000
            }).then((result) => {
                window.location.href = '/EquipmentAssignment/Index/' + userId;
            });
        }
    });
}

function DisableButton(obj) {
    if ($(obj)[0].selectedIndex == 0) {
        $('.add-button').addClass("disabled");
        $('.add-button').attr("title", "Select Equipment First");
        $('.add-button').attr("disabled","");
    }
    else {
        $('.add-button').removeClass("disabled");
        $('.add-button').removeAttr("title");
        $('.add-button').removeAttr("disabled");
    }
}