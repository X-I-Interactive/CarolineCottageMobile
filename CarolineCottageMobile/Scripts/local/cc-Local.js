$(document).ready(function () {

    $("#viewPrivacy").click(function (e) {

        var jqxhr1 = $.ajax({
            type: 'POST', url: "/Home/PrivacyStatement"
        });
        $.when(jqxhr1).done(function (responseStatement, textStatus, jqXHR) {
            $("#privacyBody").html(responseStatement);
            $('#privacyModal').modal();
        });
    });

    $("#BookingLink").click(function (e) {
        var jqxhr1 = $.ajax({
            type: 'POST', url: "/Home/CalendarList"
        });
        $.when(jqxhr1).done(function (responseStatement, textStatus, jqXHR) {
            $("#bookingBody").html(responseStatement);
            $('#bookingModal').modal();
        });
    });

    $("#contactUsLink").click(function () {

        var jqxhr1 = $.ajax({
            type: 'POST', url: "/Home/ContactUs"
        });
        $.when(jqxhr1).done(function (responseStatement, textStatus, jqXHR) {
            $("#contactFormBody").html(responseStatement);
            var form = $("#ContactUsForm")
                .removeData("validator") /* added by the raw jquery.validate plugin */
                .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin*/
            $.validator.unobtrusive.parse(form);
            $('#contactUsModal').modal({ "backdrop": "static" });
        });
    });

    $('a[href^="#"]').on('click', function () {
        $('.navbar-collapse').collapse('hide');
    });


});

function ContactUsClose(response, ajaxResponse) {
    if (response.replyText !== "OK") {
        console.log("done");
    }
    $('#contactUsModal').modal('hide');
}

