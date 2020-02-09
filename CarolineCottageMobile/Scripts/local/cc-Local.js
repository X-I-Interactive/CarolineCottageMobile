$(document).ready(function () {

    $("#page-home").click(function () {
        //window.scrollTo(0, 0);
        $('html, body').animate({ scrollTop: 0 }, 'fast');
    });

    $(".viewPrivacy").click(function (e) {

        var jqxhr1 = $.ajax({
            type: 'POST', url: "/Home/PrivacyStatement"
        });
        $.when(jqxhr1).done(function (responseStatement, textStatus, jqXHR) {
            $("#generalBody").html(responseStatement);
            $('#generalModal').modal();
        });
    });

    $(".viewCottageOverview").click(function (e) {

        var jqxhr1 = $.ajax({
            type: 'POST', url: "/Home/CottageOverview"
        });
        $.when(jqxhr1).done(function (responseStatement, textStatus, jqXHR) {
            $("#generalBody").html(responseStatement);
            $('#generalModal').modal();
        });
    });

    //  this shows prices and availability modal
    $(".PriceLink").click(function (e) {
        var jqxhr1 = $.ajax({
            type: 'POST', url: "/Home/CalendarList"
        });
        $.when(jqxhr1).done(function (responseStatement, textStatus, jqXHR) {
            $("#bookingBody").html(responseStatement);
            $('#bookingModal').modal();
        });
    });

    $(".contactUsLink").click(function () {
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

    $("#imagemodal").click(function () {
        $("#imagemodal").modal("hide");
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
        var form = $("#EnquiryForm")
            .removeData("validator") /* added by the raw jquery.validate plugin */
            .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin*/
        $.validator.unobtrusive.parse(form);
    });
}

$(function () {
    $(document).on("click", "a", function (e) {
        if ($(this).attr("href") === "#") {
            e.preventDefault();
            return;
        }
        var hash = this.href.split('#')[1];
        if (hash) {
            history.replaceState(null, null, window.location.pathname + window.location.search + '#' + hash);
        }
    });
});