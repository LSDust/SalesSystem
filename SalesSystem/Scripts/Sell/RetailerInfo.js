$(document).ready(function () {
    $("$submit").click(function () {
        $.ajax({
            type: "POST",
            url: "UpdateRetailerInfo",
            data:{
                name:$("#name").val(),
                tel:$("#tel").val(),
                add:$("#add").val()
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