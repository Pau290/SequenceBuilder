
if (window.WebSocket) {
    console.log("WebSockets supported.");
}
else {
    console.log("WebSockets not supported.");
    alert("Consider updating your browser for a richer experience.");
}

$(document).ready(function () {


    var socket = new WebSocket("wss://localhost:8181");

    function sendData() {

        if (socket.readyState === WebSocket.OPEN) {
            socket.send("data");
        }
    }

    function close() {
        if (socket.readyState === WebSocket.OPEN) {
            socket.close();
        }
    }

    socket.onopen = function (event) {
        alert("Connection established.");
    }

    socket.onmessage = function (event) {
        alert("Data received!");
    }

    socket.onclose = function (event) {
        console.log("Connection closed.");
        var code = event.code;
        var reason = event.reason;
        var wasClean = event.wasClean;

        var label = document.getElementById("status-label");

        if (wasClean) {
            alert("Connection closed normally.");
        }
        else {
            alert("Connection closed with message " + reason +
                "(Code: " + code + ")");
        }
    }

    socket.onerror = function (event) {
        console.log("Error occurred.");

        alert(event);
    }


});