// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function toggleFilter() {
    let sidebar = document.getElementById("filter-sidebar");
    if (sidebar.style.left === "0px")
        sidebar.style.left = "-180px";
    else
        sidebar.style.left = "0px";
}

let myForm = document.getElementById('filter-form');
myForm.addEventListener('submit', function () {
    let allInputs = myForm.getElementsByTagName('input');
    for (let i = 0; i < allInputs.length; i++) {
        let input = allInputs[i];
        if (input.name && !input.value) {
            input.name = '';
        }
    }
});