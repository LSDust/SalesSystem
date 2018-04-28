$(document).ready(function () {
    //数据初始化
    $.ajax({
        type: "GET",
        url: "InitCommodity",
        dataType: "json",
        success: function (data) {
            data.forEach(AddTr);
            saveInit();
            RemovalInit();
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });

    //添加商品按钮
    $("#addcomdty").click(function () {
        AddTr2();
        InserInit();
    });
});

function AddTr(comdty) {
    
    var tbody = "<tr class="+comdty.commodityId+">"+
    "<td><p>" + comdty.commodityId + "</p></td>" +
    "<td><input type='text' class='tb-input name' value=" + comdty.commodityName + "></td>" +
    "<td><a href='javascript:;' class='file'>选择图片<input type='file' name='' id=''></a></td>"+
    "<td><input type='text' class='tb-input' value=" + comdty.primeCost + ">元</td>" +
    "<td><input type='text' class='tb-input' value=" + comdty.sellingPrice + ">元</td>" +
    "<td><input type='text' class='tb-input' value=" + comdty.unit + "></td>" +
    "<td class='sd'><button class='bt-sm btn btn-default pull-right save' id="+ comdty.commodityId +">保存</button></td>" +
    "<td class='sd'><button class='bt-sm btn btn-default pull-right delete'>删除</button></td>"+
    "</tr>";
    $("#tbody").append(tbody);
}

function saveInit() {
    $(".save").click(function () {
        var id = $(this).attr("id");
        $.ajax({
            type: "POST",
            url: "SaveCommodity",
            data: {
                id:id,
                cname: $("." + id).children().next().children().val(),
                cost: $("." + id).children().next().next().next().children().val(),
                price: $("." + id).children().next().next().next().next().children().val(),
                unit: $("." + id).children().next().next().next().next().next().children().val()
            },
            success: function (data) {
                alert("保存成功");
            },
            error: function (jqXHR) {
                alert("发生错误：" + jqXHR.status);
            },
        });
    });
}

function AddTr2(){
    var tbody = "<tr class=''>" +
        "<td><p class='p'>"+Math.floor((Math.random()*1000))+"</p></td>" +
        "<td><input type='text' class='tb-input name' value=''></td>" +
        "<td><a href='javascript:;' class='file'>选择图片<input type='file' name='' id=''></a></td>"+
        "<td><input type='text' class='tb-input' value=''>元</td>" +
        "<td><input type='text' class='tb-input' value=''>元</td>" +
        "<td><input type='text' class='tb-input' value=''></td>" +
        "<td class='sd'><button class='bt-sm btn btn-default pull-right save' id='inser'>保存</button></td>" +
        "<td class='sd'><button class='bt-sm btn btn-default pull-right delete'>删除</button></td>"+
        "</tr>";
        $("#tbody").append(tbody);
}

function InserInit(){
    //保存按钮
    $("#inser").click(function () {
        var id = $("#inser").parent().parent().children().children().html();
        $.ajax({
            type: "POST",
            url: "AddCommodity",
            data: {
                id:id,
                cname: $("#inser").parent().parent().children().next().children().val(),
                pic: null,
                cost: $("#inser").parent().parent().children().next().next().next().children().val(),
                price: $("#inser").parent().parent().children().next().next().next().next().children().val(),
                unit: $("#inser").parent().parent().children().next().next().next().next().next().children().val()
            },
            success: function (data) {
                alert("保存成功");
                $("#inser").attr("id",id);
                RemovalInit();
            },
            error: function (jqXHR) {
                alert("发生错误：" + jqXHR.status);
            },
        });
    });
}

function RemovalInit(){
    $(".delete").click(function(){
        var id = $(this).parent().parent().children().children().html();
        $.ajax({
            type: "POST",
            url: "RemovalCommodity",
            data: {
                id:id,
            },
            success: function (data) {
                if(data){
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