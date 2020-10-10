let counter = 0;
var isTurnedOn = false;
var intervalFunction;

class UpdateStatsModel {
    constructor(hours, minutes) {
        this.Hours = hours;
        this.Minutes = minutes;
    }
}

function updateTimer() {
    let currentValue = document.querySelector(".timer").innerHTML;
    let minutes = currentValue.split(':')[0];
    let seconds = currentValue.split(':')[1];

    if (seconds == '00') {
        minutes = parseInt(minutes);
        --minutes;
        seconds = '59';
        updateMinuteStats();
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

function updateMinuteStats() {
    const url = "http://localhost:5000/Timer/UpdateStats";

    let data = new UpdateStatsModel(0, 1);

    let fetchResponse = fetch(url, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-type': 'application/json'
        },
        body: JSON.stringify(data)
    }).then((response) => {
        if (response.status !== 200) {
            throw new Error("Server doesn't respond");
        }
    }).catch((error) => {
        console.log(error.message);
    });

    console.log(fetchResponse);
}
