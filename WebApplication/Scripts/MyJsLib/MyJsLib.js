function MyAjax(pAjaxParam) {
    $.ajax({
        "type": "get",
        "timeout": "6000",
        "url": pAjaxParam.url,
        "data": pAjaxParam.data,
        "success": function (pData, pStatusText) {
            alert(pData);
            alert(pStatusText);
            pAjaxParam.fnSuccess();
        },
        "error": function (pXHR, pStatusText) {
            alert(pXHR);
            alert(pStatusText);
        },
        "complete": function (pXHR, pStatusText) {
            alert(pXHR);
            alert(pStatusText);
        }
    });
}
