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

function AddTr(initInventory,index){
    initInventory.commodityName;
    initInventory.inventoryQuantity;
    i = index + 1;
    if (initInventory.inventoryQuantity > 5) {
        var tbody = '<tr class="">' +
            '<td>' + i + '</td>' +
            '<td><p>' + initInventory.commodityName + '</p></td>' +
            '<td>' + initInventory.inventoryQuantity + '</td>' +
        '</tr>';
        $("tbody").append(tbody);
    } else {
        var tbody = '<tr style="font-weight:bold;color:red;">' +
            '<td>' + i + '</td>' +
            '<td><p>' + initInventory.commodityName + '</p></td>' +
            '<td>' + initInventory.inventoryQuantity + '</td>' +
        '</tr>';
        $("tbody").append(tbody);
    }

}
