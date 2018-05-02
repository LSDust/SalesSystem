$(document).ready(function () {
    $("#submit").click(function () {
        var action = $("input[name='action']:checked").val();
        if (action == "signup") {
            if ($("#pass").val() == $("#repass").val()) {
                $.ajax({
                    type: "POST",
                    url: "Signup",
                    data: {
                        tel: $("#tel").val(),
                        pass: $("#pass").val()
                    },
                    success: function (data) {
                        if (data) {
                            alert("注册成功");
                        } else {
                            alert("注册失败");
                        }
                    },
                    error: function (jqXHR) {
                        alert("发生错误：" + jqXHR.status);
                    },
                });
            } else {
                alert("两次密码不一致");
            }
        } else if (action == "signin") {
            $.ajax({
                type: "POST",
                url: "Signin",
                data: {
                    tel: $("#tel").val(),
                    pass: $("#pass").val()
                },
                success: function (data) {
                    if (data == "Admin") {
                        location.href = "../Admin/Index";
                    } else if (data == "Retailer") {
                        location.href = "../Sell/CommodityInfo";
                    } else if (data == "Purchaser") {
                        location.href = "../Client/Index";
                    } else {
                        alert(data);
                    }
                },
                error: function (jqXHR) {
                    alert("发生错误：" + jqXHR.status);
                },
            });
        }
    })
});