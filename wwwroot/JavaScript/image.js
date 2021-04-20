let image = document.querySelectorAll("main img");
let text = document.querySelector("div.weather p ");

if(text.innerHTML === "Please insert a valid city") {
    for (let img of image) {
        img.classList.add("hide");
    }
}
else {
    for(let img of image) {
        img.classList.remove("hide");
    }
}