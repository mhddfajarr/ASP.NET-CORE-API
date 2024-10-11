var token = JSON.parse(localStorage.getItem("loginSession") || '{}')?.token || null;
$(document).ready(function () {
    //checkLogin()
    //checkRole();

    $("#tableRole").DataTable({
        "paging": true,
        "responsive": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "ajax": {
            url: "https://localhost:7245/api/Roles",
            type: "GET",
            "dataType": "json",
            "dataSrc": "data",
            headers: {
                Authorization: 'Bearer ' + token
            },
        },
        "columnDefs": [{
            "defaultContent": "-",
            "targets": "_all"
        }],
        "columns": [
            {
                "render": function (data, type, row, meta) {
                    return meta.row + 1;
                },
            },
            { "data": "roleId" },
            { "data": "roleName" },
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning btn-sm mr-2" data-toggle="tooltip" data-placement="top" title="Edit Data" onClick="showModalEdit(\'' + row.roleId + '\'); return false;"><i class="fas fa-solid fa-pen"></i></button>' +
                        '<button  class="btn btn-danger btn-sm" data-toggle="tooltip" data-placement="top" title="Delete Data" onClick="deleteRole(\'' + row.roleId + '\'); return false;"><i class="fas fa-solid fa-trash"></i></button>';
                }
            }
        ],
    });
    $('#modalRole').on('hidden.bs.modal', function () {
        resetForm();
    });
});

function resetForm() {
    $('#inputRoleName').val('');
    $('#roleId').val('');
    $('#inputRoleName').removeClass('is-invalid');
}

function add() {
    var role = {
        RoleName: $('#inputRoleName').val()
    };

    $.ajax({
        type: "POST",
        url: "https://localhost:7245/api/Roles",
        headers: {
            Authorization: 'Bearer ' + token
        },
        data: JSON.stringify(role),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            const Toast = Swal.mixin({
                toast: true,
                position: "top-end",
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.onmouseenter = Swal.stopTimer;
                    toast.onmouseleave = Swal.resumeTimer;
                }
            });
            Toast.fire({
                icon: "success",
                title: "Success add new role."
            });
            $('#modalRole').modal('hide');
            resetForm();
            $('#tableRole').DataTable().ajax.reload();
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function showModalEdit(roleId) {
    $.ajax({
        type: "GET",
        url: "https://localhost:7245/api/Roles/" + roleId,
        headers: {
            Authorization: 'Bearer ' + token
        },
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var obj = result.data;
            $('#roleId').val(obj.roleId);
            $('#inputRoleName').val(obj.roleName);
            $('#modalRole').modal('show');
            $('#titleModalRole').text('Edit role');
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function deleteRole(roleId) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: "https://localhost:7245/api/Roles/" + roleId,
                dataType: "json",
                headers: {
                    Authorization: 'Bearer ' + token
                },
                success: function (result) {
                    const Toast = Swal.mixin({
                        toast: true,
                        position: "top-end",
                        showConfirmButton: false,
                        timer: 3000,
                        timerProgressBar: true,
                        didOpen: (toast) => {
                            toast.onmouseenter = Swal.stopTimer;
                            toast.onmouseleave = Swal.resumeTimer;
                        }
                    });
                    Toast.fire({
                        icon: "success",
                        title: "Success deleted role."
                    });
                    $('#modalRole').modal('hide');
                    $('#tableRole').DataTable().ajax.reload();
                },
                error: function (xhr) {
                    console.log(xhr);
                }
            });
        }
    });
}
function saveEdit() {
    var role = {
        roleId: $('#roleId').val(),
        roleName: $('#inputRoleName').val()
    };
    console.log(role.roleId)
    $.ajax({
        type: "PUT",
        url: "https://localhost:7245/api/Roles/" + role.roleId,
        data: JSON.stringify(role),
        headers: {
            Authorization: 'Bearer ' + token
        },
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            const Toast = Swal.mixin({
                toast: true,
                position: "top-end",
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.onmouseenter = Swal.stopTimer;
                    toast.onmouseleave = Swal.resumeTimer;
                }
            });
            Toast.fire({
                icon: "success",
                title: "Success edit data role."
            });
            resetForm();
            $('#modalRole').modal('hide');
            $('#tableRole').DataTable().ajax.reload();
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

$(function () {
    $.validator.setDefaults({
        submitHandler: function () {
            if ($('#roleId').val() === '') {
                add();
            } else {
                saveEdit();
            }
        }
    });
    $('#roleForm').validate({
        rules: {
            roleName: {
                required: true,
            }
        },
        messages: {
            roleName: {
                required: "Please enter a role name",
            }
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
});