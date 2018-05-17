$(document).ready(function () {
    //数据初始化
    $.ajax({
        type: "GET",
        url: "InitBill2",
        dataType: "json",
        success: function (data) {
            data.forEach(AddLi);
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });
});

function AddLi(data) {
    data.billId;
    data.retailerName;
    data.commodityName;
    data.pic;
    data.billQuantity;
    data.price;
    data.unit;
    data.paymentDate;
    data.flag;
    var a = data.price * data.billQuantity;
    if (data.flag) {
        var bill = '<li>' +
            '<div class="mc-sum-box">' +
                '<div class="myorder-sum fl"><img src=' + data.pic + '></div>' +
                '<div class="myorder-text">' +
                    '<h1>' + data.commodityName + '</h1>' +
                    '<h2>单位：' + data.unit + '</h2>' +
                    '<div class="myorder-cost">' +
                        '<span>数量:' + data.billQuantity + '</span>' +
                        '<span class="mc-t">' + data.paymentDate + '</span>' +
                    '</div>' +
                '</div>' +
            '</div>' +
            '<div class="mc-sum-Am">' +
                data.retailerName + '<span>实付：<span class="mc-t">￥' + a + '</span></span>' +
            '</div>' +
            '<h3>已完成</h3>' +
        '</li>';
        $("#Complete").append(bill);
    } else {
        var bill = '<li>' +
            '<div class="mc-sum-box">' +
                '<div class="myorder-sum fl"><img src=' + data.pic + '></div>' +
                '<div class="myorder-text">' +
                    '<h1>' + data.commodityName + '</h1>' +
                    '<h2>单位：' + data.unit + '</h2>' +
                    '<div class="myorder-cost">' +
                        '<span>数量:' + data.billQuantity + '</span>' +
                        '<span class="mc-t">' + data.paymentDate + '</span>' +
                    '</div>' +
                '</div>' +
            '</div>' +
            '<div class="mc-sum-Am">' +
                data.retailerName + '<span>实付：<span class="mc-t">￥' + a + '</span></span>' +
            '</div>' +
            '<h3>待配送</h3>' +
        '</li>';
        $("#unfinished").append(bill);
    }
}