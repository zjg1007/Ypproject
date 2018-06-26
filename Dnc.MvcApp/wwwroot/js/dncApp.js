// 使用ajax方式提交表单
function postLogonForm() {
    
    var logonFormOptions = {
        dataType: 'json',
        data: { yanzhen: $('#yanz').val() },
        success: function (data) {
            var isLogon = data.isLogon;
            if (isLogon === true) {
                //$('#logonModal').modal('hide');
                document.getElementById("gotoSystem").style.display = "";//显示
                if ($('#imgVerify').attr('src').indexOf("?") >= 0) {
                    $('#imgVerify').attr('src', '/Account/ValidateCode')
                } else {
                    $('#imgVerify').attr('src', '/Account/ValidateCode' + '?')
                }
            } else {
                var message = '<p class="bg-warning">' + data.message + '</p>';
                $('#logonTips').html(message);
                if ($('#imgVerify').attr('src').indexOf("?") >= 0) {
                    $('#imgVerify').attr('src', '/Account/ValidateCode')
                } else {
                    $('#imgVerify').attr('src', '/Account/ValidateCode' + '?')
                }
            }
        }
    };
    $('#logonForm').ajaxSubmit(logonFormOptions);
}
function postActorForm() {
    var logonFormOptions = {
        dataType: 'json',
        success: function (data) {
            var isLogon = data.isLogon;
            if (isLogon === true) {
                self.location = document.referrer;
            } else {
                if (confirm("已经有同名的演员,您确定还要添加吗？")) {
                    $('#formRegister').attr('action', '/Actor/Create');
                    $('#formRegister').submit();
                    $('#formRegister').submit().ready(function () {
                        self.location = document.referrer;
                    });
                }
                else { }

            }
        }
    };
    $('#formRegister').ajaxSubmit(logonFormOptions);
}
function postMovieForm() {
    var storage = window.localStorage;
    var str = '';
    var typelist = $('#typeList ul li.hover').each(function () {
        str += $(this).html() + ',';
    });
    str = str.substring(0, str.length - 1);//类型

    var logonFormOptions = {
        dataType: 'json',
        data: { typelist: str },
        success: function (data) {
            var isLogon = data.isLogon;
            if (isLogon === true) {
                storage.clear();
                document.location.href = '/Movie/index';
            } else {
                alert(isLogon.message);
            }
        }
    };
    $('#formMovieCreate').ajaxSubmit(logonFormOptions);
}
function postMovieForm() {
    var storage = window.localStorage;
    var str = '';
    var typelist = $('#typeList ul li.hover').each(function () {
        str += $(this).html() + ',';
    });
    str = str.substring(0, str.length - 1);//类型

    var logonFormOptions = {
        dataType: 'json',
        data: { typelist: str },
        success: function (data) {
            var isLogon = data.isLogon;
            if (isLogon === true) {
                storage.clear();
                document.location.href = '/Movie/index';
            } else {
                alert(isLogon.message);
            }
        }
    };
    $('#formMovieCreate').ajaxSubmit(logonFormOptions);
}
function postMovieEdieForm() {
    var storage = window.localStorage;
    var str = '';
    var typelist = $('#typeList ul li.hover').each(function () {
        str += $(this).html() + ',';
    });
    str = str.substring(0, str.length - 1);//类型

    var logonFormOptions = {
        dataType: 'json',
        data: { typelist: str },
        success: function (data) {
            var isLogon = data.isLogon;
            if (isLogon === true) {
                storage.clear();
                document.location.href = '/Movie/index';
            } else {
                alert(isLogon.message);
            }
        }
    };
    $('#formMovieEdieCreate').ajaxSubmit(logonFormOptions);
}
//演员添加
function aAddActor(attr) {
    var str = '/Movie/AddActor/'+attr;
    $.ajax({
        cache: true,
        type: "Get",
        url: str,// 你的formid
        async: false,
        data: { start: $('#my-startDate').val(), endTime: $('#my-endDate').val() },
        error: function (request) {
            alert("网络错误,请检查网络异常.");
        },
        success: function (data) {
            var isLogon = data.isLogon;
            if (isLogon == true) {
                alert('添加成功');
                window.location.href = '/Movie/Create';
            } else {
                alert('不能重复添加,请重新选择');
            }
        }
    });
}
function EdieActor(attr) {
    var str = '/Movie/AddActor/' + attr;
    $.ajax({
        cache: true,
        type: "Get",
        url: str,// 你的formid
        async: false,
        data: { start: $('#my-startDate').val(), endTime: $('#my-endDate').val() },
        error: function (request) {
            alert("网络错误,请检查网络异常.");
        },
        success: function (data) {
            var isLogon = data.isLogon;
            if (isLogon == true) {
                alert('添加成功');
                window.location.href = '/Movie/Edit';
            } else {
                alert('不能重复添加,请重新选择');
            }
        }
    });
}
//演员添加（编辑）
function aAddActorEdie() {
    var str = $('#addacotr').attr("srca");
    $.ajax({
        cache: true,
        type: "Get",
        url: str,// 你的formid
        async: false,
        data: { start: $('#my-startDate').val(), endTime: $('#my-endDate').val() },
        error: function (request) {
            alert("网络错误,请检查网络异常.");
        },
        success: function (data) {
            var isLogon = data.isLogon;
            if (isLogon == true) {
                alert('添加成功');
                window.location.href = '/Movie/Edit';
            } else {
                alert('不能重复添加,请重新选择');
            }
        }
    });
}
//演员删除
function RemoveActor(id)
{
    if (confirm("您确定要删除吗？")) {
        var url = '/Movie/RemoveActor/' + id;
        $.ajax({
            cache: true,
            type: "Get",
            url: url,// 你的formid
            async: false,
            error: function (request) {
                alert("网络错误,请检查网络异常.");
            },
            success: function (data) {
                var isLogon = data.isLogon;
                if (isLogon == true) {
                    if ($('#movieList ul li').size() == 1) {
                        $('#movieList').remove();
                    } else {
                         $('.dl[remid=' + id + ']').remove();
                    }
                } else {
                    alert('删除失败');
                }
            }
        });
        }
    else { }
}
//点击进行页面存储
function AddInfo()
{
    var storage = window.localStorage;
    var oName = document.getElementById('Name').value;
    var oCountry = document.getElementById('Country').value;
    var oNickname = document.getElementById('Nickname').value;
    var oLaunchName = document.getElementById('LaunchName').value;
    var oDuration = document.getElementById('Duration').value;
    var oMass = document.getElementById('Mass').value;
    var oReferral = document.getElementById('Referral').value;
    var ofiles = document.getElementById('files').value;
    var oProtagonist = document.getElementById('Protagonist').value;
    var otypeList = $('#typeList ul li');
    var strList='';
    otypeList.each(function (e) {
        if ($(this).attr('class') == 'hover')
        {
            strList += $(this).text()+',';
        }
    });
    if (strList != null)
    {
        strList = strList.substr(0, strList.length - 1)
    }
    storage.setItem('oName', oName);
    storage.setItem('oCountry', oCountry);
    storage.setItem('oNickname', oNickname);
    storage.setItem('oLaunchName', oLaunchName);
    storage.setItem('oDuration', oDuration);
    storage.setItem('oMass', oMass);
    storage.setItem('oReferral', oReferral);
    storage.setItem('ofiles', ofiles);
    storage.setItem('strList', strList);
    storage.setItem('oProtagonist', oProtagonist);
}
//增加上映信息
function AddLaunchinfo() {
    var str = '';
    var str1 = '';
    var isTrue = true;
    $('.Date').each(function (e) {
        if ($(this).val() == '') {
            isTrue = false;
        }
        str += $(this).val() + ',';
    });
    str = str.substring(0, str.length - 1);
    $('.Region').each(function (e) {
        if ($(this).val() == '') {
            isTrue = false;
        }
        str1 += $(this).val() + ',';
    });
    str1 = str1.substring(0, str1.length - 1);
    if (!isTrue) { alert('输入信息不能为空'); return; }
    $.ajax({
        cache: true,
        type: "post",
        url: '/Movie/LaunchInfo',// 你的formid
        async: false,
        data:{strdate:str,str:str1},
        error: function (request) {
            alert("网络错误,请检查网络异常.");
        },
        success: function (data) {
            var isLogon = data.isLogon;
            if (isLogon == true) {
                alert('保存成功');
                window.location.href = '/Movie/index';
            } else {
                alert('保存失败');
            }
        }
    });
}
//影厅增加
function AddCinma() {
    layer.load();
    var ocolumn = $('#seat .place ul li');
    var column = '';
    ocolumn.each(function (e) {
        if ($(this).attr('class') == 'hover') {
            column += 'True' + ',';
        } else {
            column += 'False' + ',';
        }
    });
    column = column.substring(0, column.length - 1);
    var logonFormOptions = {
        dataType: 'json',
        data: { columnList: column },
        error: function (request) {
            layer.closeAll('loading');
            layer.msg('保存失败');
        },
        success: function (data) {
            var isLogon = data.isLogon;
            if (isLogon === true) {
                layer.closeAll('loading');
                layer.msg('添加成功');
                document.location.href = '/Cinema/Index';
            } else {
                layer.closeAll('loading');
                layer.msg('添加失败');
            }
        }
    };
    $('#formCinema').ajaxSubmit(logonFormOptions);
}
function back() {
    window.history.back();
}
//影厅修改
function EdieCinma() {
    layer.load();
    var ocolumn = $('#seat .place ul li');
    var column = '';
    ocolumn.each(function (e) {
        if ($(this).attr('class') == 'hover') {
            column += 'True' + ',';
        } else {
            column += 'False' + ',';
        }
    });
    column = column.substring(0, column.length - 1);
    var logonFormOptions = {
        dataType: 'json',
        data: { columnList: column },
        error: function (request) {
            layer.closeAll('loading');
            layer.msg('保存失败');
        },
        success: function (data) {
            var isLogon = data.isLogon;
            if (isLogon === true) {
                layer.closeAll('loading');
                layer.msg('保存成功');
                document.location.href = '/Cinema/Detail';
            } else {
            layer.closeAll('loading');
                layer.msg('保存失败');
            }
        }
    };
    $('#formEditCinema').ajaxSubmit(logonFormOptions);
}
//上映信息
function addProscenium() {
    
    layer.load();
    var stat = $('.StatTime');
    var end = $('.EndTime');
    var startTime ='';
    var endTime = '';
    for (var i = 0; i < stat.length; i++) {
        startTime += stat[i].value + ',';
    }
    for (var j = 0; j < end.length; j++) {
        endTime += end[j].value+',';
    }
    startTime = startTime.substring(0, startTime.length - 1);
    endTime = endTime.substring(0, endTime.length - 1);
    var logonFormOptions = {
        dataType: 'json',
        data: { startTime: startTime, endendTime: endTime },
        error: function (request) {
            layer.closeAll('loading');
            layer.msg('添加失败');
        },
        success: function (data) {
            var isLogon = data.isLogon;
            if (isLogon === true) {
                layer.closeAll('loading');
                layer.msg('添加成功');
                document.location.href = '/Proscenium/index';
            } else {
                layer.closeAll('loading');
                layer.msg('添加失败失败');
            }
        }
    };
    $('#formProsceniumInfo').ajaxSubmit(logonFormOptions);
}