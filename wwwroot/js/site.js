﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Nav JS
const path = window.location.pathname;
const navItem = document.querySelectorAll('.nav-item');
navItem.forEach((item) => {
    const href = item.childNodes[1].pathname;
    if (href == path) {
        item.classList.add('active-item');
    }  
})