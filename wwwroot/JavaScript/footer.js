let foo = document.querySelector("footer");

/* Change the footer position if there's something on the page */
function changeFoo(value) {
    if(value === undefined) {
        foo.style.position = "absolute";
    }
    else {
        foo.style.position = "static";
    }
}