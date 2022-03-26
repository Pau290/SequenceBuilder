
var goods = {};

function setupGoods() {

    goods.allGoods = new Array();
}


function initGoods(n) {

    for (var i = 1; i <= n; i++) {

        initGood(i);
    }
}


function initGood(x, y) {

    var good = {};

    good.xpoint = x;
    good.ypoint = y;

    good.length = 3;
    good.height = 2;

    good.color = randomColor();

    good.dx = 2;
    good.dy = 2;

    goods.allGoods.push(good);

}