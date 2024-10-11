var token = JSON.parse(localStorage.getItem("loginSession") || '{}')?.token || null;
$(document).ready(function () {
    //checkLogin();
    //checkRole();
    const degreeMapping = {
        0: 'D3',
        1: 'D4',
        2: 'S1',
        3: 'S2',
        4: 'S3',
    };
    $("#tableAccount").DataTable({
        "paging": true,
        "responsive": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "ajax": {
            url: "https://localhost:7245/api/Account/empData",
            type: "GET",
            "dataType": "json",
            "dataSrc": "data",
            headers: {
                Authorization: 'Bearer ' + token
            },
            //success: function (result) {
            //    console.log(result)
            //}
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
            { "data": "nik" },
            {"data": "fullName"
},
            {
                "data": "phone",
                "render": function (data, type, row) {
                    return data ? data : "-";
                }
},
            { "data": "email" },
            {
                "render": function (data, type, row) {
                    return row.birthDate && moment(row.birthDate, moment.ISO_8601, true).isValid()
                        ? moment(row.birthDate).format('LL')
                        : "-";
                }
            },
            { "data": "universityName" },
            {
                render: function (data, type, row) {
                    return degreeMapping[row.degree] || "N/A"; // Map degree using degreeMapping
                }
            },
            { "data": "gpa" },
        ],
    });
    $('#modalRegister').on('hidden.bs.modal', function () {
        resetForm();
    });
});
$('[data-tooltip="tooltip"]').tooltip({
    trigger: "hover",
});
$(document).ajaxComplete(function () {
    $('[data-tooltip="tooltip"]').tooltip({
        trigger: "hover",
    });
});

$(document).ready(function () {
    $.ajax({
        url: "https://localhost:7245/api/University",
        type: "GET",
        dataType: "json",
        headers: {
            Authorization: 'Bearer ' + token
        },
        success: function (result) {
            $('#university').empty();
            $('#university').append('<option value="">Select a University</option>');
            $.each(result.data, function (index, university) {
                $('#university').append('<option value="' + university.id + '">' + university.name + '</option>');
            });
        },
        error: function (xhr, status, error) {
            console.error("Error fetching data: ", status, error);
        }
    });
});

function add() {
    let dataBirthDate = $('#inputBirthDate').val() === '' ? null : $('#inputBirthDate').val();
    var account = {
        firstName: $('#inputFirstName').val(),
        lastName: $('#inputLastName').val(),
        phone: $('#inputPhone').val(),
        email: $('#inputEmail').val(),
        birthDate: dataBirthDate,
        university_Id: $('#university').val(),
        gpa: $('#inputGpa').val(),
        degree: parseInt($('#degree').val())
    };
    $.ajax({
        type: "POST",
        url: "https://localhost:7245/api/Account/register",
        data: JSON.stringify(account),
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
                title: "Success add new account."
            });
            $('#modalRegister').modal('hide');
            resetForm();
            $('#tableAccount').DataTable().ajax.reload();
        },
        error: function (xhr) {
            console.log($('#inputBirthDate').val())
            const errorMessage = xhr.responseJSON.message;
            if (errorMessage.includes('Email')) {
                $('#inputEmail').addClass('is-invalid');
                $('#emailFeedback').text(errorMessage);
            }
        }
    });
}



function resetForm() {
    $('#inputFirstName, #inputLastName, #inputPhone, #inputEmail, #university, #inputGpa, #degree, #inputBirthDate, #inputPhone').val('');
    $('#inputFirstName, #inputLastName, #inputPhone, #inputEmail, #university, #inputGpa, #degree').removeClass('is-invalid');
}

$(function () {

    $.validator.setDefaults({
        submitHandler: function () {
            add()
        }
    });
    $('#registerForm').validate({
        rules: {
            gpa: {
                required: true,
                number: true,
                min: 0,
                max: 4,
                pattern: /^\d+(\.\d{1,2})?$/
            },
            phone: {
                digits: true,  // Ensures the input consists of only digits
                maxlength: 15   // Maximum length for the phone number
            }
        },
        messages: {
            gpa: {
                required: "Please enter your GPA",
                number: "Please enter a valid number (e.g., 3.50)",
                min: "GPA must be at least 0",
                max: "GPA must not exceed 4",
                pattern: "Please enter a GPA in the format X.XX (e.g., 3.90)"
            },
            phone: {
                digits: "Please enter a valid phone number (digits only)",
                maxlength: "Phone number must not exceed 15 digits"
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