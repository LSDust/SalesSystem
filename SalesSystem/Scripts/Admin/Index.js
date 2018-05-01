$(document).ready(function () {
    //数据初始化
    $.ajax({
        type: "GET",
        url: "InitPurchaser",
        dataType: "json",
        success: function (data) {
            data.forEach(AddTr);
            RemovalInit();
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });
});

function AddTr(purchaser) {
    var tbody = '<tr class="">' +
        '<td><p>'+purchaser.purchaserId+'</p></td>' +
        '<td><p>'+purchaser.purchaserName+'</p></td>' +
        '<td>' + purchaser.phoneNumber + '</td>' +
        '<td><button class="delete">删除</button></td>' +
    '</tr>';
    $("#tbody").append(tbody);
}

function RemovalInit() {
    $(".delete").click(function () {
        var id = $(this).parent().prev().prev().prev().children().text();
        alert(id);
        $.ajax({
            type: "POST",
            url: "RemovalPurchaser",
            data: {
                id: id,
            },
            success: function (data) {
                if (data) {
                    alert("删除成功");
                }
                location.reload(false);
            },
            error: function (jqXHR) {
                alert("发生错误：" + jqXHR.status);
            },
        });
    });
}