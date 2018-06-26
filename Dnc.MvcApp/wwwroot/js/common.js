// 弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
    $.messager.alert(title, msgString, msgType);
}


//弹出消息框
function alertMsg(title, msg) {
    if (msg != null && msg != undefined && msg != '') {
        $.messager.alert(title, msg);
    }
}
function alertError(title, msg) {
    $.messager.alert(title, '<div style="line-height:30px;">' + msg + '</div>', 'error');
}
function alertInfo(title, msg) {
    if (msg != null && msg != undefined && msg != '') {
        $.messager.alert(title, '<div style="line-height:30px;">' + msg + '</div>', 'info');
    }
}
function alertQuestion(title, msg) {
    $.messager.alert(title, '<div style="line-height:30px;">' + msg + '</div>', 'question');
}
function alertWaring(title, msg) {
    $.messager.alert(title, '<div style="line-height:30px;">' + msg + '</div>', 'warning');
}
//对每一个请求的URL进行处理，确保每一次请求都是新的,
//否则Url相同视为相同请求，浏览器则不执行，而是返回上一次请求结果（避免缓存问题)
function dealAjaxUrl(url) {
    var guid = GetGuid();
    var ajaxurl;
    if (url.indexOf("?") > -1)//url带参数
        ajaxurl = url + "&ajaxGuid=" + guid;
    else //url不带参数
        ajaxurl = url + "?ajaxGuid=" + guid;
    ajaxurl += "&url=" + location.href; //加上URL参数，用于出现错误时返回上一页
    return ajaxurl;
}
//根据时间生成GUID
function GetGuid() {
    var now = new Date();
    var year = now.getFullYear();
    var month = now.getMonth();
    var date = now.getDate();
    var day = now.getDay();
    var hour = now.getHours();
    var minu = now.getMinutes();
    var sec = now.getSeconds();
    var mill = now.getMilliseconds();
    month = month + 1;
    if (month < 10) month = "0" + month;
    if (date < 10) date = "0" + date;
    if (hour < 10) hour = "0" + hour;
    if (minu < 10) minu = "0" + minu;
    if (sec < 10) sec = "0" + sec;
    var guid = month + date + hour + minu + sec + mill;
    return guid;
}



//----------------------easyui公共方法----------------------------
//easyui-datagrid设置分页控件
function setPager(p) {
    $(p).pagination({
        pageSize: 20, //每页显示的记录条数，默认为10 
        pageList: [20, 30, 50], //可以设置每页记录条数的列表 
        beforePageText: '第', //页数文本框前显示的汉字 
        afterPageText: '页    共 {pages} 页',
        displayMsg: '<div style="padding-right:20px;">当前显示 <b>{from} - {to}</b> 条记录   共 <b>{total}</b> 条记录<div>'
    });
}
function Pager(tab, url, queryParams) {
    //设置分页
    var p = tab.datagrid('getPager');
    $(p).pagination({
        pageSize: 20, //每页显示的记录条数，默认为10 
        pageList: [20, 30, 50], //可以设置每页记录条数的列表 
        beforePageText: '第', //页数文本框前显示的汉字 
        afterPageText: '页    共 {pages} 页',
        displayMsg: '<div style="padding-right:20px;">当前显示 <b>{from} - {to}</b> 条记录   共 <b>{total}</b> 条记录<div>'
    });
}
//easyui-datebox 时间格式化
function timeFormatter(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    var h = date.getHours();
    var mm = date.getMinutes();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d) + ' ' + (h < 10 ? ('0' + h) : h) + ':' + (mm < 10 ? ('0' + mm) : mm);
}
//easyui-datebox 时间解析器(2013-07-17 19:00:00)
function timeParser(s) {
    if (!s) return new Date();
    var ss = s.split('-');
    var y = parseInt(ss[0], 10);
    var m = parseInt(ss[1], 10);
    var d = parseInt(ss[2], 10);
    var time = ss[2].substr(3);
    var tt = time.split(':');
    var h = parseInt(tt[0], 10);
    var mm = parseInt(tt[1], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d) && !isNaN(h) && !isNaN(mm)) {
        return new Date(y, m - 1, d, h, mm);
    }
    else {
        return new Date();
    }
}
//2010-10-10
function FormatDate(date) {
    if (date == '' || date == null)
        return '';
    data = date.replace("T", " ");
    date = date.substr(0, 4) + "/" + date.substr(5, 2) + "/" + date.substr(8, 2)
    date = new Date(date);
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    var h = date.getHours();
    var mm = date.getMinutes();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
}

