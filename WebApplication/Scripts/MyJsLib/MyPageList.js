//一个js分页列表类,能实现以下功能：
//(1)在页面上嵌入html table
//(2)在页面上嵌入跳转页签
//(3)请求后台数据
//-------------------------------
//调用示例：
/*
    var lPageList = new MyPageList();
    lPageList.mTableDivID="divOrderList";
    lPageList.mPageIndexDivID="divPageIndexList";

    var lColumnCode = new MyPageListColumn();
    lColumnCode.mColumnName="colCode";
    lColumnCode.mColumnID="colCode";
    lColumnCode.mColumnHeaderText="编码";
    lColumnCode.mColumnDataSourceID="Code";

    lPageList.mColumns.push(lColumnCode);

    lPageList.Load();

*/

//分页 - 列表 - 列
function MyPageListColumn() {

    //列标题
    this.mColumnHeaderText = "";

    //列绑定的数据源ID
    this.mColumnDataSourceID = "";
}

//分页 - 列表
function MyPageList() {
    //页面上需嵌入列表的div 的id
    this.mTableDivID = "";

    //页面上需嵌入页码的div 的id
    this.mPageIndexDivID = "";

    //列表列定义对象
    this.mColumns = new Array();

    //请求URL
    this.mRequestUrl = "";

    //请求参数
    this.mRequestParams = new Object();

    //页面数据
    this.mPageData = new Array();

    //当前页码
    this.mPageIndex = 0;

    //记录总数
    this.mRowCount = 0;

    //每页记录数
    this.mPageSize = 10;

    //总页数
    this.mPageCount = 0;
}

//参数配置
MyPageList.prototype.Config = function (pConfigParam) {
    //页面上需嵌入列表的div 的id
    this.mTableDivID = pConfigParam.mTableDivID;

    //页面上需嵌入页码的div 的id
    this.mPageIndexDivID = pConfigParam.mPageIndexDivID;

    //列表列定义对象
    this.mColumns = pConfigParam.mColumns;

    //请求URL
    this.mRequestUrl = pConfigParam.mRequestUrl;

    //请求参数
    this.mRequestParams = pConfigParam.mRequestParams;
}

//请求数据
MyPageList.prototype.LoadData = function (pPageIndex) {
    this.mPageIndex = pPageIndex;
    var lMyPageList = this;
    var llayerIndex = 0;
    llayerIndex = layer.load();
    this.mRequestParams.begin_index = (this.mPageIndex - 1) * 10 + 1;
    this.mRequestParams.end_index = (this.mPageIndex) * 10;

    $.ajax(
        {
            "type": "get"
            ,
            "timeout": "60000"
            ,
            "url": this.mRequestUrl
            ,
            "data": this.mRequestParams
            ,
            "success": function (pData, pStatusText) {
                lMyPageList.mPageData = JSON.parse(pData);
                lMyPageList.GenerateHtml();
            }
            ,
            "error": function (pXHR, pStatusText) {

            }
            ,
            "complete": function (pXHR, pStatusText) {
                layer.close(llayerIndex);
            }
        });
}

//生成分页列表和页码HTML
MyPageList.prototype.GenerateHtml = function () {

    //生成分页列表THML
    this.GenerateHtml_PageListHtml();

    //生成页码列表HTML
    this.GenerateHtml_PageIndexHtml();
}

