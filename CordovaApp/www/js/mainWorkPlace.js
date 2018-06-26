// 有关“空白”模板的简介，请参阅以下文档:
// http://go.microsoft.com/fwlink/?LinkID=397704
// 若要在 Ripple 或 Android 设备/仿真程序中调试代码: 启用你的应用程序，设置断点，
// 然后在 JavaScript 控制台中运行 "window.location.reload()"。
(function () {
    document.addEventListener( 'deviceready', onDeviceReady.bind( this ), false );
    function onDeviceReady() {
        // 捕获返回键操作，确定是否退出系统
        document.addEventListener('backbutton', function (evt) {
            navigator.notification.confirm(
                '你将退出系统，请确认！',    // 提示消息
                 onConfirm,               // 用当前选择的按钮的索引（从1开始）调用执行处理的函数
                '系统提示',                // 对话框标题
                ['取消', '退出']           // 按钮数组
            );
        }, false);


    };

    // 处理退出系统的判断
    function onConfirm(buttonIndex) {
        if (buttonIndex === 2) {
            if (window.location.href !== "file:///android_asset/www/mainWorkPlace.html") {
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
} )();