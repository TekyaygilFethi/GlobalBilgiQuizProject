var hubConn = new signalR.HubConnectionBuilder()
    .withUrl("/quizHub")
    .build();

hubConn.on('startcontest',
    function () {
        window.location.href = '/yarisma';
    });
hubConn.start();