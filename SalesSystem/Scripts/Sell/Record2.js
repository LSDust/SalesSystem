$(document).ready(function () {
    //数据初始化
    $.ajax({
        type: "GET",
        url: "InitBill2",
        dataType: "json",
        success: function (data) {
            data.forEach(AddTr);
            // saveInit();
            // RemovalInit();
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });
});

function AddTr(bill) {
    var tbody = '<tr class="">' +
        '<td><p>' + bill.billId + '</p></td>' +
        '<td><p>' + bill.commodityName + '</p></td>' +
        '<td><p>' + bill.billQuantity + '</p></td>' +
        '<td><p>' + bill.billQuantity * bill.price + '</p></td>' +
        '<td><p>' + bill.paymentDate + '</p></td>' +
    '</tr>';
    $("tbody").append(tbody);
}