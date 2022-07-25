
function clearFields() {
    $('#id').val("");
    $('#txtName').val("");
    $('#txtifscCode').val("");
    $("#chkIsActive").prop("checked",true);
    $("#bankModal").modal('show');
}



function DeleteBank(rowContext) {
    var data = table.row($(rowContext).parents("tr")).data();
    var deleteid = data["id"];
    $('#deleteid').val(data["id"]);
    $("#deleteModal").modal('show');
}


function DeleteBankDetails(url, deleteId) {
    var url = url;
    $.post(url, { ID: deleteId }, function (data) {
        if (data) {
            oTable = $('#bankDatatable').DataTable();
            oTable.draw();
        } else {
            alert("Something Went Wrong!");
        }
        $("#deleteModal").modal('hide');
    });
}

function edit(rowContext) {
    debugger;
    clearFields();
    if (table) {
        var data = table.row($(rowContext).parents("tr")).data();

        $('#id').val(data.id);
        $('#txtName').val(data.name);
        $('#txtifscCode').val(data.ifscCode);
        $("#chkIsActive").prop("checked", data.isActive);
        $("#bankModal").modal('show');
    }
}