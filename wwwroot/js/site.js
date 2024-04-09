// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Nav JS
const path = window.location.pathname;
console.log(path);

const navItem = document.querySelectorAll('.nav-item');
console.log(navItem);

navItem.forEach((item) => {
    const href = item.childNodes[1].href;
    if (href.includes(path)) {
        console.log('true');
        item.classList.add('active-item');
    }
    
})