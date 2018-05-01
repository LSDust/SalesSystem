$(document).ready(function () {
    //数据初始化
    $.ajax({
        type: "GET",
        url: "InitRetailer",
        dataType: "json",
        success: function (data) {
            data.forEach(AddTr);
            RemovalInit();
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });

    $("#addRetailer").click(function () {
        AddTr2();
        InserInit();
    });
});

function AddTr(retailer) {
    var tbody = '<tr>' +
        '<td><p>'+retailer.retailerId+'</p></td>' +
        '<td>' + retailer.retailerName+ '</td>' +
        '<td>' + retailer.phoneNumber + '</td>' +
        '<td>' + retailer.storeLocation + '</td>' +
        '<td><button class="delete">删除</button></td>' +
    '</tr>';
    $("#tbody").append(tbody);
}

function RemovalInit() {
    $(".delete").click(function () {
        var id = $(this).parent().prev().prev().prev().prev().children().text();
        $.ajax({
            type: "POST",
            url: "RemovalRetailer",
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

function AddTr2() {
    var tbody = "<tr class=''>" +
            "<td><p class='p' id='" + Math.floor((Math.random() * 1000)) + "'>id</p></td>" +
            "<td><input type='text' class='tb-input' value=''></td>" +
            "<td><input type='text' class='tb-input' value=''></td>" +
            "<td><input type='text' class='tb-input' value=''></td>" +
            "<td><button class='save'>保存</button></td>" +
        "</tr>";
    $("#tbody").append(tbody);
}
function InserInit() {
    $(".save").click(function () {
        var id = $(this).parent().prev().prev().prev().prev().children().attr("id");
        var name = $(this).parent().prev().prev().prev().children().val();
        var tel = $(this).parent().prev().prev().children().val();
        var address = $(this).parent().prev().children().val();
        $.ajax({
            type: "POST",
            url: "AddRetailer",
            data: {
                id: id,
                name: name,
                tel: tel,
                address: address
            },
            success: function (data) {
                if (data) {
                    alert("保存成功");
                } else {
                    alert("保存失败")
                }                
            },
            error: function (jqXHR) {
                alert("发生错误：" + jqXHR.status);
            },
        });
    });
}