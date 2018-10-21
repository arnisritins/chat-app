(function () {

    // Configure hub
    var connection = new signalR.HubConnectionBuilder()
        .withUrl('/chatHub')
        .configureLogging(signalR.LogLevel.Information)
        .build();

    // Add event listeners
    connection.on('ReceiveMessage', onMessageReceived);

    // Start connection
    connection.start()
        .catch(handleError);

    // Define elements
    var messages = document.getElementById('messages');
    var form = document.getElementById('form');
    var input = document.getElementById('input');

    form.addEventListener('submit', function (e) {
        e.preventDefault();
        sendMessage();
    });

    /**
     * Process received message
     * 
     */
    function onMessageReceived(user, message) {
        var elem = document.createElement('div');
        elem.innerHTML = '<strong>' + user +': </strong>' + message;

        messages.appendChild(elem);
    }

    /**
     * Send message
     * 
     */
    function sendMessage() {
        var message = input.value.trim();

        if (!message)
            return;

        connection
            .invoke("SendMessage", message)
            .catch(handleError);

        input.value = '';
    }

    /**
     * Error handler
     * 
     */
    function handleError(err) {
        console.log(err.toString());
    }

})();
