(function () {
    "use strict";

    document.addEventListener('deviceready', onDeviceReady.bind(this), false);

    function onDeviceReady() {
        // 处理 Cordova 暂停并恢复事件
        document.addEventListener('pause', onPause.bind(this), false);
        document.addEventListener('resume', onResume.bind(this), false);
        document.addEventListener("online", onOnline, false); // 连线事件
        document.addEventListener("offline", onOffline, false); // 掉线事件

        // 绑定检查设备相关信息的方法
        var equipmentInfo = document.getElementById('equipmentInformation');
        equipmentInfo.addEventListener("click", equipmentInformation);
        // 绑定检查网络信息的方法
        var networkInfo = document.getElementById('networkInformation');
        networkInfo.addEventListener("click", checkConnection);
        // 绑定对话框的方法
        var getNoticeInfo = document.getElementById('noticeInfo');
        getNoticeInfo.addEventListener("click", getNotice);
        // 绑定检查GPS数据的方法
        var getAccelInfo = document.getElementById('getAccel');
        getAccelInfo.addEventListener("click", getAccelerometer);

        // 绑定检查GPS数据的方法
        var getGPSInfo = document.getElementById('gpsInfo');
        getGPSInfo.addEventListener("click", getGPSPositon);
        // 绑定拍照的方法
        var getCameraInfo = document.getElementById('cameraInfo');
        getCameraInfo.addEventListener("click", getCameraPicture);

        // 跳转sqliteDemo
        var sqlieDemo = document.getElementById('sqliteDemo');
        sqlieDemo.addEventListener("click", gotoSqlieDemo);
    };


    // 设备信息
    function equipmentInformation() {
        document.getElementById('apiClassName').innerHTML = "Cordova对象：device";
        document.getElementById('apiClassDescription').innerHTML = "当前设备的基础信息";

        var model = device.model; // 机型数据
        var devString = device.platform; // 移动设备平台
        var uuidString = device.uuid;    // 移动设备的uuid
        var versionString = device.version; // 版本
        var isSim = device.isVirtual;
        var serialString = device.serial;  // 移动设备的序列号

        var htmlString = "";
        htmlString = htmlString + "<dl><dt>机型数据</dt><dd>" + model + "</dd></dl>";
        htmlString = htmlString + "<dl><dt>移动设备平台</dt><dd>" + devString + "</dd></dl>";
        htmlString = htmlString + "<dl><dt>移动设备的uuid</dt><dd>" + uuidString + "</dd></dl>";
        htmlString = htmlString + "<dl><dt>版本</dt><dd>" + versionString + "</dd></dl>";
        htmlString = htmlString + "<dl><dt>有无Sim卡</dt><dd>" + isSim + "</dd></dl>";
        htmlString = htmlString + "<dl><dt>设备的序列号</dt><dd>" + serialString + "</dd></dl>";
        document.getElementById('apiContent').innerHTML = htmlString;

    }

    // 网络连接类型
    function checkConnection() {
        document.getElementById('apiClassName').innerHTML = "Cordova对象：navigator ";
        document.getElementById('apiClassDescription').innerHTML = "通过 navigator.connection 提供用途当前设备的网络信息和状态信息：";

        //document.getElementById('apiName').innerHTML = "当前网络连接类型：";
        var networkState = navigator.connection.type;

        var states = {};
        states[Connection.UNKNOWN] = '未知连接。';
        states[Connection.ETHERNET] = '以太网连接';
        states[Connection.WIFI] = 'WiFi 连接';
        states[Connection.CELL_2G] = ' 2G 连接';
        states[Connection.CELL_3G] = ' 3G 连接';
        states[Connection.CELL_4G] = ' 4G 连接';
        states[Connection.CELL] = '蜂窝电话连接';
        states[Connection.NONE] = '没有网络连接';

        document.getElementById('apiContent').innerHTML = states[networkState];
    }
    function validatemobile(mobile) {
        if (mobile.length == 0) {
            alert('请输入手机号码！');
            document.form1.mobile.focus();
            return false;
        }
        if (mobile.length != 11) {
            alert('请输入有效的手机号码！');
            document.form1.mobile.focus();
            return false;
        }

        var myreg = /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1}))+\d{8})$/;
        if (!myreg.test(mobile)) {
            alert('请输入有效的手机号码！');
            document.form1.mobile.focus();
            return false;
        }
    }
    function onPause() {
        // TODO: 此应用程序已挂起。在此处保存应用程序状态。
    };

    function onResume() {
        // TODO: 此应用程序已重新激活。在此处还原应用程序状态。
    };

    function onOnline() {
        // 处理连线事件
    }
    function onOffline() {
        // 处理掉线事件
    }

    // 设备感应器的空间位置
    function getAccelerometer() {
        navigator.accelerometer.getCurrentAcceleration(onSuccess, onError);
    }
    function onSuccess(acceleration) {
        document.getElementById('apiClassName').innerHTML = "Cordova对象：acceleration";
        document.getElementById('apiClassDescription').innerHTML = "当前设备感应器的空间位置";
        var aString = 'X: ' + acceleration.x + '<br/>' +
              'Y: ' + acceleration.y + '<br/>' +
              'Z: ' + acceleration.z + '<br/>' +
              '时间戳: ' + acceleration.timestamp + '<br/>';
        document.getElementById('apiContent').innerHTML = aString;

    }

    function onError() {
        alert('错误!');
    }

    // 设备GPS坐标数据
    function getGPSPositon() {
        document.getElementById('apiClassName').innerHTML = "Cordova对象：navigator.geolocation";
        document.getElementById('apiClassDescription').innerHTML = "当前设备的GPS空间位置";

        navigator.geolocation.getCurrentPosition(onGPSSuccess, onGPSError);
    }
        

    var onGPSSuccess = function (position) {
        var gpsString = '维度: ' + position.coords.latitude+
              '经度: ' + position.coords.longitude 
              //'海拔: ' + position.coords.altitude + '<br/>' +
              //'精度: ' + position.coords.accuracy + '<br/>' +
              //'海拔精度: ' + position.coords.altitudeAccuracy + '<br/>' +
              //'头方向: ' + position.coords.heading + '<br/>' +
              //'速度: ' + position.coords.speed + '<br/>' +
              //'时间戳: ' + position.timestamp + '<br/>';

        document.getElementById('apiContent').innerHTML = gpsString;

    };

    function onGPSError(error) {
        alert('错误代码: ' + error.code + '\n' +
              '错误信息: ' + error.message + '\n');
    }

    // 拍照相关
    function getCameraPicture() {
        document.getElementById('apiClassName').innerHTML = "Cordova对象：Camera";
        document.getElementById('apiClassDescription').innerHTML = "使用当前设备的摄像头拍照";

        navigator.camera.getPicture(onCameraSuccess, onCamaraFail, {
            quality: 50,
            destinationType: Camera.DestinationType.FILE_URI
        });
    }

    function onCameraSuccess(imageURI) {
        var imgString = '<img src="' + imageURI + '" style="width:100%" />';
        document.getElementById('apiContent').innerHTML = imgString;

    }

    function onCamaraFail(message) {
        alert('拍照失败: ' + message);
    }

    function getNotice() {
        document.getElementById('apiClassName').innerHTML = "Cordova对象：navigator.notification";
        document.getElementById('apiClassDescription').innerHTML = "会话框";

        navigator.notification.confirm(
        '你将退出系统，请确认！',    // 提示消息
         onConfirm,               // 用当前选择的按钮的索引（从1开始）调用执行处理的函数
        '系统提示',                // 对话框标题
        ['取消', '退出']           // 按钮数组
        );
    }

    function onConfirm(buttonIndex) {
        var noticeString = "";
        if (buttonIndex == 1) {
            noticeString = "1----取消";
        } else {
            noticeString = "2----退出";
        }
        document.getElementById('apiContent').innerHTML = noticeString;

    }
     
    function gotoSqlieDemo() {
        window.location = "sqliteDemo.html";
    }
})();