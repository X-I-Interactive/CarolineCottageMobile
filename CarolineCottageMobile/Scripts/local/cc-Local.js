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
            $('#contactUsModal').modal();
        });
    });

    $('a[href^="#"]').on('click', function () {
        $('.navbar-collapse').collapse('hide');
    });

    $(".car-image").on("click", function () {
        var src = $(this).attr("src");
        $(".modal-img").prop("src", src);
        $("#imagemodal").modal("show");
    });

    $(document).on("click", ".viewLine", function () {        
        DisplayEnquiryForm(this);
    });

    $(document).on("click", ".closeEnquiry", function () {
        $(".enquiryFormBody").remove();
    });
});

function ContactUsClose(response, ajaxResponse) {
    if (response.replyText !== "OK") {
        console.log("error");
    }
    $('#contactUsModal').modal('hide');
}

function EnquiryClose(response, ajaxResponse) {
    if (response.replyText !== "OK") {
        console.log("error");
    }
    $(".enquiryFormBody").replaceWith(response.enquiryResult);
}

function DisplayEnquiryForm(that) {
    $(".enquiryFormBody").remove();
    var weekID = $(that).parents("div.row").prop("id");
    var anchor = $(that).parents("div.row");
    var jqxhr1 = $.ajax({
        type: 'POST', url: "/Home/EnquiryForm", data: { weekID: weekID }
    });
    $.when(jqxhr1).done(function (responseStatement, textStatus, jqXHR) {
        $(anchor).after(responseStatement);
    });
}
