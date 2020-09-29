function updateTimer() {
    let currentValue = document.querySelector(".timer").innerHTML;
    let minutes = currentValue.split(':')[0];
    let seconds = currentValue.split(':')[1];

    if (seconds == '00') {
        minutes = parseInt(minutes);
        --minutes;
        seconds = '59';
    }
    else {
        seconds = parseInt(seconds);
        --seconds;
    }

    if (parseInt(seconds) < 10){
        seconds = `0${seconds}`;
    }

    document.querySelector('.timer').innerHTML = 
        `${minutes}:${seconds}`;
}

function timerOn() {
    if (!isTurnedOn) {
        intervalFunction = setInterval(updateTimer, 1000);
        document.querySelector('.timerBtn').value = "Stop";
        isTurnedOn = true;
        console.log("On");
    } else {
        clearInterval(intervalFunction);
        document.querySelector('.timerBtn').value = "Start timer";
        isTurnedOn = false;
        console.log("Stopped");
    }
}

let counter = 0;
var isTurnedOn = false;
var intervalFunction;
