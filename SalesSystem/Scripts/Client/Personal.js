$(document).ready(function () {
    $("$submit").click(function () {
        $.ajax({
            type: "POST",
            url: "UpdatePurchaserInfo",
            data: {
                name: $("#name").val(),
                tel: $("#tel").val()
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