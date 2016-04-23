/*
______ _      _   _  _____ _    _ _____ 
| ___ \ |    | \ | ||  ___| |  | /  ___|
| |_/ / |    |  \| || |__ | |  | \ `--. 
|    /| |    | . ` ||  __|| |/\| |`--. \
| |\ \| |____| |\  || |___\  /\  /\__/ /
\_| \_\_____/\_| \_/\____/ \/  \/\____/ 

Rlnews.co.uk

*/

//Active class for news feed navigation
$(function () {
    var url = window.location.pathname;
    var split = url.split('/');
    var action = split[1];

    switch (action) {
        case "news":
            $('.nav a#news').addClass('nav-active');
            $('.nav a#myteam').removeClass('nav-active');
            break;
        case "my-team":
            $('.nav a#myteam').addClass('nav-active');
            $('.nav a#news').removeClass('nav-active');
            break;
    }
});

//Active class for news feed navigation
$(function () {
    var url = window.location.pathname;
    var split = url.substring(url.lastIndexOf('/') + 1);

    switch (split) {
        case "latest":
            $('.sm-container a#latest').addClass('active-option');
            $('.sm-container a#popular').removeClass('active-option');
            $('.sm-container a#trending').removeClass('active-option');
            $('.sm-container a#discussed').removeClass('active-option');
            break;
        case "popular":
            $('.sm-container a#popular').addClass('active-option');
            $('.sm-container a#Latest').removeClass('active-option');
            $('.sm-container a#trending').removeClass('active-option');
            $('.sm-container a#discussed').removeClass('active-option');
            break;
        case "trending":
            $('.sm-container a#trending').addClass('active-option');
            $('.sm-container a#Latest').removeClass('active-option');
            $('.sm-container a#popular').removeClass('active-option');
            $('.sm-container a#discussed').removeClass('active-option');
            break;
        case "discussed":
            $('.sm-container a#discussed').addClass('active-option');
            $('.sm-container a#Latest').removeClass('active-option');
            $('.sm-container a#popular').removeClass('active-option');
            $('.sm-container a#trending').removeClass('active-option');
            break;
    }  
});

//Like button AJAX
$(".like-btn").click(function (e) {

    e.preventDefault();

    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[0];
    var newsid = $(this).val();

    $.ajax({
        url: baseUrl + "/news/LikeNewsItem",
        type: "POST", 
        cache: false, 
        data: { 'newsid': newsid },
        success: function (data) {
            if (data.success) {
                $('#scoreid-' + newsid).text(data.message);
            }
        },
        error: function (xhr) {
            alert('error');
        }
    });
});

//Dislike button AJAX
$(".dislike-btn").click(function (e) {
    e.preventDefault();

    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[0];
    var newsid = $(this).val();

    $.ajax({
        url: baseUrl + "news/DislikeNewsItem",
        type: "POST",
        cache: false,
        data: { 'newsid': newsid },
        success: function (data) {
            if (data.success) {
                $('#scoreid-' + newsid).text(data.message);
            }
        },
        error: function (xhr) {
            alert('error');
        }
    });
});

//View counter AJAX
$(".view-tracker").click(function () {

    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[0];
    var newsid = $(this).attr('id');

    $.ajax({
        url: baseUrl + "news/AddViewToNewsItem",
        type: "POST",
        cache: false,
        data: { 'newsid': newsid }
    });
});

//Hide related news on page load
$(document).ready(function () {
    $(".related-articles").hide();

    //Click to show and hide related news
    $(".view-related").click(function (e) {
        e.preventDefault();
        $(this).closest("div").find(".related-articles").fadeToggle(200);

        if (this.text === "View Related Articles") {
            $(this).text("Hide Related Articles");
        } else {
            $(this).text("View Related Articles");
        }

    });
});

//Hide related news on page load
$(document).ready(function () {
    $(window).load(function () {
        $('#lockedModal').modal('show');
    });

    $('#lockedModal').modal({
        backdrop: 'static',
        keyboard: false
    })
});

