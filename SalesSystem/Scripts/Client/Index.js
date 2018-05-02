﻿$(document).ready(function () {
    //数据初始化
    $.ajax({
        type: "GET",
        url: "InitRetailer",
        dataType: "json",
        success: function (data) {
            data.forEach(AddLi);
            Inita();
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });
});

function AddLi(retailer, index) {
    var i = index + 1;
    var li = '<li class="line">' +
        '<a href="ProductList">' +
            '<div class="pro-img"><img src="../../Image/Retailer/Head/00'+i+'.jpg" alt=""></div>' +
            '<div class="pro-con">' +
                '<h3>'+retailer.retailerName+'</h3>' +
                '<p>月销' + retailer.sales + '笔</p>' +
                '<b style="margin-top:0.6rem;color:#000;">' + retailer.storeLocation + '</b>' +
            '</div>' +
        '</a>' +
    '</li>';
    $("ol:last").append(li);
}