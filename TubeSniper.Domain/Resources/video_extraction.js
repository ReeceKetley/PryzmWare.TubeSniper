(function () {
    var videos = [];
    var items = $("ytd-video-renderer");
    items.each(function (index, item) {
        var video = {};
        video["id"] = $("#video-title", item).attr("href");
        video["title"] = $("#video-title", item).attr("title");
        video["chanel_name"] = $("a[href*=\"user\"]", item).text();
        video["channel_url"] = $("a[href*=\"user\"]", item).attr("href");
        video["views"] = $("#metadata-line > span:nth-child(1)", item).text();
        video["date"] = $("#metadata-line > span:nth-child(2)", item).text();
        video["duration"] = $("ytd-thumbnail-overlay-time-status-renderer", item).text().trim();
        videos.push(video);
    });

    return JSON.stringify(videos);
})();