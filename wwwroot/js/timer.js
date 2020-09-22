function UpdateTimer() {
    let now = new Date();
    let time = `${now.getHours()}:${now.getMinutes()}:${now.getSeconds()}`;

    //document.querySelector(".timer").innerHTML = time;
}

setInterval(UpdateTimer, 1000);