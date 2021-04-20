/* All the image in the web page */
let image = document.querySelectorAll("main img");

/* The paragraph where i show the error */
let text = document.querySelector("div.weather p ");

/* Add and remove 'hide' class to the image if there's error */
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