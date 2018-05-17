$(document).ready(function(){
    //数据初始化
    $.ajax({
        type: "GET",
        url: "InitBill",
        dataType: "json",
        success: function (data) {
            data.forEach(AddTr);
            InitDistribution();
            // saveInit();
            // RemovalInit();
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });
});

function AddTr(bill) {
    if (bill.flag) {
        var tbody = '<tr class="">' +
            '<td><p>' + bill.billId + '</p></td>' +
            '<td><p>' + bill.commodityName + '</p></td>' +
            '<td><p>' + bill.purchaserName + '</p></td>' +
            '<td><p>' + bill.billQuantity + '</p></td>' +
            '<td><p>' + bill.billQuantity * bill.price + '</p></td>' +
            '<td><p>' + bill.paymentDate + '</p></td>' +
            '<td><p>已完成</p></td>' +
        '</tr>';
    } else {
        var tbody = '<tr class="">' +
            '<td><p>' + bill.billId + '</p></td>' +
            '<td><p>' + bill.commodityName + '</p></td>' +
            '<td><p>' + bill.purchaserName + '</p></td>' +
            '<td><p>' + bill.billQuantity + '</p></td>' +
            '<td><p>' + bill.billQuantity * bill.price + '</p></td>' +
            '<td><p>' + bill.paymentDate + '</p></td>' +
            '<td><button class="distribution">去配送</button></td>' +
        '</tr>';
    }
    $("tbody").append(tbody);
}

function InitDistribution(){
    $(".distribution").click(function () {
        var bid = $(this).parent().prev().prev().prev().prev().prev().prev().children().text();
        $.ajax({
            type: "POST",
            url: "UpdateBill",
            data: {
                bid:bid
            },
            success: function (data) {
                alert("保存成功");
                window.location.reload();
                // saveInit();
                // RemovalInit();
            },
            error: function (jqXHR) {
                alert("发生错误：" + jqXHR.status);
            },
        });
    });
}