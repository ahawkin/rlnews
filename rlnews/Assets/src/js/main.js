//RlNews JS - Main

//Like button AJAX
$(".like-btn").click(function (e) {
    e.preventDefault();

    var newsid = $(this).val();

    $.ajax({
        url: "news/LikeNewsItem",
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

    var newsid = $(this).val();

    $.ajax({
        url: "news/DislikeNewsItem",
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