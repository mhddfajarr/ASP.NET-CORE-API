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