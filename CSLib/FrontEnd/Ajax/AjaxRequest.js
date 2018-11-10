var AjaxRequest1 = {
    url: ""
    ,
    type: ""
    ,
    success: function () { }
    ,
    error: function () { }
    ,
    complete: function () { }
    ,
    StartRequest: function () {
        $.ajax({
            type: this.type
            ,
            url: this.url
            ,
            timeout: 1000 * 60
            ,
            success: function (pData) {
                this.success(pData);
            }
            ,
            error: function (pXHR) {
                this.error(pXHR);
            }
            ,
            complete: function (pXHR) {
                this.complete(pXHR);
            }
        });
    }
}