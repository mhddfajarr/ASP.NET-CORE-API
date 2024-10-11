var token = JSON.parse(localStorage.getItem("loginSession") || '{}')?.token || null;
var data = JSON.parse(localStorage.getItem("data"));
var NIK = data.nik;


const degreeMapping = {
    0: 'D3',
    1: 'D4',
    2: 'S1',
    3: 'S2',
    4: 'S3',
};
$(document).ready(function () {
    //checkLogin()
    //$('#modalEditProfile').on('shown.bs.modal', function () {
    //    $('.select2bs4').each(function () {
    //        $(this).select2({
    //            theme: 'bootstrap4',
    //            dropdownParent: $('#modalEditProfile') // Ensure dropdown stays within modal
    //        });
    //    });
    //});
    $.ajax({
        url: "https://localhost:7245/api/Profile/" + NIK,
        type: "GET",
        headers: {
            "Authorization": `Bearer ${token}`
        },
        success: function (data) {
            const profile = data.data
            $('#nikProfile').text(profile.nik || "N/A");
            $('#fullName').text(profile.firstName + " " + profile.lastName || "N/A");
            $('#firstName').text(profile.firstName || "N/A");
            $('#lastName').text(profile.lastName || "N/A");
            $('#phone').text(profile.phone || "-");
            $('#email').text(profile.email || "N/A");


            if (profile.birthDate != null) {
                const formatBirthDate = moment(profile.birthDate).format('LL')
                $('#birthDate').text(formatBirthDate || "N/A");
            } else {
                $('#birthDate').text("-");
            }

            $('#gpa').text(profile.gpa !== null && profile.gpa !== undefined ? profile.gpa : "N/A");
            $('#degree').text(degreeMapping[profile.degree] || "N/A");

            const univID = profile.university_Id;
            if (univID) {
                $.ajax({
                    url: "https://localhost:7245/api/University/" + univID,
                    type: "GET",
                    headers: {
                        "Authorization": 'Bearer ' + token
                    },
                    success: function (univData) {
                        const universityProfile = univData.data;

                        $('#university').text(universityProfile.name || "N/A");
                    },
                    error: function (xhr, status, error) {
                        console.error("Error fetching university data:", error);
                    }
                });
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching data:", error);
        }
    });

    $.ajax({
        url: "https://localhost:7245/api/University",
        type: "GET",
        dataType: "json",
        headers: {
            Authorization: 'Bearer ' + token
        },
        success: function (result) {
            $('#inputUniversityProfile').append('<option value="" disabled>Select a University</option>');
            $.each(result.data, function (index, university) {
                $('#inputUniversityProfile').append('<option value="' + university.id + '">' + university.name + '</option>');
            });
        },
        error: function (xhr, status, error) {
            console.error("Error fetching data: ", status, error);
        }
    });

});

function showModalEditProfile(NIK) {
    resetForm();

    $.ajax({
        type: "GET",
        url: "https://localhost:7245/api/Profile/" + NIK,
        headers: {
            Authorization: 'Bearer ' + token
        },
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var obj = result.data;
            $('#inputNik').val(obj.nik);
            $('#inputFirstName').val(obj.firstName);
            $('#inputLastName').val(obj.lastName);
            $('#inputPhone').val(obj.phone);
            $('#inputEmail').val(obj.email);
            $('#inputBirthDate').val(obj.birthDate ? obj.birthDate.split('T')[0] : "");

            $('#inputUniversityProfile').val(obj.university_Id);
            $('#inputDegree').val(obj.degree);
            $('#inputGpa').val(obj.gpa);
            $('#modalEditProfile').modal('show');
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function saveEditProfile() {
    let dataBirthDate = $('#inputBirthDate').val() === '' ? null : $('#inputBirthDate').val();
    let inputNik = $('#inputNik').val()
    var profile = {
        nik: inputNik,
        firstName: $('#inputFirstName').val(),
        lastName: $('#inputLastName').val(),
        phone: $('#inputPhone').val(),
        email: $('#inputEmail').val(),
        birthDate: dataBirthDate,
        university_Id: $('#inputUniversityProfile').val(),
        gpa: $('#inputGpa').val(),
        degree: parseInt($('#inputDegree').val())
    };
    $.ajax({
        type: "PUT",
        url: "https://localhost:7245/api/Profile/" + inputNik,
        data: JSON.stringify(profile),
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
                title: "Success edit profile."
            });
            loadProfileData(inputNik);
            $('#modalEditProfile').modal('hide');
            resetForm();
        },
        error: function (xhr) {
            console.log(xhr)
            const errorMessage = xhr.responseJSON.message;
            if (errorMessage.includes('Email')) {
                $('#inputEmail').addClass('is-invalid');
                $('#emailFeedback').text(errorMessage);
            }
        }
    });
}

$(function () {
    $.validator.setDefaults({
        submitHandler: function () {
            saveEditProfile()
        }
    });
    $('#editProfileForm').validate({
        rules: {
            gpa: {
                required: true,
                number: true,
                min: 0,
                max: 4,
                pattern: /^\d+(\.\d{1,2})?$/
            },
            phone: {
                required: true,
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
                required: "Please enter your phone number",
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

function resetForm() {
    $('#inputFirstName, #inputLastName, #inputPhone, #inputEmail, #university, #inputGpa, #degree').removeClass('is-invalid');
}

function loadProfileData(NIK) {
    $.ajax({
        url: "https://localhost:7245/api/Profile/" + NIK,
        type: "GET",
        headers: {
            "Authorization": `Bearer ${token}`
        },
        success: function (data) {
            const profile = data.data;
            $('#nikProfile').text(profile.nik || "N/A");
            $('#fullName').text(profile.firstName + " " + profile.lastName || "N/A");
            $('#firstName').text(profile.firstName || "N/A");
            $('#lastName').text(profile.lastName || "N/A");
            $('#phone').text(profile.phone || "-");
            $('#email').text(profile.email || "N/A");

            if (profile.birthDate != null) {
                const formatBirthDate = moment(profile.birthDate).format('LL')
                $('#birthDate').text(formatBirthDate);
            } else {
                $('#birthDate').text("-");
            }
            $('#university').text(profile.university_Name || "N/A");
            $('#gpa').text(profile.gpa !== null && profile.gpa !== undefined ? profile.gpa : "N/A");
            $('#degree').text(degreeMapping[profile.degree] || "N/A");
            
        },
        error: function () {
            console.error("Error fetching profile data.");
        }
    });
}