//生成分页列表
MyPageList.prototype.GenerateHtml_PageListHtml = function () {
    var lTableHtml = '';
    var lPageIndexHtml = '';
    var lPageTable = $("<table></table>");
    var lPageTableHead = $("<thead></thead>");
    var lPageTableHeadTr = $("<tr></tr>");
    var lPageTableHeadTrTh = $("<th></th>");
    var lPageTableBody = $("<tbody></tbody>");
    var lPageTableBodyTr = $("<tr></tr>");
    var lPageTableBodyTrTd = $("<td></td>");

    lPageTable.addClass("table table-bordered table-striped table-hover");

    lPageTable.append(lPageTableHead);
    lPageTable.append(lPageTableBody);

    lPageTableHead.append(lPageTableHeadTr);
    for (var i = 0; i < this.mColumns.length; i++) {
        lPageTableHeadTrTh = $("<th></th>");
        lPageTableHeadTrTh.text(this.mColumns[i].mColumnHeaderText);
        lPageTableHeadTr.append(lPageTableHeadTrTh);
    }

    for (var i = 0; i < this.mPageData.length; i++) {
        lPageTableBodyTr = $("<tr></tr>");
        for (var j = 0; j < this.mColumns.length; j++) {
            lPageTableBodyTrTd = $("<td></td>");
            lPageTableBodyTrTd.text(this.mPageData[i][this.mColumns[j].mColumnDataSourceID]);
            lPageTableBodyTr.append(lPageTableBodyTrTd);
        }
        lPageTableBody.append(lPageTableBodyTr);
    }

    $('#' + this.mTableDivID).append(lPageTable);
}

