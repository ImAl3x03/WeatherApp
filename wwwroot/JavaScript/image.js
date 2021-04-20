let image = document.querySelector("div.weather img");

if(image.src === "") {
    image.classList.add("hide");
}
else {
    image.classList.remove("hide");
}