$(document).ready(function () {
    var loginSession = localStorage.getItem("loginSession");

    if (loginSession) {
        Swal.fire({
            title: "you already have a login session",
            icon: "error",
            confirmButtonText: "OK"
        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = '/dashboard';
            }
        });
    }
});