﻿function MyAjax(pAjaxParam) {
    $.ajax({
        "type": "get",
        "timeout": "60000",
        "url": pAjaxParam.url,
        "data": pAjaxParam.data,
        "success": function (pData, pStatusText) {
            //alert("success " + pData);
            //alert("success " + pStatusText);
            pAjaxParam.fnSuccess(pData);
        },
        "error": function (pXHR, pStatusText) {
            alert("error " + pXHR);
            alert("error " + pStatusText);
        },
        "complete": function (pXHR, pStatusText) {
            //alert("complete " + pXHR);
            //alert("complete " + pStatusText);
        }
    });
}
