

var docks = {};

function setupDocks() {

    docks.allDocks = new Array();
}

function initDocks(n) {

    for (var i = 1; i <= n; i++) {

        initDock(i, n);
    }
}

function changeDockColor(index) {

    var dock = docks.allDocks[index - 1];

    var color = getColor('#F18928', '#0A0A0A', index);

    dock.color = color;
}

function changeDockSize(index, size) {

    var dock = docks.allDocks[index - 1];

    dock.radius = size;
}


function initDock(index, total) {

    var dock = {};

    dock.index = index;

    var newColor = getColor('#60615D', '#000', index);

    dock.color = newColor;

    dock.radius = 8;

    var initx = 780;

    var posx = Math.round(initx / (total / (total - index)));

    var posy = 680;

    if (total > 20 && index > 20) {

        posx = Math.round(initx / (total / (total - (index - 20))));

        posx -= 10;

        posy += 40;
    }

    dock.xpoint = posx;
    dock.ypoint = posy;

    dock.goods = 0;

    docks.allDocks.push(dock);

    logDock(index);
}

