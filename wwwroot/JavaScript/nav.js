const navbarElement = document.querySelector("div.wrap header nav");
let number = 0;
let open = false;
let id = undefined;

function anim() {
    id = setInterval(navAnimation, 10);
    console.log("function called");
}

function navAnimation() {
    if(open) {
        number += 2;
        navbarElement.style.left = number + 'px';
    }
    else {
        number -= 2;
        navbarElement.style.left = number + 'px';
    }

    if(parseInt(navbarElement.style.left) === -100) {
        clearInterval(id);
        open = true;
    }

    if(parseInt(navbarElement.style.left) === 0) {
        clearInterval(id);
        open = false;
    }
}