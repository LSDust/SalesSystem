$(document).ready(function(){
    //数据初始化
    $.ajax({
        type: "GET",
        url: "InitPorductList",
        dataType: "json",
        success: function (data) {
            data.forEach(AddLi);
            InitButton();
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });
});

function AddLi(porduct){
    porduct.commodityName;
    porduct.commodityPic;
    porduct.commodityPrice;
    porduct.commodityUnit;
    porduct.frequency;
    var li = 
    '<li class="line">'+
        '<a href="#">'+
            '<div class="pro-img"><img src="'+porduct.commodityPic+'" alt=""></div>'+
            '<div class="pro-con">'+
                '<h3>'+porduct.commodityName+'</h3>'+
                '<p>月销'+porduct.frequency+'笔</p>'+
                '<b style="margin-top:0.9rem">￥'+porduct.commodityPrice +'  /'+ porduct.commodityUnit +'</b>'+
            '</div>'+
        '</a>'+
        '<div class="D-BuyNum">'+
            '<button class="decrease" style="">'+
                '<i class="icon-reduce"></i>'+
            '</button>'+
            '<input style="" type="number" class="line amount" value="0">'+
            '<button class = "addition">'+
                '<i class="icon-plus"></i>'+
            '</button>'+
        '</div>'+
    '</li>';
    $("ol:last").append(li);
    // $(".amount").hide();
    // $(".decrease").hide();
}

function InitButton(){
    $(".addition").click(function(){
        // console.log($(this).prev().val());
        // $(this).prev().show();
        var a = $(this).prev().val();
        $(this).prev().val(parseInt(a)+1);
        Count();
    });
    $(".decrease").click(function(){
        var b = $(this).next().val();
        if(b != 0){
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
    $("#count").text("合计：￥"+sum+"元");
}