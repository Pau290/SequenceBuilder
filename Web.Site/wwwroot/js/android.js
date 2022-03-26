


var androids = {};


function setupAndroids() {

    androids.allAndroids = new Array();
}




function moveinBoundAmbit(android) {
    
    if (android.xpoint + android.dx > android.ambitx - android.radius
        || android.xpoint + android.dx < android.radius) {
        android.dx = -android.dx;
    }
    if (android.ypoint + android.dy > android.ambity - android.radius
        || android.ypoint + android.dy < android.radius) {
        android.dy = -android.dy;
    }

    android.xpoint += android.dx;
    android.ypoint += android.dy;

}



function initAndroids(n) {

    for (var i = 1; i <= n; i++) {

        initAndroid(i);
    }
}


function initAndroid(index) {

    var android = {};

    android.index = index;

    android.radius = 3;

    var newColor = getColor('#799131', '#020024', index);

    android.color = newColor;

    android.xpoint = random(200, 400);
    android.ypoint = random(100, 200);

    android.dx = 2;
    android.dy = 2;

    android.isearching = true;
    android.freezed = false;

    android.ambitx = android.xpoint;
    android.ambity = android.ypoint;

    android.goods = 0;

    android.dock = 0;

    androids["android_" + index] = android;

    androids.allAndroids.push(android);

    logAndroid(index);
}

function loadGoods(n) {

    android.goods = n;
}

function stopAndroid(index) {

    var android = androids["android_" + index];

    android.isearching = false;

}

function freezeAndroid(android) {

    android.isearching = false;

    android.freezed = true;
}

function changeAndroidColor(android, color) {

    var android = androids["android_" + android];

    android.color = color;
}

function unfreezeAndroid(index) {

    var android = androids["android_" + index];

    android.freezed = false;
}

function setAndroidDock(android, dock) {

    var android = androids["android_" + android];

    android.dock = dock;
}


function moveAndroid(index, x, y) {

    var android = androids["android_" + index];

    android.isearching = false;

    moveAndroidTo(x, y, index);

}

function moveAndroidToDock(android, dock) {

    var the_dock = docks.allDocks[dock - 1];

    moveAndroidTo(the_dock.xpoint, the_dock.ypoint, android);
}


function moveAndroidTo(x, y, index) {

    var android = androids["android_" + index];

    if (android.freezed) {
        return;
    }

    android.moveinterval =
        setInterval(() => {

            displaceAndroid(x, y, android);

    }, 5);

}

function androidToSearch(x, y, index) {

    var android = androids["android_" + index];

    unfreezeAndroid(index);

    android.goods = 0;

    android.ambitx = x + 100;
    android.ambity = y + 100;

    initGood(android.xpoint, android.ypoint);

    android.isearching = true;

    var dock = docks.allDocks[android.dock - 1];

    dock.goods = 4;

    android.moveinterval =
        setInterval(() => {

            displaceAndroidToSearch(x, y, android);

        }, 5);

}

function displaceAndroid(x, y, android) {

    if (android.freezed) {
        return;
    }

    var mrg = 4;

    if (((android.xpoint + mrg) >= x) && (android.ypoint + mrg >= y)) {

        freezeAndroid(android);

        clearInterval(android.moveinterval);
        return;
    }

    var _dx = 2;

    if ((android.xpoint + mrg) >= x) {

        _dx = - _dx;

        if (android.xpoint - mrg <= x) {
            _dx = 0;
        }
    }

    var _dy = 2;

    if ((android.ypoint + mrg) >= y) {

        _dy = - _dy;

        if (android.ypoint - mrg <= y) {
            _dy = 0;
        }
    }

    if (!android.freezed) {

        android.xpoint += _dx;

        android.ypoint += _dy;
    }

}


function displaceAndroidToSearch(x, y, android) {

    var mrg = 4;

    if (((android.xpoint - mrg) <= x) && (android.ypoint - mrg <= y)) {

        //freezeAndroid(android);

        clearInterval(android.moveinterval);
        return;
    }

    var _dx = 2;

    if ((android.xpoint - mrg) <= x) {

        _dx = - _dx;

        //if (android.xpoint + mrg >= x) {
        //    _dx = 0;
        //}
    }

    var _dy = 2;

    if ((android.ypoint - mrg) <= y) {

        _dy = - _dy;

        //if (android.ypoint + mrg >= y) {
        //    _dy = 0;
        //}
    }

    if (!android.freezed) {

        android.xpoint -= _dx;

        android.ypoint -= _dy;

    } else {

        console.log(android + " freezed");
    }

}