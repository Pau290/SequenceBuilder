
$(document).ready(function () {


    $('#btnInit').click(() => {

        switchOnRadar();
        console.log("switch on radar");
    });

    function switchOnRadar() {

        switchOnSocketRadar();
    }


    var radars = {};

    initRadars();

    function initRadars() {

        radars.allRadars = new Array();
    }

    initRadar(1);

    function initRadar(index) {

        var the_canvas = document.getElementById("radar" + index);

        var the_ctx = the_canvas.getContext("2d");

        var the_chip = {};

        the_chip.canvas = the_canvas;

        the_chip.ctx = the_ctx;

        radars.allRadars.push(the_chip);
    }


    var radar1 = radars.allRadars[0];

    Begin();
    

    function Begin() {

        setupTheAndroids();

        setupTheDocks();

        setupTheGoods();

        setupTheShips();
    }



    function setupTheAndroids() {

        setupAndroids();

        //initAndroids(10);
    }

    function setupTheDocks() {

        setupDocks();

        //initDocks(36);
    }

    function setupTheGoods() {

        setupGoods();
    }

    function setupTheShips() {

        setupShips();

        //initShips(15);
    }


    function drawDock(index) {

        var dock = docks.allDocks[index - 1];

        radar1.ctx.beginPath();

        radar1.ctx.rect(dock.xpoint, dock.ypoint, dock.radius, dock.radius);

        radar1.ctx.stroke();

        radar1.ctx.fillStyle = dock.color;
        radar1.ctx.fill();
        radar1.ctx.closePath();
    }


    function drawAndroidGoods(android) {
       
        for (var i = 1; i <= android.goods; i++) {

            var x = android.xpoint;

            x += 3;

            var y = android.ypoint;

            y -= 3;

            radar1.ctx.rect(x, y, 3, 2);

            radar1.ctx.stroke();
            radar1.ctx.fillStyle = android.color;
            radar1.ctx.fill();
            radar1.ctx.closePath();   

        }
    }

    function drawAndroid(index) {

        var android = androids["android_" + index];

        radar1.ctx.beginPath();
        radar1.ctx.arc(android.xpoint, android.ypoint, android.radius, 0, Math.PI * 2);
        radar1.ctx.fillStyle = android.color;
        radar1.ctx.fill();
        radar1.ctx.closePath();    

        if (android.isearching) {
            moveinBoundAmbit(android);
        }

        drawAndroidGoods(android);

    }

    function moveShipinBoundAmbit(ship) {

        var mrg = 12;

        if (ship.xpoint + mrg >= 800 - ship.circleradius) {

            ship.xpoint -= 2;
        }

        if (ship.xpoint - mrg <= 0 + ship.circleradius) {

            ship.xpoint += 2;
        }

        if (ship.ypoint >= 400 - ship.circleradius) {

            ship.ypoint -= 2;
        }

        if (ship.ypoint <= 0 + ship.circleradius) {

            ship.ypoint += 2;
        }

    }

    function deriveShip(ship) {

        var rnd = random(1, 100);

        if (rnd > 80) {

            ship.circleradius -= random(0, 1);
        }
        else if (rnd > 60) {

            ship.circleradius += random(0, 1);
        }   
    }

    function moveInCircles(ship, speed, circleradius, circleCenterX, circleCenterY, color, freezed) {


        // to place the square correctly we must add the calculated
        // new x and y values to the circle center
        //var x = newX + circleCenterX;
        //var y = newY + circleCenterY;

        var x = circleCenterX;
        var y = circleCenterY;

        if (!freezed) {

            // Math.PI/180 is for transforming angle into radiant
            // Math.cos(angle) is the ratio of adjacent to hypothenuse
            // Math.sin(angle) is the ratio of opposite to hypothenuse
            // the hypothenuse is the radius
            var newX = circleradius * Math.cos(angle * (Math.PI / 180));
            var newY = circleradius * Math.sin(angle * (Math.PI / 180));

            x += newX;
            y += newY;

            if (x < 780 && y < 780 && x > 20 && y > 400) {

                deriveShip(ship);
            }            
        }

        radar1.ctx.fillStyle = color;

        radar1.ctx.font = '12px serif';
        radar1.ctx.fillText('◄', x, y);

        //radar1.ctx.fillRect(x, y, width, height);

        // increase the angle so that it moves in circular way
        // it is not necessary to limit/reset the angle to 360°
        // because sinus and cosinus work for angles bigger than 360°
        // sin(0) = sin(180) = sin(360) = sin(540) = sin(720) ...
        angle += speed;  
        
    }


    // speed of the movement
    // initially 1, means it increases the x value
    // if set to -1 the square moves into the other direction


    // the radius and angle of the circle, we start at angle 0
    var angle = 0;
    
    function drawGood(good) {

        radar1.ctx.rect(good.xpoint, good.ypoint, good.length, good.height);

        radar1.ctx.stroke();
        //radar1.ctx.fillStyle = good.color;
        radar1.ctx.fill();
        radar1.ctx.closePath(); 

        good.ypoint += good.dy;
    }


    function draw() {


        radar1.ctx.clearRect(0, 0, radar1.canvas.width, radar1.canvas.height);

        drawGrid();

        $.each(androids.allAndroids, (i) => {

            drawAndroid(androids.allAndroids[i].index);
        });

        $.each(docks.allDocks, (i) => {

            drawDock(docks.allDocks[i].index);
        }); 


        $.each(goods.allGoods, (i) => {

            drawGood(goods.allGoods[i]);
        }); 

        $.each(ships.allShips, (i) => {

            var ship = ships.allShips[i];  

            moveInCircles(
                ship,
                0.02,
                ship.circleradius,
                ship.xpoint,
                ship.ypoint,
                ship.color,
                ship.freezed);

            moveShipinBoundAmbit(ship);

        });
               
    }

    function drawGrid() {

        for (var x = 0.5; x < 800; x += 40) {
            radar1.ctx.moveTo(x, 0);
            radar1.ctx.lineTo(x, 800);
        }

        for (var y = 0.5; y < 800; y += 40) {
            radar1.ctx.moveTo(0, y);
            radar1.ctx.lineTo(800, y);
        }

        radar1.ctx.strokeStyle = "#F9F5F1";
        radar1.ctx.stroke();
    }


    setInterval(() => draw(), 10);


    //setTimeout(() => {

    //    androidToShip(2, 4);

    //    androidToDock(6, 24);

    //}, 2000);


});