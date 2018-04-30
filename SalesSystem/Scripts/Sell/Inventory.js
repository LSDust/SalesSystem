$(document).ready(function(){
    //数据初始化
    $.ajax({
        type: "GET",
        url: "InitInventory",
        dataType: "json",
        success: function (data) {
            data.forEach(AddTr);
        },
        error: function (jqXHR) {
            alert("发生错误：" + jqXHR.status);
        },
    });
});

function AddTr(initInventory){
    initInventory.commodityName;
    initInventory.inventoryQuantity;
    var tbody = '<tr class="">'+
        '<td>'+1+'</td>'+
        '<td><p>'+initInventory.commodityName+'</p></td>'+
        '<td>'+initInventory.inventoryQuantity+'</td>'+
    '</tr>';
    $("tbody").append(tbody);
}
