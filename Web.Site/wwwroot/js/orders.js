

function interpretOrder(order) {


    if (order.type == "init.androids") {

        orderInitAndroids(order.num);       
    }

    if (order.type == "init.ships") {

        orderInitShips(order.num);
    }

    if (order.type == "init.docks") {

        orderInitDocks(order.num);
    }

    if (order.type == "load.goods") {

        androidLoadGoods(order.android, order.goods, order.ship);

        var newColor = getColor('#F18928', '#BB5B02', order.android);

        changeAndroidColor(order.android, newColor);
    }

    if (order.type == "android.search") {

        var x = random(100, 200);

        var y = random(100, 200);

        var newColor = getColor('#799131', '#020024', order.android);

        changeAndroidColor(order.android, newColor);

        androidToSearch(x, y, order.android);

    }

    // move

    if (!order.android) {
        return;
    }


    if (order.dock) {

        androidToDock(order.android, order.dock);
    }


    if (order.ship) {

        androidToShip(order.android, order.ship);
    }

}

function androidFreeDock(android, dock) {

    android.goods = 0;

    dock.goods = order.goods;
}

function androidLoadGoods(android, goods, ship) {

    var the_android = androids["android_" + android];

    the_android.goods = goods;

    logAndroidLoadGoods(android, goods, ship);
}

function androidToShip(android, ship) {

    stopAndroid(android);

    moveAndroidToShip(android, ship);

    logAndroidToShip(android, ship);

}


function moveAndroidToShip(android, ship) {

    var the_ship = ships.allShips[ship - 1];

    stopShip(ship);

    moveAndroidTo(the_ship.xpoint, the_ship.ypoint, android);
}

function androidToDock(android, dock) {
   
    unfreezeAndroid(android);

    setAndroidDock(android, dock);

    moveAndroidToDock(android, dock);

    logAndroidToDock(android, dock);

}


function orderInitAndroids(num) {

    initAndroids(num);

}

function orderInitDocks(num) {

    initDocks(num);

}

function orderInitShips(num) {

    initShips(num);

}

