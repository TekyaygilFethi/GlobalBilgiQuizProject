document.addEventListener('DOMContentLoaded', function () {

    $.ajax({
        type: "GET",
        url: '/Home/SeeResultsJson',
        success: function (received) {
            var values = received.data.chosenCounts;
            var question = received.data.currentQuestion;

            if (question === "3") {
                $('#explanation').html('YARIŞMAYI BİTİRDİNİZ!');
            }

            var chart = new Chart(document.getElementById("serviceChart"),
                {
                    type: 'bar',
                    data: {
                        labels: ["A", "B", "C", "D"],
                        datasets: [{
                            label: "Doğruluk Oranları",
                            backgroundColor: [
                                'rgba(255, 99, 132, 0.2)',
                                'rgba(255, 159, 64, 0.2)',
                                'rgba(255, 205, 86, 0.2)',
                                'rgba(75, 192, 192, 0.2)',],
                            data: values
                        }]
                    },
                    options: {
                        responsive: true,
                        title: {
                            display: true,
                            text: 'Hub Quiz'
                        }
                    }
                });
        },
        error: function (error) {

            alert("Hata! " + error);
        }
    });


    var hubConn = new signalR.HubConnectionBuilder()
        .withUrl("/quizHub")
        .build();


    hubConn.on('changequestion', function () {
        window.location.href = '/yarisma';
    });

    hubConn.start();
});