MyPageList.prototype.GenerateHtml_PageIndexHtml = function () {
    if (this.mPageData.length > 0) {
        //返回的代表每条数据的JSON对象中，必须包括RowCnt成员！！！！！
        this.mRowCount = this.mPageData[0].RowCnt;
        this.mPageCount = Math.ceil(this.mRowCount / this.mPageSize);

        var lPageIndexNav = $("<nav></nav>");
        var lPageIndexNav_Ul = $("<ul></ul>");
        var lPageIndexNav_Ul_Li = $("<li></li>");
        var lPageIndexNav_Ul_Li_A = $("<a></a>");

        lPageIndexNav_Ul.addClass("pagination");

        lPageIndexNav_Ul_Li = $("<li></li>");
        lPageIndexNav_Ul_Li_A = $("<a></a>");
        if (this.mPageIndex == 1) {
            lPageIndexNav_Ul_Li.addClass("disabled");
        }
        else {
            lPageIndexNav_Ul_Li_A.on("click", null, { "pageList": this, "pageIndex": this.mPageIndex - 1}, this.PageIndexJump);
        }
        lPageIndexNav_Ul_Li_A.attr("href", "#");
        lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
        lPageIndexNav_Ul_Li_A.text("上一页");
        lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);


        if (this.mPageIndex - 1 > 5) {
            lPageIndexNav_Ul_Li = $("<li></li>");
            lPageIndexNav_Ul_Li_A = $("<a></a>");
            lPageIndexNav_Ul_Li_A.attr("href", "#");
            lPageIndexNav_Ul_Li_A.on("click", null, this.mPageIndex - 4, myPageList.LoadData);
            lPageIndexNav_Ul_Li_A.text("...");
            lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
            lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

            lPageIndexNav_Ul_Li = $("<li></li>");
            lPageIndexNav_Ul_Li_A = $("<a></a>");
            lPageIndexNav_Ul_Li_A.attr("href", "#");
            lPageIndexNav_Ul_Li_A.on("click", null, this.mPageIndex - 2, myPageList.LoadData);
            lPageIndexNav_Ul_Li_A.text(this.mPageIndex - 2);
            lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
            lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

            lPageIndexNav_Ul_Li = $("<li></li>");
            lPageIndexNav_Ul_Li_A = $("<a></a>");
            lPageIndexNav_Ul_Li_A.attr("href", "#");
            lPageIndexNav_Ul_Li_A.on("click", null, this.mPageIndex - 1, myPageList.LoadData);
            lPageIndexNav_Ul_Li_A.text(this.mPageIndex - 1);
            lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
            lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

        }
        else {
            for (var i = 1; i < this.mPageIndex; i++) {
                lPageIndexNav_Ul_Li = $("<li></li>");
                lPageIndexNav_Ul_Li_A = $("<a></a>");
                lPageIndexNav_Ul_Li_A.attr("href", "#");
                lPageIndexNav_Ul_Li_A.on("click", null, i, myPageList.LoadData);
                lPageIndexNav_Ul_Li_A.text(i);
                lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
                lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);
            }
        }

        lPageIndexNav_Ul_Li = $("<li></li>");
        lPageIndexNav_Ul_Li.addClass("active");
        lPageIndexNav_Ul_Li_A = $("<a></a>");
        lPageIndexNav_Ul_Li_A.attr("href", "#");
        lPageIndexNav_Ul_Li_A.on("click", null, this.mPageIndex, myPageList.LoadData);
        lPageIndexNav_Ul_Li_A.text(this.mPageIndex);
        lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
        lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

        if (this.mPageCount - this.mPageIndex > 5) {
            lPageIndexNav_Ul_Li = $("<li></li>");
            lPageIndexNav_Ul_Li_A = $("<a></a>");
            lPageIndexNav_Ul_Li_A.attr("href", "#");
            lPageIndexNav_Ul_Li_A.on("click", null, { "pageList": this, "pageIndex": this.mPageIndex - 1 }, this.PageIndexJump);
            lPageIndexNav_Ul_Li_A.text(this.mPageIndex + 1);
            lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
            lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

            lPageIndexNav_Ul_Li = $("<li></li>");
            lPageIndexNav_Ul_Li_A = $("<a></a>");
            lPageIndexNav_Ul_Li_A.attr("href", "#");
            lPageIndexNav_Ul_Li_A.on("click", null, this.mPageIndex + 2, myPageList.LoadData);
            lPageIndexNav_Ul_Li_A.text(this.mPageIndex + 2);
            lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
            lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

            lPageIndexNav_Ul_Li = $("<li></li>");
            lPageIndexNav_Ul_Li_A = $("<a></a>");
            lPageIndexNav_Ul_Li_A.attr("href", "#");
            lPageIndexNav_Ul_Li_A.on("click", null, this.mPageIndex + 4, myPageList.LoadData);
            lPageIndexNav_Ul_Li_A.text("...");
            lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
            lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

        }
        else {
            for (var i = this.mPageIndex + 1; i <= this.mPageCount; i++) {
                lPageIndexNav_Ul_Li = $("<li></li>");
                lPageIndexNav_Ul_Li_A = $("<a></a>");
                lPageIndexNav_Ul_Li_A.attr("href", "#");
                lPageIndexNav_Ul_Li_A.on("click", null, i, this.LoadData);
                lPageIndexNav_Ul_Li_A.text(i);
                lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
                lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);
            }
        }

        lPageIndexNav_Ul_Li = $("<li></li>");
        lPageIndexNav_Ul_Li_A = $("<a></a>");
        if (this.mPageIndex == this.mPageCount) {
            lPageIndexNav_Ul_Li.addClass("disabled");
        }
        else {
            lPageIndexNav_Ul_Li_A.on("click", null, this.mPageIndex + 1, myPageList.LoadData);
        }
        lPageIndexNav_Ul_Li_A.attr("href", "#");
        lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
        lPageIndexNav_Ul_Li_A.text("下一页");
        lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

        lPageIndexNav_Ul_Li = $("<li></li>");
        lPageIndexNav_Ul_Li_A = $("<a></a>");
        lPageIndexNav_Ul_Li.addClass("disabled");
        lPageIndexNav_Ul_Li_A.attr("href", "#");
        lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
        lPageIndexNav_Ul_Li_A.text("共" + this.mRowCount + "条数据，" + myPageList.mPageCount + "页");
        lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);
    }
    lPageIndexNav.append(lPageIndexNav_Ul);

    $('#' + this.mPageIndexDivID).append(lPageIndexNav);
}

MyPageList.prototype.PageIndexJump = function (pData) {
    var lPageList = pData.data.pageList;
    var lPageIndex = pData.data.pageIndex;
    debugger;
    lPageList.LoadData(lPageIndex);

}
var myPageList = new MyPageList();