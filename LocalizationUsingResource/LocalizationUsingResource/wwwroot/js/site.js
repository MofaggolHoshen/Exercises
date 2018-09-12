// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#selectLanguage").on('change', function () {

        var data = $("#selectLanguage").val();
        var uri = window.location.origin + "/Home/SetLanguage";

        $.ajax({
            type: "POST",
            url: uri,
            data: { culture: data, returnUrl: window.location.href },
            success: function (data) {
                if (data) {
                    alert("Please refresh your page!!");
                }
            }
        });
    });
});