$(document).ready(function () {
    //数据初始化
    $.ajax({
        type: "GET",
        url: "InitCommodity",
        dataType: "json",
        success: function (data) {
            data.forEach(AddComdty)
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });
    //添加商品按钮
    $("#addcomdty").click(function () {
        var tbody = "<tr class=''>" +
            "<td></td>" +
            "<td><input type='text' class='tb-input' placeholder=></td>" +
            "<td><input name='imgfile' type='file' accept='image/jpg, image/jpeg' /></td>" +
            "<td><input type='text' class='tb-input' placeholder=>元</td>" +
            "<td><input type='text' class='tb-input' placeholder=>元</td>" +
            "<td class='sd'><button class='bt-sm btn btn-default pull-right'>保存</button></td>" +
            "<td class='sd'><button class='bt-sm btn btn-default pull-right'>删除</button></td>" +
            "</tr>";
        $("#tbody").append(tbody);
    });
    //保存按钮
    $("#save").click(function () {
        $.ajax({
            type: "POST",
            url: "AddCommodity",
            data: {
                cname: $(".commdityName").val(),
                pic: $(".commdityPic").val(),
                cost: $(".primeCost").val(),
                price: $(".sellingPrice").val(),
                unit: $(".unit").val()
            },
            success: function (data) {
                alert("保存成功");
            },
            error: function (jqXHR) {
                alert("发生错误：" + jqXHR.status);
            },
        });
    });
});

function AddComdty(comdty) {
    
    var tbody = "<tr class=''>"+
    "<td>"+comdty.commodityId+"</td>"+
    "<td><input type='text' class='tb-input name' placeholder=" + comdty.commodityName + "></td>" +
    "<td><input name='imgfile' type='file' accept='image/jpg, image/jpeg' /></td>"+
    "<td><input type='text' class='tb-input' placeholder=" + comdty.primeCost + ">元</td>" +
    "<td><input type='text' class='tb-input' placeholder=" + comdty.sellingPrice + ">元</td>" +
    "<td class='sd'><button class='bt-sm btn btn-default pull-right' onclick='func1()'>保存</button></td>" +
    "<td class='sd'><button class='bt-sm btn btn-default pull-right'>删除</button></td>"+
    "</tr>";
    $("#tbody").append(tbody);
}

function func1() {
    var a = $("table").rows[0].innerHTML;   //断点
    alert(a);
}