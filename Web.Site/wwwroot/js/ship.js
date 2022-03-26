
var ships = {};

function setupShips() {

    ships.allShips = new Array();
}

function initShips(n) {

    for (var i = 1; i <= n; i++) {

        initShip(i);
    }
}

function initShip(i){

    var ship = {};

    ship.i = i;

    ship.color = getColor('#A592D8', '#2C146C', i);

    ship.xpoint = random(600, 800);
    ship.ypoint = random(100, 400);

    ship.freezed = false;

    ship.circleradius = random(40, 80);

    ships.allShips.push(ship);
    logShip(i);
}


function stopShip(index) {

    var ship = ships.allShips[index - 1];

    ship.freezed = true;
}

