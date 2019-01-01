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
function PageTableColumn() {

    //列标题
    this.mColumnHeaderText = "";

    //列绑定的数据源ID
    this.mColumnDataSourceID = "";
}

function PageTableButtonColumn() {
    //按钮标题
    this.mButtonCaption = "";

    //单击事件处理程序
    this.mClickEventHandler = new Object();
}

PageTableButtonColumn.prototype = PageTableColumn;


//分页 - 表格
function PageTable(pConfigParam) {
    //页面上需嵌入列表的div 的id
    this.mTableDivID = pConfigParam.mTableDivID;

    //列表列定义对象
    this.mColumns = pConfigParam.mColumns;

    //请求URL
    this.mRequestUrl = pConfigParam.mRequestUrl;

    //请求参数
    this.mRequestParams = pConfigParam.mRequestParams;

    //页面数据
    this.mPageData = new Array();

    //分页对象
    this.mPageIndex = new PageIndex();
    this.mPageIndex.mPageTable = this;
    //页面上需嵌入页码的div 的id
    this.mPageIndex.mPageIndexDivID = pConfigParam.mPageIndexDivID;
}

//分页 - 页码
function PageIndex() {

    this.mPageIndexDivID = "";

    //当前页码
    this.mCurrentIndex = 0;

    //记录总数
    this.mRowCount = 0;

    //每页记录数
    this.mPageSize = 10;

    //总页数
    this.mPageCount = 0;

    //表格对象
    this.mPageTable = new Object();
}

//请求数据
PageTable.prototype.LoadData = function (pIndex) {
    this.mPageIndex.mCurrentIndex = pIndex;
    var lPageTable = this;
    var llayerIndex = 0;
    llayerIndex = layer.load();
    this.mRequestParams.begin_index = (this.mPageIndex.mCurrentIndex - 1) * 10 + 1;
    this.mRequestParams.end_index = (this.mPageIndex.mCurrentIndex) * 10;

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
                lPageTable.mPageData = JSON.parse(pData);
                lPageTable.GenerateHtml();
                lPageTable.mPageIndex.GenerateHtml();
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

//生成分页列表
PageTable.prototype.GenerateHtml = function () {
    var lPageTable = $("<table></table>");
    var lPageTableHead = $("<thead></thead>");
    var lPageTableHeadTr = $("<tr></tr>");
    var lPageTableHeadTrTh = $("<th></th>");
    var lPageTableBody = $("<tbody></tbody>");
    var lPageTableBodyTr = $("<tr></tr>");
    var lPageTableBodyTrTd = $("<td></td>");

    $('#' + this.mTableDivID).html("");

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
            if (PageTableColumn.prototype.isPrototypeOf(this.mColumns[j])) {
                lPageTableBodyTrTd.text(this.mPageData[i][this.mColumns[j].mColumnDataSourceID]);
            }
            else if (PageTableButtonColumn.prototype.isPrototypeOf(this.mColumns[j])) {
                var lBtn = $("<input type='button'/>");
                lBtn.addClass("btn btn-info");
                lBtn.attr("value", this.mColumns[j].mButtonCaption);
                lBtn.on("click", null, {"RowData":this.mPageData[i]}, this.mColumns[j].mClickEventHandler);
                lPageTableBodyTrTd.append(lBtn);
            }

            lPageTableBodyTr.append(lPageTableBodyTrTd);
        }
        lPageTableBody.append(lPageTableBodyTr);
    }

    $('#' + this.mTableDivID).append(lPageTable);
}

