var token = JSON.parse(localStorage.getItem("loginSession") || '{}')?.token || null;

$(document).ready(function () {
    //checkLogin()
    //checkRole();
    getData(function (data) {
        if (data) {
            updateCharts(data.data);
        } else {
            console.log('gagal');
        }
    });

    // Function to update charts based on received data
    function updateCharts(receivedData) {
        var degrees = receivedData.map(item => item.degree);
        var totals = receivedData.map(item => item.total);

        // Update Area Chart
        var areaChartCanvas = $('#areaChart').get(0).getContext('2d');
        var areaChartData = {
            labels: degrees,
            datasets: [{
                label: 'Total per Degree (Area)',
                backgroundColor: 'rgba(60,141,188,0.9)',
                borderColor: 'rgba(60,141,188,0.8)',
                pointRadius: false,
                pointColor: '#3b8bba',
                pointStrokeColor: 'rgba(60,141,188,1)',
                pointHighlightFill: '#fff',
                pointHighlightStroke: 'rgba(60,141,188,1)',
                data: totals
            }]
        };

        var areaChartOptions = {
            maintainAspectRatio: false,
            responsive: true,
            legend: {
                display: false
            },
            scales: {
                xAxes: [{
                    gridLines: {
                        display: false,
                    }
                }],
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    },
                    gridLines: {
                        display: false,
                    }
                }]
            }
        };

        new Chart(areaChartCanvas, {
            type: 'line',
            data: areaChartData,
            options: areaChartOptions
        });

        // Update Line Chart
        var lineChartCanvas = $('#lineChart').get(0).getContext('2d');
        var maxTotal = Math.max(...totals) + 1;
        var lineData = {
            labels: degrees,
            datasets: [{
                label: 'Total per Degree (Line)',
                borderColor: 'rgba(255, 99, 132, 1)',
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                fill: true,
                data: totals,
                tension: 0.1
            }]
        };

        var lineOptions = {
            maintainAspectRatio: false,
            responsive: true,
            legend: {
                display: false
            },
            scales: {
                xAxes: [{
                    gridLines: {
                        display: false,
                    }
                }],
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        max: maxTotal
                    },
                    gridLines: {
                        display: false,
                    }
                }]
            }
        };

        new Chart(lineChartCanvas, {
            type: 'line',
            data: lineData,
            options: lineOptions
        });

        // Update Donut Chart
        var donutChartCanvas = $('#donutChart').get(0).getContext('2d');
        var donutData = {
            labels: degrees,
            datasets: [{
                data: totals,
                backgroundColor: ['#f56954', '#00a65a', '#f39c12', '#00c0ef', '#3c8dbc'],
            }]
        };

        var donutOptions = {
            maintainAspectRatio: false,
            responsive: true,
        };

        new Chart(donutChartCanvas, {
            type: 'doughnut',
            data: donutData,
            options: donutOptions
        });

        // Update Bar Chart
        var barChartCanvas = $('#barChart').get(0).getContext('2d');
        var barData = {
            labels: degrees,
            datasets: [{
                label: 'Total per Degree (Bar)',
                backgroundColor: 'rgba(60,141,188,0.9)',
                borderColor: 'rgba(60,141,188,0.8)',
                borderWidth: 1,
                data: totals
            }]
        };

        var barOptions = {
            maintainAspectRatio: false,
            responsive: true,
            datasetFill: false,
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        max: maxTotal
                    }
                }]
            }
        };

        new Chart(barChartCanvas, {
            type: 'bar',
            data: barData,
            options: barOptions
        });



        // Update Stacked Bar Chart
        var stackedBarChartCanvas = $('#stackedBarChart').get(0).getContext('2d');
        var stackedBarData = {
            labels: degrees,
            datasets: [{
                label: 'Total per Degree (Stacked)',
                backgroundColor: 'rgba(60,141,188,0.9)',
                data: totals
            }]
        };

        var stackedBarOptions = {
            maintainAspectRatio: false,
            responsive: true,
            scales: {
                xAxes: [{
                    stacked: true,
                    gridLines: {
                        display: false,
                    }
                }],
                yAxes: [{
                    stacked: true,
                    gridLines: {
                        display: false,
                    }
                }],
            },
            legend: {
                display: true
            }
        };

        new Chart(stackedBarChartCanvas, {
            type: 'bar',
            data: stackedBarData,
            options: stackedBarOptions
        });

        // Update Pie Chart
        var pieChartCanvas = $('#pieChart').get(0).getContext('2d');
        var pieData = {
            labels: degrees,
            datasets: [{
                data: totals,
                backgroundColor: ['#f56954', '#00a65a', '#f39c12', '#00c0ef', '#3c8dbc'],
            }]
        };

        var pieOptions = {
            maintainAspectRatio: false,
            responsive: true,
        };

        new Chart(pieChartCanvas, {
            type: 'pie',
            data: pieData,
            options: pieOptions
        });
    }
});

function getData(callback) {
    $.ajax({
        url: 'https://localhost:7245/api/GetData/byDegree',
        method: 'GET',
        headers: {
            Authorization: 'Bearer ' + token // Corrected here
        },
        success: function (data) {
            callback(data);
        },
        error: function (error) {
            console.error('Error fetching data:', error);
            callback(null);
        }
    });
}
