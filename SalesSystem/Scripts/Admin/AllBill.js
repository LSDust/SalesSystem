$(document).ready(function () {
    //数据初始化
    $.ajax({
        type: "GET",
        url: "InitAllBill",
        dataType: "json",
        success: function (data) {
            data.forEach(AddTr);
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });
});

function AddTr(bill) {
    bill.commodityName;
    bill.inventoryQuantity;
    var tbody = '<tr>' +
        '<td>' + bill.billId + '</td>' +
        '<td>' + bill.retailerName + '</td>' +
        '<td>' + bill.purchaserName + '</td>' +
        '<td>' + bill.commodityName + '</td>' +
        '<td>' + bill.billQuantity + '</td>' +
        '<td>' + bill.billQuantity * bill.price + '</td>' +
        '<td>' + bill.paymentDate + '</td>' +
    '</tr>';
    $("tbody").append(tbody);
}
