var isTurnedOn = false;
var intervalFunction;

class UpdateStatsModel {
    constructor(hours, minutes) {
        this.Hours = hours;
        this.Minutes = minutes;
    }
}

function UpdateTimer() {
    let currentValue = document.querySelector(".timer").innerHTML;
    let minutes = currentValue.split(':')[0];
    let seconds = currentValue.split(':')[1];

    if (currentValue == '00:00') {
        SwitchTimer();
        Reset();
        SendRequestToUpdateStats();

        return;
    }

    if (seconds == '00') {
        minutes = parseInt(minutes) - 1;
        seconds = '59';
        SendRequestToUpdateStats();
    }
    else {
        seconds = parseInt(seconds) - 1;

        if (seconds < 10){
            seconds = `0${seconds}`;
        }
    }

    document.querySelector('.timer').innerHTML = 
        `${minutes}:${seconds}`;
}

function SwitchTimer() {
    if (!isTurnedOn) {
        intervalFunction = setInterval(UpdateTimer, 1000);
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

function Reset() {
    document.querySelector('.timer').innerHTML = "25:00";
    document.querySelector('.timerBtn').value = "Start timer";
    isTurnedOn = false;
    clearInterval(intervalFunction);
}

function SendRequestToUpdateStats() {
    const url = "https://localhost:5001/Timer/UpdateStats";

    let data = new UpdateStatsModel(0, 1);

    let fetchResponse = fetch(url, {
        method: 'POST',
        mode: 'cors',
    }).then((response) => {
        if (response.status !== 200) {
            throw new Error("Server doesn't respond");
        }
    }).catch((error) => {
        console.log(error.message);
    });

    console.log(fetchResponse);
}
