$(document).ready(function () {
    $("#save").click(function () {
        $.ajax({
            type: "POST",
            url: "AddCommdity",
            data: {
                cname: $("#commdityName").val(),
                pic: $("#commdityPic").val(),
                cost: $("#primeCost").val(),
                price: $("#sellingPrice").val(),
                unit: $("#unit").val()
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