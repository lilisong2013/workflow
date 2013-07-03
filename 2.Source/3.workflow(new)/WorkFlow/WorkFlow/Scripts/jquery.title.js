$(document).ready(function () {
    var option = {
        url: titleUrl,
        type: "POST",
        dataType: "json",
        data: { pageName: PageName },
        success: showResponse
    };

    $.ajax(option);

    //ajax成功响应的回调函数
    function showResponse(responseText, statusText) {
        //alert(responseText);
        var dataJson = eval("(" + responseText + ")");
        document.title = dataJson.pageName;
    }
});

