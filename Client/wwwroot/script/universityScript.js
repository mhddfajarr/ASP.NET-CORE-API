$(document).ready(function () {
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
                    return '<button class="btn btn-primary btn-sm mr-2" onClick="showModalEdit(\'' + row.id + '\'); return false;"><i class="fas fa-solid fa-pen"></i></button>' +
                        '<button  class="btn btn-danger btn-sm" onClick="deleteUniversity(\'' + row.id +  '\'); return false;"><i class="fas fa-solid fa-trash"></i></button>';
                }
            }
        ],
        
    });
    document.getElementById("universityForm").reset();
    //$('#buttonEdit').hide();
});

function add() {
    var university = new Object();
    university.name = $('#inputUnivName').val();
    $.ajax({
        type: "POST",
        url: "https://localhost:7245/api/University",
        data: JSON.stringify(university),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            Swal.fire({
                title: "Success add new data!",
                icon: "success"
            });
            $('#modalUniversity').modal('hide');
            document.getElementById("universityForm").reset();
            $('#tableUniversity').DataTable().ajax.reload();
        },
        error: function (xhr) {
            console.log(xhr)
        }
    });
}
function showModalEdit(univ_Id) {
    //$('#buttonEdit').show();
    //$('#buttonSave').hide();
    $.ajax({
        type: "GET",
        url: "https://localhost:7245/api/University/"+ univ_Id,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var obj = result.data;
            $('#univ_Id').val(obj.id);
            $('#inputUnivName').val(obj.name);
            $('#modalUniversity').modal('show');
        },
        error: function (xhr) {
            console.log(xhr)
        }
    });
}

function saveEdit() {
    var university = new Object();
    university.id = $('#univ_Id').val();
    university.name = $('#inputUnivName').val();
    $.ajax({
        type: "PUT",
        url: "https://localhost:7245/api/University/" + university.id,
        data: JSON.stringify(university),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            Swal.fire({
                title: "Success edit data!",
                icon: "success"
            });
            document.getElementById("universityForm").reset();
            $('#modalUniversity').modal('hide');
            $('#tableUniversity').DataTable().ajax.reload();
        },
        error: function (xhr) {
            console.log(xhr)
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
                success: function (result) {
                    Swal.fire({
                        title: "Deleted!",
                        text: "Your file has been deleted.",
                        icon: "success"
                    });
                    $('#modalUniversity').modal('hide');
                    $('#tableUniversity').DataTable().ajax.reload();
                },
                error: function (xhr) {
                    console.log(xhr)
                }
            });
            
        }
    });

}

$(function () {
    $.validator.setDefaults({
        submitHandler: function () {
            add();
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