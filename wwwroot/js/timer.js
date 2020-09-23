function UpdateTimer() {
    let now = new Date();
    let time = `${now.getHours()}:${now.getMinutes()}:${now.getSeconds()}`;

    document.querySelector(".timer").innerHTML = time;
}

function timerOn() {
    let timerVal = document.querySelector(".timer").innerHTML;
    console.log(timerVal);
    isTurnedOn = !isTurnedOn;
}

function increment() {
    i = ++i;
    console.log(i);
}

let i = 0;
let isTurnedOn = false;

let int = setInterval(UpdateTimer, 1000);

if (isTurnedOn) {
    setInterval(increment, 1000);
} else {
    clearInterval(int);
}