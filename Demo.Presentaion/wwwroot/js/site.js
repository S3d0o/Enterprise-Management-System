// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('form input, form select, form textarea').on('blur', function () {
        $(this).valid();
    });
});
document.addEventListener('DOMContentLoaded', function () {
    const toastElList = [].slice.call(document.querySelectorAll('.toast'));
    const toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl, { autohide: true, delay: 5000 });
    });
    toastList.forEach(toast => toast.show());
});