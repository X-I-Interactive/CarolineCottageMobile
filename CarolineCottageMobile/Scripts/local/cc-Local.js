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
        $('#bookingModal').modal();
    });    

    $('a[href^="#"]').on('click', function () {        
        $('.navbar-collapse').collapse('hide');
    });
    

});