function FormatYear(date) {
    if (date == '' || date == null)
        return '';
    data = date.replace("T", " ");
    date = date.substr(0, 4) + "/" + date.substr(5, 2) + "/" + date.substr(8, 2)
    date = new Date(date);
    var y = date.getFullYear();
    //var m = date.getMonth() + 1;
    //var d = date.getDate();
   // var h = date.getHours();
    //var mm = date.getMinutes();
    return y;
}
//时间控件2011-10-10
function formatD(date) {
    return date.getFullYear() + "-" + date.getMonth() + "-" + date.getDay();
}

//打开弹出窗口
function openWin(title, w, h, url) {
    AppentDiv();
    if (w == 0) {
        w = document.body.clientWidth;
    }
    if (h == 0) {
        w = document.body.clientHeight;
    }

    var top = (document.body.clientHeight - h) / 2 - 50;
    var left = (document.body.clientWidth - w) / 2;
    
    document.getElementById("iframe").src = url;
    $('#win').window({
        title: title,
        width: w,
        height: h,
        modal: true,
        top : top ,
        left: left,
        closed: false
    });
}

//$(function () {
//    IsLogin();
//});

function AppentDiv() {
    if ($('#win')) {
        $('#win').remove();
    }
    if ($('#openWin')) {
        $("#openWin").append("<div id=\"win\" class=\"easyui-window\" style=\" padding: 0px; margin: auto;\" data-options=\"iconCls:'icon-save',top:'3px',closed:true,minimizable:false,maximizable:false,collapsible:false\"><iframe id=\"iframe\" name=\"iframe\" frameborder=\"0\" style=\"width: 100%; height: 100%;\"></iframe></div>");
    }
}

//打开弹出窗口
function openMaxWin(title, url) {
    var w = $(document).width();
    var h = $(document).height();
    AppentDiv();
    document.getElementById("iframe").src = url;
    $('#win').window({
        title: title,
        width: w,
        height: h ,
        top: 0,
        left: 0,
        fit:true,
        shadow: true,
        closed: false
    });
}
//选中行
function GetSelectValue(tab) {
    var rows = $('#' + tab).datagrid('getSelections');
    return rows;
}

//关闭弹出框
function CloseWindowed() {
    $('#win').window
    ({
        closed: true
    });
  
}

//关闭弹出框
function CloseWindow(msg, fun) {
    alertInfo('提示', msg);

    $('#win').window
    ({
        closed: true
    
    });
  
    if (fun != undefined && fun != null && fun != '') {
        fun();
    }
}
//关闭弹出框
function CloseWindowSetInter() {
    $('#win').window
    ({
        closed: true
    });
    //locationHome();
}
//获取url中的参数
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
}
//两个参数，一个是cookie的名子，一个是值
function SetCookie(name, value) {
    var Days = 30; //此 cookie 将被保存 30 天
    var exp = new Date();    //new Date("December 31, 9998");
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}

//取cookies函数
function GetCookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;
}

//删除cookie
function DelCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}

function IsLogin()
{
    //var lobtn = document.getElementById('loginbtn');
    //lobtn.onclick = function () {
    //    if (this.innerText = '注销') {
    //        DelCookie('userid');
    //        document.location = '../../User/Login.html';
    //    } else {
    //        document.location = '../../User/Login.html';
    //    }
    //}
    if (GetCookie('userid') == '' || GetCookie('userid') == null) {
        //lobtn.innerHTML = '登录';
        document.location = '../../User/Login.html';
        return;
    } 
}


function easyRightDisplay() {

    //获取所有的toolbar按钮

    var mId = getUrlParam("menuid");

    var rId = '';

    $.post(
        "../../api/SystemManage/sysmodule.ashx",
        { method: "GetModuleRoleButton", moduleId: mId, roleId: rId },
        function (data) {

            var button = $('div.datagrid div.datagrid-toolbar a');
            for (var i = 0; i < button.length; i++) {

                var toolbar = button[i];

                var id = toolbar.id;
                $('div.datagrid div.datagrid-toolbar a').eq(i).hide();
                if (data != undefined && data.indexOf(id) > -1) {  //隐藏Id为add的按钮

                    $('div.datagrid div.datagrid-toolbar a').eq(i).show();
                    $('div.datagrid div.datagrid-toolbar a').eq(i).disabled = false;
                }

            }
        },
        "text"
    );


}

function easyTbDisplay() {

    //获取所有的toolbar按钮

    var mId = getUrlParam("menuid");

    var rId = '';

    $.post(
        "../../api/SystemManage/sysmodule.ashx",
        { method: "GetModuleRoleButton", moduleId: mId, roleId: rId },
        function (data) {

           
            var button = $("#tb div a");
            for (var i = 0; i < button.length; i++) {

                var toolbar = button[i];                
                var id = toolbar.id;
                
                toolbar.style.display = "none";
                if (data != undefined && data.indexOf(id) > -1) {  
                    toolbar.style.display = "";                    
                }

            }
        },
        "text"
    );


}