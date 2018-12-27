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
MyPageList.prototype.Config = function (pConfigParam)
{
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
    var lTableHtml = '';
    var lPageIndexHtml = '';

    lTableHtml += '<table class="table table-bordered table-striped table-hover">';
    lTableHtml += '<thead>';
    lTableHtml += '<tr>';
    for (var i = 0; i < this.mColumns.length; i++) {
        lTableHtml += '<th>';
        lTableHtml += this.mColumns[i].mColumnHeaderText;
        lTableHtml += '</th>';
    }
    lTableHtml += '</tr>';
    lTableHtml += '</thead>';
    lTableHtml += '<tbody>';

    for (var i = 0; i < this.mPageData.length; i++) {
        lTableHtml += '<tr>';
        for (var j = 0; j < this.mColumns.length; j++) {
            lTableHtml += '<td>';
            lTableHtml += this.mPageData[i][this.mColumns[j].mColumnDataSourceID];
            lTableHtml += '</td>';
        }
        lTableHtml += '</tr>';
    }

    lTableHtml += '</tbody>';
    lTableHtml += '</table>';
    $('#' + this.mTableDivID).html(lTableHtml);

    if (this.mPageData.length > 0) {
        //返回的代表每条数据的JSON对象中，必须包括RowCnt成员！！！！！
        this.mRowCount = this.mPageData[0].RowCnt;
        this.mPageCount = Math.ceil(this.mRowCount / this.mPageSize);

        lPageIndexHtml += '<nav>';
        lPageIndexHtml += '<ul class="pagination">';

        if (this.mPageIndex == 1) {
            lPageIndexHtml += '<li class="disabled"><a href="#">上一页</li>';
        }
        else {
            lPageIndexHtml += '<li><a href="#" onclick="myPageList.LoadData(' + (this.mPageIndex - 1) + ')">上一页</li>';
        }

        if (this.mPageIndex - 1 > 5) {
            lPageIndexHtml += '<li><a href="#" onclick="myPageList.LoadData(' + (this.mPageIndex - 4) + ')">...</li>';
            lPageIndexHtml += '<li><a href="#" onclick="myPageList.LoadData(' + (this.mPageIndex - 2) + ')">' + (this.mPageIndex - 2) + '</li>';
            lPageIndexHtml += '<li><a href="#" onclick="myPageList.LoadData(' + (this.mPageIndex - 1) + ')">' + (this.mPageIndex - 1) + '</li>';
        }
        else {
            for (var i = 1; i < this.mPageIndex; i++) {
                lPageIndexHtml += '<li><a href="#" onclick="myPageList.LoadData(' + i + ')">' + i + '</li>';
            }
        }

        lPageIndexHtml += '<li class="active"><a href="#" onclick="myPageList.LoadData(' + this.mPageIndex + ')">' + this.mPageIndex + '</li>';

        if (this.mPageCount - this.mPageIndex > 5) {
            lPageIndexHtml += '<li><a href="#" onclick="myPageList.LoadData(' + (this.mPageIndex + 1) + ')">' + (this.mPageIndex + 1) + '</li>';
            lPageIndexHtml += '<li><a href="#" onclick="myPageList.LoadData(' + (this.mPageIndex + 2) + ')">' + (this.mPageIndex + 2) + '</li>';
            lPageIndexHtml += '<li><a href="#" onclick="myPageList.LoadData(' + (this.mPageIndex + 4) + ')">...</li>';
        }
        else {
            for (var i = this.mPageIndex + 1; i <= this.mPageCount; i++) {
                lPageIndexHtml += '<li><a href="#" onclick="myPageList.LoadData(' + i + ')">' + i + '</li>';
            }
        }

        if (this.mPageIndex == this.mPageCount) {
            lPageIndexHtml += '<li class="disabled"><a href="#">下一页</li>';
        }
        else {
            lPageIndexHtml += '<li><a href="#" onclick="myPageList.LoadData(' + (this.mPageIndex + 1) + ')">下一页</li>';
        }
        lPageIndexHtml += "<li class='disabled'><a href='#'>共" + this.mRowCount + "条数据，" + this.mPageCount + "页</a></li>";
        lPageIndexHtml += '</ul>';
        lPageIndexHtml += '</nav>';
    }

    $('#' + this.mPageIndexDivID).html(lPageIndexHtml);
}

var myPageList = new MyPageList();