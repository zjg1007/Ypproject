// 有关“空白”模板的简介，请参阅以下文档:
// http://go.microsoft.com/fwlink/?LinkID=397704
// 若要在 Ripple 或 Android 设备/仿真程序中调试代码: 启用你的应用程序，设置断点，
// 然后在 JavaScript 控制台中运行 "window.location.reload()"。
(function () {
    "use strict";

    document.addEventListener('deviceready', onDeviceReady.bind(this), false);
    var isExitSystem = "2"

    function onDeviceReady() {
        // 处理 Cordova 暂停并恢复事件
        document.addEventListener('pause', onPause.bind(this), false);
        document.addEventListener('resume', onResume.bind(this), false);
        // 捕获返回键操作，确定是否退出系统
        document.addEventListener('backbutton', function (evt) {
            navigator.notification.confirm(
                '你将退出系统，请确认！',    // 提示消息
                 onConfirm,               // 用当前选择的按钮的索引（从1开始）调用执行处理的函数
                '系统提示',                // 对话框标题
                ['取消', '退出']           // 按钮数组
            );
        }, false);

        var register = document.getElementById('register');
        register.addEventListener('click', regst);

    };


    function onPause() {
        // TODO: 此应用程序已挂起。在此处保存应用程序状态。
    };

    function onResume() {
        // TODO: 此应用程序已重新激活。在此处还原应用程序状态。
    };

  

    // 处理注册
    function regst() {
        var userName = document.getElementById('userName').value;
        var password = document.getElementById('password').value;
        if (userName === '' || password === '') {
            document.getElementById('logonStatusPrompt').innerHTML = "<span style='font-family:\"Microsoft YaHei\";'> 用户名或者密码不能为空值!</span>";
        } else if (userName.length < 11) {
            alert('请输入正确的手机号码');
        } else if (password.length < 6) {
            alert('密码起码6个字符,请重新输入');
        }
        else {
            var urlString = 'http://pengcheng.ngrok.cc/api/Account/Register';
            var statusElementID = "logonStatusPrompt";
            var statusMessage = "<span style='font-family:\"Microsoft YaHei\";'>正在注册 ...</span>";

            // 使用 ajax 向服务端提交 json 规格的数据（用户名和密码）
            $.ajax({
                type: 'POST',       // 提交的协议方式
                url: urlString,     // 后端服务的 url
                data:{ username: userName, password:password}, 
                beforeSend: function () {
                    $('#' + statusElementID).html(statusMessage);
                }
            }).done(function (data) {
                var isLogon = data.isLogon;
                if (isLogon) {
                    alert(data.message);
                    var storage = window.localStorage;
                    storage.setItem("username", userName);
                    window.location = "mainWorkPlace.html";
                } else {
                    document.getElementById('logonStatusPrompt').innerHTML = data.message;
                }
            }).fail(function () {
                document.getElementById('logonStatusPrompt').innerHTML = "网络连接错误或服务器问题。";

            }).always(function () {

            });
        }
    }
    // 处理退出系统的判断
    function onConfirm(buttonIndex) {
        if (buttonIndex === 2) {
            if (window.location.href !== "file:///android_asset/www/index.html") {
                window.history.back();
            } else {
                if (navigator.app) {
                    navigator.app.exitApp();
                }
                else if (navigator.device) {
                    navigator.device.exitApp();
                }
                else {
                    window.close();
                }
            }
        }
    }



})();