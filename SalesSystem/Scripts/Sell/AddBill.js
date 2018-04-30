$(document).ready(function(){
    $("#AddCommodity").click(function(){
        AddTr3();
    });

    $.ajax({
        type: "GET",
        url: "InitPurchaser",
        dataType: "json",
        success: function (data) {
            data.forEach(AddOption2);
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });

    $("#AddBill").click(function(){
        var table =document.getElementById("tab1");
        var rows = table.rows.length - 1;
        var purchaser = $("select:first").val();
        var i;
        for(i = 1;i<=rows&&i>0;i++){
            var cname = $('tr:eq('+ i +')').children().next().children().find("option:selected").text();
            var quantity = $('tr:eq('+ i +')').children().next().next().children().val();
            $.ajax({
                type: "POST",
                url: "SaveBill",
                data: {
                    purchaserId:purchaser,
                    cname: cname,
                    quantity: quantity
                },
                success: function (data) {
                    alert("保存成功");      //待改
                },
                error: function (jqXHR) {
                    alert("发生错误：" + jqXHR.status);
                },
            });
        }
    });
});

function AddOption2(purchaser){
    $("select:first").append("<option value="+purchaser.purchaserId+">"+purchaser.purchaserName+"</option>");
}

function AddTr3(){
    var table =document.getElementById("tab1");
    var rows = table.rows.length;
    var tbody = '<tr class="">'+
        '<td>'+rows+'</td>'+
        '<td>'+
            '<select name="">'+
                '<option value="">无</option>'+
            '</select>'+
        '</td>'+
        '<td><input type="text" class="tb-input" placeholder="" onchange="count()"></td>'+
        '<td class="sd"><p class="count"></p></td>'+
    '</tr>';
    $("tbody").append(tbody);
    InitCommodity();
}