PageIndex.prototype.GenerateHtml = function () {
    $('#' + this.mPageIndexDivID).html("");
    if (this.mPageTable.mPageData.length > 0) {

        //返回的代表每条数据的JSON对象中，必须包括RowCnt成员！！！！！
        this.mRowCount = this.mPageTable.mPageData[0].RowCnt;

        this.mPageCount = Math.ceil(this.mRowCount / this.mPageSize);
        var lPageIndexNav = $("<nav></nav>");
        var lPageIndexNav_Ul = $("<ul></ul>");
        var lPageIndexNav_Ul_Li = $("<li></li>");
        var lPageIndexNav_Ul_Li_A = $("<a></a>");

        lPageIndexNav_Ul.addClass("pagination");

        lPageIndexNav_Ul_Li = $("<li></li>");
        lPageIndexNav_Ul_Li_A = $("<a></a>");
        if (this.mCurrentIndex == 1) {
            lPageIndexNav_Ul_Li.addClass("disabled");
        }
        else {
            lPageIndexNav_Ul_Li_A.on("click", null, { "pageIndex": this, "index": this.mCurrentIndex - 1 }, this.PageIndexJump);
        }
        lPageIndexNav_Ul_Li_A.attr("href", "#");
        lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
        lPageIndexNav_Ul_Li_A.text("上一页");
        lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);


        if (this.mCurrentIndex - 1 > 5) {
            lPageIndexNav_Ul_Li = $("<li></li>");
            lPageIndexNav_Ul_Li_A = $("<a></a>");
            lPageIndexNav_Ul_Li_A.attr("href", "#");
            lPageIndexNav_Ul_Li_A.on("click", null, { "pageIndex": this, "index": this.mCurrentIndex - 4 }, this.PageIndexJump);
            lPageIndexNav_Ul_Li_A.text("...");
            lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
            lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

            lPageIndexNav_Ul_Li = $("<li></li>");
            lPageIndexNav_Ul_Li_A = $("<a></a>");
            lPageIndexNav_Ul_Li_A.attr("href", "#");
            lPageIndexNav_Ul_Li_A.on("click", null, { "pageIndex": this, "index": this.mCurrentIndex - 2 }, this.PageIndexJump);
            lPageIndexNav_Ul_Li_A.text(this.mCurrentIndex - 2);
            lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
            lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

            lPageIndexNav_Ul_Li = $("<li></li>");
            lPageIndexNav_Ul_Li_A = $("<a></a>");
            lPageIndexNav_Ul_Li_A.attr("href", "#");
            lPageIndexNav_Ul_Li_A.on("click", null, { "pageIndex": this, "index": this.mCurrentIndex - 1 }, this.PageIndexJump);
            lPageIndexNav_Ul_Li_A.text(this.mCurrentIndex - 1);
            lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
            lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

        }
        else {
            for (var i = 1; i < this.mCurrentIndex; i++) {
                lPageIndexNav_Ul_Li = $("<li></li>");
                lPageIndexNav_Ul_Li_A = $("<a></a>");
                lPageIndexNav_Ul_Li_A.attr("href", "#");
                lPageIndexNav_Ul_Li_A.on("click", null, { "pageIndex": this, "index": i }, this.PageIndexJump);
                lPageIndexNav_Ul_Li_A.text(i);
                lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
                lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);
            }
        }

        lPageIndexNav_Ul_Li = $("<li></li>");
        lPageIndexNav_Ul_Li.addClass("active");
        lPageIndexNav_Ul_Li_A = $("<a></a>");
        lPageIndexNav_Ul_Li_A.attr("href", "#");
        lPageIndexNav_Ul_Li_A.on("click", null, { "pageIndex": this, "index": this.mCurrentIndex }, this.PageIndexJump);
        lPageIndexNav_Ul_Li_A.text(this.mCurrentIndex);
        lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
        lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

        if (this.mPageCount - this.mCurrentIndex > 5) {
            lPageIndexNav_Ul_Li = $("<li></li>");
            lPageIndexNav_Ul_Li_A = $("<a></a>");
            lPageIndexNav_Ul_Li_A.attr("href", "#");
            lPageIndexNav_Ul_Li_A.on("click", null, { "pageIndex": this, "index": this.mCurrentIndex + 1 }, this.PageIndexJump);
            lPageIndexNav_Ul_Li_A.text(this.mCurrentIndex + 1);
            lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
            lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

            lPageIndexNav_Ul_Li = $("<li></li>");
            lPageIndexNav_Ul_Li_A = $("<a></a>");
            lPageIndexNav_Ul_Li_A.attr("href", "#");
            lPageIndexNav_Ul_Li_A.on("click", null, { "pageIndex": this, "index": this.mCurrentIndex + 2 }, this.PageIndexJump);
            lPageIndexNav_Ul_Li_A.text(this.mCurrentIndex + 2);
            lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
            lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

            lPageIndexNav_Ul_Li = $("<li></li>");
            lPageIndexNav_Ul_Li_A = $("<a></a>");
            lPageIndexNav_Ul_Li_A.attr("href", "#");
            lPageIndexNav_Ul_Li_A.on("click", null, { "pageIndex": this, "index": this.mCurrentIndex + 4 }, this.PageIndexJump);
            lPageIndexNav_Ul_Li_A.text("...");
            lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
            lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);

        }
        else {
            for (var i = this.mCurrentIndex + 1; i <= this.mPageCount; i++) {
                lPageIndexNav_Ul_Li = $("<li></li>");
                lPageIndexNav_Ul_Li_A = $("<a></a>");
                lPageIndexNav_Ul_Li_A.attr("href", "#");
                lPageIndexNav_Ul_Li_A.on("click", null, { "pageIndex": this, "index": i }, this.PageIndexJump);
                lPageIndexNav_Ul_Li_A.text(i);
                lPageIndexNav_Ul_Li.append(lPageIndexNav_Ul_Li_A);
                lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);
            }
        }

        lPageIndexNav_Ul_Li = $("<li></li>");
        lPageIndexNav_Ul_Li_A = $("<a></a>");
        if (this.mCurrentIndex == this.mPageCount) {
            lPageIndexNav_Ul_Li.addClass("disabled");
        }
        else {
            lPageIndexNav_Ul_Li_A.on("click", null, { "pageIndex": this, "index": this.mCurrentIndex + 1 }, this.PageIndexJump);
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
        lPageIndexNav_Ul_Li_A.text("共" + this.mRowCount + "条数据，" + this.mPageCount + "页");
        lPageIndexNav_Ul.append(lPageIndexNav_Ul_Li);
    }
    lPageIndexNav.append(lPageIndexNav_Ul);

    $('#' + this.mPageIndexDivID).append(lPageIndexNav);
}

PageIndex.prototype.PageIndexJump = function (pData) {
    var lPageIndex = pData.data.pageIndex;
    var lIndex = pData.data.index;
    lPageIndex.mPageTable.LoadData(lIndex);
}