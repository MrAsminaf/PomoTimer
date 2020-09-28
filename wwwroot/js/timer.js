function updateTimer() {
    let currentValue = document.querySelector(".timer").innerHTML;
    //document.querySelector('.timer').innerHTML = ++counter;
    console.log(currentValue);
    let minutes = currentValue.split(':')[0];
    let seconds = currentValue.split(':')[1];
    console.log(`${minutes}:${seconds}`);
}

function timerOn() {
    if (!isTurnedOn) {
        intervalFunction = setInterval(updateTimer, 1000);
        document.querySelector('.timerBtn').value = "Stop";
        isTurnedOn = true;
    } else {
        clearInterval(intervalFunction);
        isTurnedOn = false;
    }
}

let counter = 0;
var isTurnedOn = false;
var intervalFunction;
