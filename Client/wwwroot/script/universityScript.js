var token = JSON.parse(localStorage.getItem("loginSession") || '{}')?.token || null;
$(document).ready(function () {
    //checkLogin()
    //checkRole();
    $("#tableUniversity").DataTable({
        "paging": true,
        "responsive": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "ajax": {
            url: "https://localhost:7245/api/University",
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
            { "data": "id" },
            { "data": "name" },
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning btn-sm mr-2" data-toggle="tooltip" data-placement="top" title="Edit Data" onClick="showModalEdit(\'' + row.id + '\'); return false;"><i class="fas fa-solid fa-pen"></i></button>' +
                        '<button  class="btn btn-danger btn-sm" data-toggle="tooltip" data-placement="top" title="Delete Data" onClick="deleteUniversity(\'' + row.id + '\'); return false;"><i class="fas fa-solid fa-trash"></i></button>';
                }
            }
        ],
    });
    $('#modalUniversity').on('hidden.bs.modal', function () {
        resetForm();
    });

});
//tooltip di index
$('[data-tooltip="tooltip"]').tooltip({
    trigger: "hover",
});

//tooltip di action table
$(document).ajaxComplete(function () {
    $('[data-toggle="tooltip"]').tooltip({
        trigger: "hover",
    });
});

function resetForm() {
    $('#univ_Id').val('');
    $('#inputUnivName').val('');
    $('#univ_Id').removeClass('is-invalid');
    $('#inputUnivName').removeClass('is-invalid');
}

function add() {
    var university = {
        name: $('#inputUnivName').val()
    };

    $.ajax({
        type: "POST",
        url: "https://localhost:7245/api/University",
        headers: {
            Authorization: 'Bearer ' + token
        },
        data: JSON.stringify(university),
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
                title: "Success add new university."
            });
            $('#modalUniversity').modal('hide');
            resetForm();
            $('#tableUniversity').DataTable().ajax.reload();
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function showModalEdit(univ_Id) {
    $.ajax({
        type: "GET",
        url: "https://localhost:7245/api/University/" + univ_Id,
        headers: {
            Authorization: 'Bearer ' + token
        },
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var obj = result.data;
            $('#univ_Id').val(obj.id);
            $('#inputUnivName').val(obj.name);
            $('#modalUniversity').modal('show');
            $('#titleModal').text('Edit university');
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

$('#modalUniversity').on('hidden.bs.modal', function () {
    resetForm();
    $('#titleModal').text('Add new university');
});


function saveEdit() {
    var university = {
        id: $('#univ_Id').val(),
        name: $('#inputUnivName').val()
    };

    $.ajax({
        type: "PUT",
        url: "https://localhost:7245/api/University/" + university.id,
        data: JSON.stringify(university),
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
                title: "Success edit university."
            });
            resetForm();
            $('#modalUniversity').modal('hide');
            $('#tableUniversity').DataTable().ajax.reload();
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function deleteUniversity(univ_Id) {
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
                url: "https://localhost:7245/api/University/" + univ_Id,
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
                        title: "Success deleted university."
                    });
                    $('#modalUniversity').modal('hide');
                    $('#tableUniversity').DataTable().ajax.reload();
                },
                error: function (xhr) {
                    console.log(xhr);
                }
            });
        }
    });
}

$(function () {
    $.validator.setDefaults({
        submitHandler: function () {
            if ($('#univ_Id').val() === '') {
                add();
            } else {
                saveEdit();
            }
        }
    });
    $('#universityForm').validate({
        rules: {
            univName: {
                required: true,
            }
        },
        messages: {
            univName: {
                required: "Please enter a university name",
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


