//Take the paragraph where i can show the time
let element = document.querySelector('nav p#time');

//Function where i take the time
function showTime() {
    let date = new Date();
    element.innerHTML = date.toLocaleDateString() + " " + date.toLocaleTimeString();
}

setInterval(showTime, 1000);