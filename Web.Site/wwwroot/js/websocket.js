

    var socket = null;

    function switchOnSocketRadar() {

        socket = new WebSocket("ws://localhost:8181");

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
            console.log("Connection established.");
        }

        socket.onmessage = function (event) {

            var order = JSON.parse(event.data);
            
            interpretOrder(order);
        }

        socket.onclose = function (event) {

            console.log("Connection closed.");
            var code = event.code;
            var reason = event.reason;
            var wasClean = event.wasClean;

            if (wasClean) {
                alert("Connection closed normally.");
            }
            else {
                console.log("Connection closed with message " + reason +
                    "(Code: " + code + ")");
            }
        }

        socket.onerror = function (event) {

            console.log("check the socket server");
        }

    }
