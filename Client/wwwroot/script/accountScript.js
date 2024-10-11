$(document).ready(function () {
    var loginSession = localStorage.getItem("loginSession");

    if (loginSession) {
        Swal.fire({
            title: "you already have a login session",
            icon: "error",
            confirmButtonText: "OK"
        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = document.referrer || 'Dashboard';
            }
        });
    }
});

async function login() {
    $('#spinnerOverlay').removeClass('d-none');
    var login = {
        nikOrEmail: $('#inputNikOrEmail').val(),
        password: $('#inputPassword').val()
    };

    try {
        const result = await $.ajax({
            type: "POST",
            url: "https://localhost:7245/api/Account/login",
            data: JSON.stringify(login),
            contentType: "application/json; charset=utf-8",
        });

        $('#spinnerOverlay').addClass('d-none');
        localStorage.setItem('loginSession', JSON.stringify(result.data));
        localStorage.setItem('toastMessage', 'Login berhasil! Selamat datang!');

        var data = result.data.token; 
        const decodedToken = jwt_decode(data);
        var roles = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        var userData = {
            nik: decodedToken.nik,
            fullname: decodedToken.fullname,
            roles: Array.isArray(roles) ? roles : [roles]
        };

        localStorage.setItem('data', JSON.stringify(userData));

        // Tunggu hingga permintaan untuk mengatur sesi selesai
        await $.ajax({
            type: "POST",
            url: "/SetSession/Login", 
            data: JSON.stringify(userData),
            contentType: "application/json; charset=utf-8",
        });

        var urlRedirect = localStorage.getItem("redirectUrl");
        if (urlRedirect) {
            localStorage.removeItem("redirectUrl");
            window.location.href = urlRedirect;
        } else {
            if (roles === "Employee") {
                window.location.href = '/Profile';
            } else {
                window.location.href = '/Dashboard';
            }
        }
    } catch (xhr) {
        $('#spinnerOverlay').addClass('d-none');
        const errorMessage = xhr.responseJSON?.message || "An error occurred";
        console.log(xhr);
        Swal.fire({
            title: errorMessage,
            icon: "error"
        });
    }
}


$(function () {
    $.validator.setDefaults({
        submitHandler: function () {
           login()
        }
    });
    $('#loginForm').validate({
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
            $('#nikOrEmailFeedback').text('NIK or Email is required!');
            $('#passwordFeedback').text('Password is required!');
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
});

$('#togglePassword').on('click', function () {
    const passwordInput = $('#inputPassword');
    const toggleIcon = $('#toggleIcon');

    if (passwordInput.attr('type') === 'password') {
        passwordInput.attr('type', 'text');
        toggleIcon.removeClass('fa-eye-slash').addClass('fa-eye');
    } else {
        passwordInput.attr('type', 'password');
        toggleIcon.removeClass('fa-eye').addClass('fa-eye-slash');
    }
});



