$(document).ready(function () {
    var data = localStorage.getItem("data");
    if (data) {
        var parsedData = JSON.parse(data);
        var fullname = parsedData.fullname;
        var roles = parsedData.roles

        document.getElementById("userFullName").textContent = fullname;
       
    }
});

function logout() {
    Swal.fire({
        title: "Are you sure you want to log out?",
        text: "You will be logged out of your account.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, log out!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "GET", 
                url: "/SetSession/DeleteSession", 
                success: function (result) {
                    localStorage.removeItem("loginSession");
                    localStorage.removeItem("data");
                    localStorage.removeItem("toastMessage");
                    window.location.href = '/';
                },
                error: function (xhr) {
                    console.log(xhr);
                }
            });
            
        }
    });
}

function checkLogin() {
    var loginSession = localStorage.getItem("loginSession")
    var chek = localStorage.getItem("redirectUrl")

    if (!loginSession) {
        localStorage.setItem("redirectUrl", window.location.href);
        Swal.fire({
            title: "Please login to access this page",
            icon: "error",
            width: 600,
            padding: "3em",
            backdrop: `
               rgb(32, 33, 36)
            `,
            confirmButtonText: "OK"
        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = 'Account';
                return;
            }
        });
    }
    var toastMessage = localStorage.getItem('toastMessage');

    if (toastMessage) {
        const Toast = Swal.mixin({
            toast: true,
            position: "top-end",
            showConfirmButton: false,
            timer: 2000,
            timerProgressBar: true,
            didOpen: (toast) => {
                toast.onmouseenter = Swal.stopTimer;
                toast.onmouseleave = Swal.resumeTimer;
            }
        });
        Toast.fire({
            icon: "success",
            title: "Signed in successfully"
        });

        localStorage.removeItem('toastMessage');
    }
}

function checkRole() {
    var data = localStorage.getItem("data");
    var parsedData = JSON.parse(data);
    if (parsedData) {
        var roles = parsedData.roles
    }
    if (roles == "Employee") {
        Swal.fire({
            title: "buan admin",
            icon: "error",
            width: 600,
            padding: "3em",
            backdrop: `
               rgb(32, 33, 36)
            `,
            confirmButtonText: "OK"
        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = '/Profile';
            }
        });
    }
}