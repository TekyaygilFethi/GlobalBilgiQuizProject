function MapQuestionAnswers(isIncrement) {
    $.ajax({
        type: "GET",
        url: '/Admin/ChangeQuestion?IsIncrement=' + isIncrement,
        success: function (result) {

            var data = result.data;

            if (data === "redirect") {
                window.location.href = '/kapanis';
            }

            $('#question').html('Soru: ' + data.question);

            var a1String = "A) " + data.answer1;
            $('#a1').html(a1String);

            var a2String = "B) " + data.answer2;

            $('#a2').html(a2String);

            var a3String = "C) " + data.answer3;
            $('#a3').html(a3String);

            var a4String = "D) " + data.answer4;

            $('#a4').html(a4String);
            if (isIncrement === "true") {
                var interval = window.setInterval(function () {

                    $.ajax({
                        type: "GET",
                        url: '/Home/GetTimeDifference',
                        success: function (received) {

                            seconds = received.seconds;

                            $('#timer').html(seconds);


                        },
                        error: function (error) {

                            console.log("Hata! " + error);
                        },
                    });
                }, 1000);
            }
        },
        error: function (error) {
            alert("Hata! " + error);
        },
    });
}

document.addEventListener('DOMContentLoaded', function () {
    var interval = window.setInterval(function () {

        $.ajax({
            type: "GET",
            url: '/Home/GetTimeDifference',
            success: function (received) {

                seconds = received.seconds;

                $('#timer').html(seconds);

                if (seconds === -1) {
                    clearInterval(interval);
                }

            },
            error: function (error) {

                console.log("Hata! " + error);
            },
        });
    }, 1000);




    MapQuestionAnswers("false");
    singleValues = [0, 0, 0, 0];
    var singleChart = new Chart(document.getElementById("serviceSingleChart"),
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
                    data: singleValues
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

    var valuesGlobal = [0, 0];
    var globalChart = new Chart(document.getElementById("serviceGlobalChart"),
        {
            type: 'pie',
            data: {
                labels: ["Doğru Cevap", "Yanlış Cevap"],
                datasets: [{
                    label: "Doğruluk Oranları",
                    backgroundColor: ["#3e95cd", "#8e5ea2"],
                    data: valuesGlobal
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

    var hubConn = new signalR.HubConnectionBuilder()
        .withUrl("/quizHub")
        .build();
    hubConn.on('broadcast',
        function (info) {
            globalChart.data.datasets[0].data = [info.info.totalTrue, info.info.totalFalse];
            globalChart.update();
        });
    hubConn.on('broadcastbyquestion',
        function (info) {
            singleChart.data.datasets[0].data = [info.info.chosenCounts[0], info.info.chosenCounts[1], info.info.chosenCounts[2], info.info.chosenCounts[3]];
            singleChart.update();
        });
    hubConn.on('resetsinglebarchart',
        function (info) {
            singleChart.data.datasets[0].data = [0, 0, 0, 0];
            singleChart.update();
        });


    hubConn.on('endcountdown',
        function () {
            $('#nextQuestion').prop("disabled", false);
            clearInterval(interval);
        });

    hubConn.start();

    $('#nextQuestion').click(function () {
        $('#nextQuestion').prop("disabled", true);
        MapQuestionAnswers("true");
    });
});