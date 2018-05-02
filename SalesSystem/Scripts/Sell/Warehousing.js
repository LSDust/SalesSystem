$(document).ready(function () {
    $("#addInventory").click(function(){
        var table =document.getElementById("tab1");
        var rows = table.rows.length;
        var tbody = "<tr class=''>"+
            "<td><p>"+ rows +"</p></td>"+
            "<td>"+
                "<select name='' class='selectpicker span2'>"+
                    "<option value=''>无</option>"+
                "</select>"+
            "</td>"+
            "<td><input type='text' class='tb-input' onchange='count()'></td>"+ //加单位
            "<td><p class='count'></p></td>"+
        "</tr>";
        $("#tbody").append(tbody);
        InitCommodity();
        // InitOnchange();
    });

    $("#AddWareHousing").click(function(){
        var table =document.getElementById("tab1");
        var rows = table.rows.length - 1;
        var i;
        for(i = 1;i<=rows&&i>0;i++){
            var quantity = $('tr:eq('+ i +')').children().next().next().children().val();
            var name = $('tr:eq('+ i +')').children().next().children().find("option:selected").text();
            $.ajax({
                type: "POST",
                url: "SaveManage",
                data: {
                    //id:id,
                    cname: name,
                    quantity: quantity
                },
                success: function (data) {
                    alert("保存成功");      //待改
                },
                error: function (jqXHR) {
                    alert("发生错误：" + jqXHR.status);
                },
            });
            //$.ajax({
            //    type: "POST",
            //    url: "SaveManageBill",
            //    data: {
            //        //id:id,
            //        cname: name,
            //        quantity: quantity
            //    },
            //    success: function (data) {
            //        alert("保存成功");      //待改
            //    },
            //    error: function (jqXHR) {
            //        alert("发生错误：" + jqXHR.status);
            //    },
            //});
        }
    });
});


function InitCommodity(){
    $.ajax({
        type: "GET",
        url: "InitCommodity",
        dataType: "json",
        success: function (data) {
            data.forEach(AddOption);
            InitSelect();
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });
}

function AddOption(comdty,index){
    // console.log(comdty.commodityId);
    $("select:last").append("<option value="+comdty.sellingPrice+">"+comdty.commodityName+"</option>");
}

function InitSelect(){      //删除
    $("select").change(function(){
        var value = $(this).val();
    });
}

function count(){       //待改
    var table =document.getElementById("tab1");
    var rows = table.rows.length - 1;
    var quantity = $('tr:eq('+ rows +')').children().next().next().children().val();
    var price = $('tr:eq('+ rows +')').children().next().children().val();
    var count = quantity*price;
    $('tr:eq('+ rows +')').children().next().next().next().children().text(count+'元');
    sum();
}

function sum(){
    var sum = 0;
    $(".count").each(function(){
        sum += parseInt($(this).html());
    });
    $("#sum").text("合计："+sum+"元");
}