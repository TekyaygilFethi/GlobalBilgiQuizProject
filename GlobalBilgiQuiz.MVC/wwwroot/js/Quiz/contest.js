$(function () {
    var rnd1 = Math.floor((Math.random() * 10) + 1);
    var rnd2 = Math.floor((Math.random() * 10) + 1);

    $('#randoms').html("Cevabı işaretleyebilmek için işlemi çözün: " + rnd1 + "+" + rnd2 + "=?");

    var interval = window.setInterval(function () {
        $.ajax({
            type: "GET",
            url: "/Home/GetTimeDifference",
            success: function (received) {
                seconds = received.seconds;

                $('#timer').html(seconds);
                if (seconds == 1) {
                    clearInterval(interval);
                }

                window.setTimeout(function () {
                    window.location.href = '/sonuclar'; //henüz yapmadık
                }, seconds * 1000)
            }
        })
    }, 1000);

    $("label.btn").on('click', function (event) {
        if ($('input[name=AnswerID]').is(":enabled")) {
            var selectedChoiceBtn = $(this).find('input:radio');

            var choice = selectedChoiceBtn.val();
            var questionId = $('#questionid').val();
            var questionMetricId = $('#questionmetricid').val();
            var capcthasum = $('#captchasum').val();

            $.ajax({
                type: 'POST',
                url: '/yarismapost',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify({ questionmetricid: questionMetricId, questionid: questionId, answerid: choice, random1: rnd1, random2: rnd2, UserEnteredSumCaptcha: capcthasum }),
                success: function (received) {
                    if (received.isSuccess === false) {
                        $('#captchasum').css({
                            "border": "1px solid red"
                        });
                    } else {
                        $('input[name=AnswerID]').attr("disabled", true);
                        $('label.btn').removeClass('lblbtnhover');

                        $('label.btn').removeClass('btn-primary').addClass('btn-secondary');
                        selectedChoiceBtn.parent().removeClass("btn-secondary").addClass('btn-success');
                    }
                },
                dataType: 'json'
            });
        }
    });

    var hubConn = new signalR.HubConnectionBuilder()
        .withUrl("/quizhub")
        .build();

    hubConn.on('updatecounter', function (data) {
        $('#timer').html(data);
    });

    hubConn.on('endcountdown', function (data) {
        window.location = '/sonuclar';
    });


    //BURDA KALDIN!!!!
});