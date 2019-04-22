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
            $('#contactUsModal').modal();
        });
    });

    $('a[href^="#"]').on('click', function () {        
        $('.navbar-collapse').collapse('hide');
    });
    

});

