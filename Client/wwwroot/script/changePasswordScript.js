var token = JSON.parse(localStorage.getItem("loginSession") || '{}')?.token || null;

$(document).ready(function () {
    $('#exampleCheck2').change(function () {
        var inputOldPassword = $('#oldPassword');
        var inputNewPassword = $('#newPassword');
        var inputConfirmPassword = $('#confirmPassword');

        // Jika checkbox dicentang
        if ($(this).is(':checked')) {
            inputOldPassword.attr('type', 'text');
            inputNewPassword.attr('type', 'text');
            inputConfirmPassword.attr('type', 'text');
        } else {
            // Jika checkbox tidak dicentang
            inputOldPassword.attr('type', 'password');
            inputNewPassword.attr('type', 'password');
            inputConfirmPassword.attr('type', 'password');
        }
    });
    var data = localStorage.getItem("data");
    if (data) {
        var parsedData = JSON.parse(data);
        var nik = parsedData.nik;
    }
    $('#nikChangePassword').val(nik);
});

function saveChangePassword() {
    var data = {
        nik: $('#nikChangePassword').val(),
        oldPassword: $('#oldPassword').val(),
        newPassword: $('#newPassword').val(),
    };
    console.log(data)
    $.ajax({
        type: "PUT",
        url: "https://localhost:7245/api/Profile/changePassword",
        data: JSON.stringify(data),
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
                title: "Success change password."
            });
            resetForm();
        },
        error: function (xhr) {
            const errorMessage = xhr.responseJSON.message;
            if (errorMessage.includes('password')) {
                $('#oldPassword').addClass('is-invalid');
                $('#feedbackOldPassword').text(errorMessage);
            }
        }
    });
}
function resetForm() {
    $('#oldPassword, #newPassword, #confirmPassword,').val('');
    $('#oldPassword, #newPassword, #confirmPassword, ').removeClass('is-invalid');
}


$(function () {
    $.validator.setDefaults({
        submitHandler: function () {
            saveChangePassword()
        }
    });
    $('#formChangePassword').validate({
        rules: {
            oldPassword: {
                required: true,
            },
            newPassword: {
                required: true,
            },
            confirmPassword: {
                required: true,
                equalTo: "#newPassword" 
            }
        },
        messages: {
            oldPassword: {
                required: "Please enter your old password",
            },
            newPassword: {
                required: "Please enter your new password",
            },
            confirmPassword: {
                required: "Please confirm your new password",
                equalTo: "Passwords do not match with new password"
            }
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.siblings('.invalid-feedback').append(error); // Menggunakan siblings untuk menempatkan pesan di bawah input
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
});
