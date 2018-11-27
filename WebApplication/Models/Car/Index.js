function Index() {
    this.CarList = new Array();
}

Index.prototype.GenerateData = function () {
    var lCar = new Car();
    lCar.CarNO = "998963";
    this.CarList.push(lCar);

    lCar = new Car();
    lCar.CarNO = "98983";
    this.CarList.push(lCar);

    lCar = new Car();
    lCar.CarNO = "98967";
    this.CarList.push(lCar);

    lCar = new Car();
    lCar.CarNO = "98961";
    this.CarList.push(lCar);

    lCar = new Car();
    lCar.CarNO = "98957";
    this.CarList.push(lCar);

    lCar = new Car();
    lCar.CarNO = "8689";
    this.CarList.push(lCar);

    lCar = new Car();
    lCar.CarNO = "84595";
    this.CarList.push(lCar);

    lCar = new Car();
    lCar.CarNO = "8359";
    this.CarList.push(lCar);

    lCar = new Car();
    lCar.CarNO = "8356";
    this.CarList.push(lCar);

    lCar = new Car();
    lCar.CarNO = "8315";
    this.CarList.push(lCar);

    lCar = new Car();
    lCar.CarNO = "8153";
    this.CarList.push(lCar);
}


$(function () {

    var lIndex = new Index();
    var lPaging = new Paging();

    lIndex.GenerateData();

    if (lIndex.CarList.length > 0) {
        for (var i = 0; i < lPaging.Paging_Size; i++) {
            if (i < lIndex.CarList.length) {
                $("#CarListBody").append('<tr><td>' + (i + 1) + '</td><td>' + lIndex.CarList[i].CarNO + '</td></tr>');
            }
            else {
                $("#CarListBody").append('<tr><td></td><td></td></tr>');
            }
        }
    }

    //设置【分页】页码

    //取得页数
    var lPages = Math.ceil(lIndex.CarList.length / lPaging.Paging_Size);

    if (lPages == 0) {
        //如果页数=0，隐藏分页
        $("#Pages").css("display", "none");
    } else {
        //否则展示页码
        $("#Pages").html('');
        for (var i = 1; i <= lPages; i++)
            $("#Pages").append('<li class="page-item"><a class="page-link" href="#">' + i + '</a></li>');
    }

});