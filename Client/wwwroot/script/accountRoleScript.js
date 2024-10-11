var token = JSON.parse(localStorage.getItem("loginSession") || '{}')?.token || null;
$(document).ready(function () {
    //checkLogin();
    //checkRole();

    $("#tableAccountRole").DataTable({
        "paging": true,
        "responsive": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "ajax": {
            url: "https://localhost:7245/api/GetData/accountRole",
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
            { "data": "nik" },
            { "data": "accountName"},
            {
                "render": function (data, type, row) {
                    // Gabungkan nama role menjadi string
                    return row.roles.map(role => role.roleName).join(', ');
                }
}
        ],
    });

});