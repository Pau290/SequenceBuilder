

function randomColor() {
    return '#' + ('000000' + Math.floor(Math.random() * 16777215).toString(16)).slice(-6);
}

function random(min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
}


function getColor(gama1, gama2, tone) {

    return chroma.mix(gama1, gama2, tone / 24);
}