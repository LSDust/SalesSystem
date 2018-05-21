$(document).ready(function(){
    //数据初始化
    $.ajax({
        type: "GET",
        url: "InitPorductList",
        data:{
            id:$_GET['id']
        },
        dataType: "json",
        success: function (data) {
            data.forEach(AddLi);
            InitButton();
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });

    $.ajax({
        type: "GET",
        url: "ThisRetailer",
        data: {
            id: $_GET['id']
        },
        dataType: "json",
        success: function (data) {
            InitRetailer(data);
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });

    $("#count").click(function () {
        $("li").each(function () {
            var quantity = $(this).children().next().children().next().val();
            var cname = $(this).children().children().next().children().children().text();
            if (quantity > 0) {
                $.ajax({
                    type: "POST",
                    url: "AddBill2",
                    data: {
                        id: $_GET['id'],
                        cname: cname,
                        quantity: quantity
                    },
                    success: function (data) {
                        alert("保存成功");
                    },
                    error: function (jqXHR) {
                        alert("保存成功：" + jqXHR.status);
                    },
                });
            }
        });
    });
});

var $_GET = (function () {
    var url = window.document.location.href.toString();
    var u = url.split("?");
    if (typeof (u[1]) == "string") {
        u = u[1].split("&");
        var get = {};
        for (var i in u) {
            var j = u[i].split("=");
            get[j[0]] = j[1];
        }
        return get;
    } else {
        return {};
    }
})();

function AddLi(porduct) {
    porduct.commodityName;
    porduct.commodityPic;
    porduct.commodityPrice;
    porduct.commodityUnit;
    porduct.frequency;
    porduct.quantity;
    var li = 
    '<li class="line">'+
        '<a href="#">'+
            '<div class="pro-img"><img src="'+porduct.commodityPic+'" alt=""></div>'+
            '<div class="pro-con">'+
                '<span><h3>'+porduct.commodityName+'</h3></span>'+
                '<p>库存：' + porduct.quantity + '</p>' +
                '<b style="margin-top:0.9rem">￥'+porduct.commodityPrice +'  /'+ porduct.commodityUnit +'</b>'+
            '</div>'+
        '</a>'+
        '<div class="D-BuyNum">'+
            '<button class="decrease" style="">'+
                '<i class="icon-reduce"></i>'+
            '</button>'+
            '<input id = ' + porduct.quantity + ' style="" type="number" class="line amount" value="0">' +
            '<button class = "addition">'+
                '<i class="icon-plus"></i>'+
            '</button>'+
        '</div>'+
    '</li>';
    $("ol:last").append(li);
    // $(".amount").hide();
    // $(".decrease").hide();
}

function InitRetailer(data) {
    //data.retailerId;
    //data.retailerName;
    //data.retailerPic;
    //data.phoneNumber;
    //data.storeLocation;
    $("#img").attr("src", data.retailerPic)
    $("#retailer").text(data.retailerName);
    $("#add").text(data.storeLocation);
    $("#tel").text(data.phoneNumber);
}

function InitButton(){
    $(".addition").click(function(){
        // console.log($(this).prev().val());
        // $(this).prev().show();
        var a = $(this).prev().val();
        var quantity = $(this).prev().attr("id");
        if (a < quantity) {
            $(this).prev().val(parseInt(a) + 1);
            Count();
        }
    });
    $(".decrease").click(function(){
        var b = $(this).next().val();
        if(b > 0){
            $(this).next().val(b-1);
            Count();
        }
    });
} 

function Count(){
    var li = $("#list-content").children("li").length;
    var sum = 0;
    for(i = 1;i<=li&&i>0;i++){
        var count = parseInt($('li:eq('+ i +')').children().children().next().children().next().next().html().replace(/[^0-9]/ig,""));
        var quantity = $('li:eq('+ i +')').children().next().children().next().val();
        sum += Number(count)*quantity;
    }
    $("#count").text("合计：￥" + sum + "元");
}