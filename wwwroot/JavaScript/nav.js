const navbarElement = document.querySelector("div.wrap header nav");
const mainElement = document.querySelector("main");
let margin = 130;
let number = 0;
let open = false;
let id = undefined;

function anim() {
    if(id === undefined) {
        id = setInterval(navAnimation, 10);
    }
}

function navAnimation() {
    if(open) {
        margin += 2;
        number += 2;
        navbarElement.style.left = number + 'px';
        mainElement.style.marginLeft = margin + 'px';
    }
    else {
        margin -= 2;
        number -= 2;
        navbarElement.style.left = number + 'px';
        mainElement.style.marginLeft = margin + 'px';
    }

    if(parseInt(navbarElement.style.left) === -100) {
        clearInterval(id);
        open = true;
        id = undefined;
    }

    if(parseInt(navbarElement.style.left) === 0) {
        clearInterval(id);
        open = false;
        id = undefined;
    }
}