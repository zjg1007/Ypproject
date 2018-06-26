// 配置控制器
angular.module('starter.controllers', [])

    // 行业要闻控制器
    .controller('CurrentAfairsCtrl', function ($http, $ionicLoading, $scope, $state, Articles, $rootScope) {
        $ionicLoading.show({
            template: 'Loading...'
        });
        $scope.title = "电影";
        //$scope.currentAffairItems = CurrentAffairItems.all();
        //$scope.currentAffairItems = [];
        //Articles.getAll().then(function (respsone) {
        //    $scope.currentAffairItems = respsone.data;
        //});
        $scope.IsHitList = [];
        $scope.ComingSoon = [];
        //正在热映
        var getString1 = 'http://pengcheng.ngrok.cc/api/article/IsHitList';
        $http.get(getString1).success(function (data) {
            $scope.IsHitList = data;
        })
        //即将上映
        var getString2 = 'http://pengcheng.ngrok.cc/api/article/ComingSoon';
        $http.get(getString2).success(function (data) {
            $scope.ComingSoon = data;
        })
        //搜索
        $scope.searchMocie = function () {
            var name = document.getElementById('searchpar');
            $state.go('tab.movieSearch', { moviename: name.value });
        }
        //查看电影信息
        $scope.seleMovieindex = function (id) {
            $state.go('tab.MovieDetailCtrl', { movieid: id });
        }
        //选项卡 start
        var mySwiper = new Swiper('.swiper-container', {
            onSlideChangeEnd: function (swiper) {
                var j = mySwiper.activeIndex;
                $('.maple-tab li, .maple-tab2 li').removeClass('active').eq(j).addClass('active');
            }
        })
        /*列表切换*/
        $('.maple-tab li, .maple-tab2 li').on('click', function (e) {
            e.preventDefault();
            //得到当前索引
            var i = $(this).index();
            $('.maple-tab li, .maple-tab2 li').removeClass('active').eq(i).addClass('active');
            mySwiper.slideTo(i, 1000, false);
        });
        //选项卡 end
        //下拉刷新 start

        $scope.doRefresh = function () {
            $http.get(getString1).success(function (data) {
                $scope.IsHitList = data;
            }).finally(function () {
                $scope.$broadcast('scroll.refreshComplete');
            });
            $http.get(getString2).success(function (data) {
                $scope.ComingSoon = data;
            }).finally(function () {
                    $scope.$broadcast('scroll.refreshComplete');
                });
        };
        //下拉刷新  end
        //定位 start
        $ionicLoading.hide();
        //定位 end
    })  
    // 电影明细
    .controller('currentAfairsDetailCtrl', function ($ionicLoading, $scope, $state, $stateParams, Articles, $rootScope) {
        $ionicLoading.show({
            template: 'Loading...'
        });
        //$scope.item = CurrentAffairItems.get($stateParams.currentAfairsId);
        $scope.selectTabWithIndex = function (index) {
            $ionicTabsDelegate.select(index);
        }
        Articles.getDetail($stateParams.currentAfairsId).then(function (respsone) {
            $scope.item = respsone.data;
            $scope.name = respsone.data[0].getReadyplayList[0].movieName;//电影名称
            var arr = new Array();
            for (var i = 0; i < respsone.data.length; i++) {
                arr.push(respsone.data[i].time);
            }
            $scope.Time = arr;
        });
        //购票选上映时间段
        $scope.locationMovie = function (Cinemaid) {
            $rootScope.cinemaid = Cinemaid;
            $state.go('tab.movieIndex', { movieid: $stateParams.currentAfairsId });
        }
        $ionicLoading.hide();
    })

    //根据电影名称搜索电影
     .controller('movieSearchCtrl', function ($ionicLoading, $http, $scope, $state, $stateParams, Articles, $rootScope) {
         $ionicLoading.show({
             template: 'Loading...'
         });
         $scope.title = $stateParams.moviename;
         
         var url = 'http://pengcheng.ngrok.cc/api/article/GetSearchMovieAll';
         $http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";
         $http({
             method: "POST",
             url: url,
             data: $.param({ moviename: $stateParams.moviename}),
             async: false,
             dataType: 'json'
         }).success(function (data, status) {
             if (data.length == 0) {
                 $scope.message = '已经很努力帮您找了,还是找不到.';
                 $scope.items = null;
             } else {
                 $scope.items = data;
                 $scope.message = null;
             }
         }).error(function (data, status) {
             alert('失败');
         });
         $scope.selectmovieInfo = function (movieid) {
             $state.go('tab.MovieDetailCtrl', { movieid: movieid });
         }
         $scope.searchMociepar = function () {
             var value = document.getElementById('search');
             $http({
                 method: "POST",
                 url: url,
                 data: $.param({ moviename:value.value }),
                 async: false,
                 dataType: 'json'
             }).success(function (data, status) {
                 if (data.length == 0) {
                     $scope.message = '已经很努力帮您找了,还是找不到.';
                     $scope.items = null;
                 } else {
                     $scope.message = null;
                     $scope.items = data;
                 }
                 $ionicLoading.hide();
                 
             }).error(function (data, status) {
                 alert('失败');
             });
         }
         $ionicLoading.hide();
     })

    // 影院列表
    .controller('OpinionLeadersCtrl', function ($http,$ionicLoading, $scope, Cinema) {
        $ionicLoading.show({
            template: 'Loading...'
        });
        $scope.title = "影院";
        $scope.CinemaListAll = [];
        Cinema.getAll().then(function (respsone) {
            $scope.CinemaListAll = respsone.data;
        });
        //下拉刷新 start
        $scope.doRefresh = function () {
            var getString = 'http://pengcheng.ngrok.cc/api/article/GetCinemaList';
            $http.get(getString).success(function (data) {
                $scope.CinemaListAll = data;
            })
                .finally(function () {
                    $scope.$broadcast('scroll.refreshComplete');
                });
        };
        //下拉刷新  end
        $ionicLoading.hide();
    })

    // 查该影院所有电影
    .controller('MovieByCinemaIDCtrl', function ($ionicLoading, $rootScope, $state, $scope, $stateParams, Cinema) {
        $ionicLoading.show({
            template: 'Loading...'
        });
        $scope.title = "影院";
        Cinema.getDetail($stateParams.cinemaid).then(function (respsone) {
            $scope.movieListByCinemaID = respsone.data;
        });
        //查看电影信息
        $scope.selectmovie = function (id) {
            $rootScope.cinemaid = $stateParams.cinemaid;
            $state.go('tab.CinemaIndex', { movieid: id });
        }
        //购票选上映时间段
        $scope.paymovieDate = function (movieid) {
            $rootScope.cinemaid = $stateParams.cinemaid;
            $state.go('tab.CinemaIndex', { movieid: movieid });
        }
        $ionicLoading.hide();
    })
    //影院列表（购票）
     .controller('CinemaIndexCtrl', function ($state,$ionicLoading, $scope, $rootScope, $stateParams, Articles, $http) {
         $ionicLoading.show({
             template: 'Loading...'
         });
         //上映信息
         var getString = 'http://pengcheng.ngrok.cc/api/article/GettMoList';
         $http.get(getString + '?movieid=' + $stateParams.movieid + '&&cinemid=' + $rootScope.cinemaid).success(function (data) {
             $scope.items = data;
             $scope.name = data[0].getReadyplayList[0].movieName;//电影名称
             $scope.CinemaName = data[0].getReadyplayList[0].cinemaName;//影院名称
             $scope.CinemaSite = data[0].getReadyplayList[0].cinemaSite;//影院地址
             $scope.CinemaServe = data[0].getReadyplayList[0].cinemaServe;//服务
             $scope.MoviePicture = data[0].getReadyplayList[0].moviePicture;//电影图片
             $scope.MovieGrade = data[0].getReadyplayList[0].movieGrade;//电影评分
         })
         //购票
         $scope.paymovie = function (id) {
             $state.go('tab.CnSelectSeat', { id: id });
         }
         $ionicLoading.hide();
     })
    //影院列表（选座）
     .controller('CnSelectSeatCtrl', function ($state, $rootScope, $http, $ionicPopup, $scope, $stateParams) {
          
             var attr = new Array();
             var pricevalue = '';

             var getString = 'http://pengcheng.ngrok.cc/api/article/SelectSeat';
             $http.get(getString + '?id=' + $stateParams.id).success(function (data) {
                 //本地存储-username
                 var storage = window.localStorage;
                 $scope.items = data;
                 attr = data[0].homePlace.split(',');
                 $scope.price = data[0].price;
                 pricevalue = data[0].price;
                 $scope.username = storage.getItem("username");
                 $rootScope.readid = $stateParams.id;
             })

            
             //选座
             $scope.zuowei = function () {
                 //选座 start
                 var ulcount = $('.place ul.clearfix');
                 var licount = $('.place ul.clearfix li');
                 var str = new Array();
                 for (var i = 1; i <= ulcount.size() ; i++) {
                     for (var j = 1; j <= 12; j++) {
                         str.push(i + '排' + j + '座');
                     }
                 }
                 $.each(str, function (i, val) {
                     var txt = $('<div style="display:none"></div>').text(val);
                     licount.eq(i).append(txt);
                 });
                 //选座 end
                 $.each(attr, function (i, val) {
                     if (val == 'True') {
                         licount.eq(i).attr('disabled', "true");
                         licount.eq(i).css('background', '#666');
                     } else if (val == 'ok') {
                         licount.eq(i).addClass("select");
                         licount.eq(i).attr('disabled', "true");
                     }
                 });
                 $('.place ul.clearfix li').on('click', function (e) {
                     e.preventDefault();
                     if ($(this).attr('disabled') != 'disabled') {
                         $(this).toggleClass('hover');
                         $(this).find('div').stop().toggle();
                         if ($(".place ul.clearfix li[class='hover']").size() > 4) {
                             $(this).toggleClass('hover');
                             $(this).find('div').stop().hide();
                             //一个提示对话框
                             var alertPopup = $ionicPopup.alert({
                                 title: '约票提醒您!',
                                 template: '一个用户只能购4张票'
                             });
                         }
                     }
                     $('.money').html('票价' + $(".place ul.clearfix li[class='hover']").size() * pricevalue + '元');
                     $rootScope.price = ($(".place ul.clearfix li[class='hover']").size() * pricevalue) + ($(".place ul.clearfix li[class='hover']").size() * 3);
                 });
             }
             //提交订单信息
             $scope.subSeat = function () {
                 //获取座位 start
                 var Sear = $(".place ul.clearfix li[class='hover']");
                 var seararrty = new Array();
                 Sear.each(function (e) {
                     seararrty.push($(this).find('div').html());
                 });
                 $rootScope.Sear = seararrty.join(',');
                 //获取座位  end
                 //位置选择录入 start
                 var oliSear = new Array();
                 var oli = $('.place ul.clearfix li');
                 oli.each(function () {
                     if ($(this).attr('class') == 'hover' || $(this).attr('class') == 'select') {
                         oliSear.push("ok");
                     } else if ($(this).attr('disabled') == 'disabled') {
                         oliSear.push("True");
                     } else {
                         oliSear.push("False");
                     }
                 });
                 $rootScope.selectSeart = oliSear.join(',');
                 //位置选择录入 end
                 $state.go('tab.CnIndentsub', { id: $stateParams.id });
             }
        
     })

      // 提交订单
    .controller('CnIndentsubCtrl', function ($ionicLoading, $state, $rootScope, $http, $ionicPopup, $scope, $stateParams) {
        var getString = 'http://pengcheng.ngrok.cc/api/article/SelectSeat';
        var price = $rootScope.price;
        var selectSeart = $rootScope.selectSeart;
        var Sear = $rootScope.Sear;

        $rootScope.price = price;
        $rootScope.selectSeart = selectSeart;
        $rootScope.Sear = Sear;
        $rootScope.id = $stateParams.id;
        $http.get(getString + '?id=' + $stateParams.id).success(function (data) {
            //本地存储-username
            var storage = window.localStorage;
            $scope.items = data;
            $scope.Sear = $rootScope.Sear.replace(new RegExp(',', 'gm'), '');//座位
            $scope.count = $rootScope.Sear.split(',').length;
            $scope.selectSeart = $rootScope.selectSeart;//位置信息
            $scope.price = $rootScope.price;//价格
        })
        //本地存储-username
        var storage = window.localStorage;
        $scope.submSeart = function () {
            $ionicLoading.show({
                template: 'Loading...'
            });

            //提交
            var getString = 'http://pengcheng.ngrok.cc/api/article/subDealInfo';
            $http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";
            $http({
                method: "POST",
                url: getString,
                data: $.param({
                    RealityMone: price,
                    Site: Sear,
                    id: $stateParams.id,
                    username: storage.getItem("username"),
                    Place: selectSeart
                }),
                async: false,
                dataType: 'json'
            }).success(function (data, status) {
                if (data.isLogon) {
                    $rootScope.GainNumber = data.standby;
                    //$ionicPopup.alert({
                    //    title: '约票温馨提示！',
                    //    template: data.message
                    //});
                    $state.go('tab.Cncomplete', { id: $stateParams.id });

                } else {
                    $ionicPopup.alert({
                        title: '约票温馨提示！',
                        template: data.message
                    });
                }
                $ionicLoading.hide();
            }).error(function (data, status) {
                $ionicLoading.hide();
                $ionicPopup.alert({
                    title: '请您检查网络！',
                    template: data.message
                });
            });
        }
    })
     // 完成订单
    .controller('CncompleteCtrl', function ($ionicLoading, $state, $rootScope, $http, $ionicPopup, $scope, $stateParams) {
        var getString = 'http://pengcheng.ngrok.cc/api/article/SelectSeat';
        var price = $rootScope.price;
        var Sear = $rootScope.Sear;
        $http.get(getString + '?id=' + $rootScope.id).success(function (data) {
            //本地存储-username 
            var storage = window.localStorage;
            $scope.items = data;
            $scope.Sear = Sear.replace(new RegExp(',', 'gm'), ' ');//座位
            $scope.price = price;//价格
            $scope.GainNumber = $rootScope.GainNumber.replace(new RegExp(',', 'gm'), '  |  ');;//约票号
        })
    })
    //根据id查电影信息-index
      .controller('MovieDetailCtrl', function ($http,$ionicPopup, $ionicLoading, $scope, $state, $stateParams, Articles, $rootScope) {
          $ionicLoading.show({
              template: 'Loading...'
          });
          //获取信息
          Articles.getAllByTypeID($stateParams.movieid).then(function (respsone) {
              $scope.items = respsone.data;
          });
          $scope.paymovie = function () {
              $state.go('tab.currentAfairs-detail', { currentAfairsId: $stateParams.movieid });
          }
          $scope.shangyinginfo = function () {
              $state.go('tab.Release', { movieid: $stateParams.movieid });
          }
          //想看的电影
          
          //本地存储-username
          var storage = window.localStorage;
          
          $scope.WantLook = function () {
              var url = 'http://pengcheng.ngrok.cc/api/article/WantMovie';
              $http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";
              $http({
                  method: "POST",
                  url: url,
                  data: $.param({ id: $stateParams.movieid, username: storage.getItem("username") }),
                  async: false,
                  dataType: 'json'
              }).success(function (data, status) {
                  if (data.isLogon) {
                      $ionicPopup.alert({
                          title: '约票温馨提示！',
                          template: data.message
                      });
                  } else {
                      $ionicPopup.alert({
                          title: '约票温馨提示！',
                          template: data.message
                      });
                  }
                  $ionicLoading.hide();

              }).error(function (data, status) {
                  alert('失败');
              });
          }
          //测试页面加载完成事件
          var mySwiper3 = new Swiper('.swiper-container3', {
              resistanceRatio: 0.5,//抵抗率。边缘抵抗力的大小比例。值越小抵抗越大越难将slide拖离边缘，0时完全无法拖离。
              slidesPerView: 3,//可以设置为number或者 'auto'则自动根据slides的宽度来设定数量。
              spaceBetween: 10,//slide之间的距离（单位px）。
              resistance: true, //继续拖动Swiper会离开边界，释放后弹回。
              preloadImages: true,//默认为true，Swiper会强制加载所有图片。
              updateOnImagesReady: true,//当所有的内嵌图像（img标签）加载完成后Swiper会重新初始化。使用此选项需要先开启preloadImages: true
              observer: true,//修改swiper自己或子元素时，自动初始化swiper
              observeParents: true,//修改swiper的父元素时，自动初始化swiper
          });
          $ionicLoading.hide();
      })
     // 电影上映信息
    .controller('movieIndexCtrl', function ($state,$ionicLoading, $scope, $rootScope, $stateParams, Articles, $http) {
        $ionicLoading.show({
            template: 'Loading...'
        });
        var getString = 'http://pengcheng.ngrok.cc/api/article/GettMoList';
        $http.get(getString + '?movieid=' + $stateParams.movieid + '&&cinemid=' + $rootScope.cinemaid).success(function (data) {
            $scope.items = data;
            $scope.name = data[0].getReadyplayList[0].movieName;//电影名称
            $scope.CinemaName = data[0].getReadyplayList[0].cinemaName;//影院名称
            $scope.CinemaSite = data[0].getReadyplayList[0].cinemaSite;//影院地址
            $scope.CinemaServe = data[0].getReadyplayList[0].cinemaServe;//服务
            $scope.MoviePicture = data[0].getReadyplayList[0].moviePicture;//电影图片
            $scope.MovieGrade = data[0].getReadyplayList[0].movieGrade;//电影评分
        })
        $scope.paymovie = function (id) {
            $state.go('tab.SelectSeat', { id: id });
        }
        $ionicLoading.hide();
    })
    // 上映信息
    .controller('ReleaseCtrl', function ($ionicLoading, $scope, $stateParams, Cinema, Articles) {
        $ionicLoading.show({
            template: 'Loading...'
        });

        $scope.title = "影院";
        Articles.getMovieByName($stateParams.movieid).then(function (respsone) {
            $scope.releaseInfo = respsone.data;
        });
        $ionicLoading.hide();
    })
    // 查看个人信息
    .controller('TopicCtrl', function ($http,$ionicLoading, $scope, User) {
        $ionicLoading.show({
            template: 'Loading...'
        });
        //本地存储-username
        var storage = window.localStorage;
        //注销
        $scope.logout = function () {
            storage.clear();
            window.location.href = '../index.html';
        }

        //根据用户名获取用户信息
        var url = 'http://pengcheng.ngrok.cc/api/Account/PostSelectInfo';

        $http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";
          //下拉刷新 start

        $scope.doRefresh = function () {
            $http({
                method: "POST",
                url: url,
                data: $.param({ username: storage.getItem("username") }),
                async: false,
                dataType: 'json'
            }).success(function (data, status) {
                $scope.item = data;
                $ionicLoading.hide();
            }).error(function (data, status) {
                alert('失败');
            }).finally(function () {
                    $scope.$broadcast('scroll.refreshComplete');
                });
        };
        //下拉刷新  end
        $http({
            method: "POST",
            url: url,
            data: $.param({ username: storage.getItem("username") }),
            async: false,
            dataType: 'json'
        }).success(function (data, status) {
            $scope.item = data;
            $ionicLoading.hide();
        }).error(function (data, status) {
            alert('失败');
        });
    })
      // 查看个人信息-修改
    .controller('UserIndexCtrl', function ($ionicPopup, $stateParams, $http, $ionicLoading, $scope, User, $ionicActionSheet) {
       
        $ionicLoading.show({
            template: 'Loading...'
        });
        //点击上传图片,提示选择上传还是拍照
        $scope.selectImg = function () {
            var hideSheet = $ionicActionSheet.show({
                buttons: [{
                    text: '相册'
                }, {
                    text: '拍照'
                }
                ],
                titleText: '选择图片',
                cancelText: '取消',
                cancel: function () {
                    // add cancel code..
                },
                buttonClicked: function (index) {
                    navigator.camera.getPicture(cameraSuccess, cameraError, {
                        sourceType: index
                    }); //调用系统相册、拍照
                }
            });
        }
        //根据选项执行功能
        function cameraSuccess(img) {
            $scope.img = img;//这里返回的img是选择的图片的地址，可以直接赋给img标签的src，就能显示了
            window.resolveLocalFileSystemURL(img, function success(fileEntry) {
                upload(fileEntry.toInternalURL());//将获取的文件地址转换成file transfer插件需要的绝对地址
            }, function () {
                alert("上传失败");
            });
        }

        function cameraError(img) {
            alert("上传失败");
        }

        function upload(fileURL) {//上传图片
            var win = function (r) {//成功回调方法
                var response = JSON.parse(r.response);//你的上传接口返回的数据
                if (response.datas.state) {
                    alert("修改成功");
                } else {
                    alert(response.datas.error);
                }
            }
            var fail = function (error) {//失败回调方法
                alert("上传失败");
            }

            var options = new FileUploadOptions();
            options.fileKey = "pic";//这是你的上传接口的文件标识，服务器通过这个标识获取文件
            options.fileName = fileURL.substr(fileURL.lastIndexOf('/') + 1);
            options.mimeType = "image/gif";//图片

            var ft = new FileTransfer();
            ft.upload(fileURL, encodeURI('uploadurl'), win, fail, options);//开始上传，uoloadurl是你的上传接口地址
        }

        //本地存储-username
            var storage = window.localStorage;
        //根据用户名获取用户信息
            var url = 'http://pengcheng.ngrok.cc/api/Account/PostSelectInfo';
        $http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";
        $http({
            method: "POST",
            url: url,
            data: $.param({ username:storage.getItem("username")}),
            async: false,
            dataType: 'json'
        }).success(function (data, status) {
            $scope.items = data;
            $scope.img = data.headimg;
            $ionicLoading.hide();
        }).error(function (data, status) {
            alert('失败');
            $ionicLoading.hide();
        });
        //保存
        $scope.submib = function () {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var _url = 'http://pengcheng.ngrok.cc/api/Account/subUserInfo';
            var name=document.getElementById('uname').value;
            var even = document.getElementById('evainfo').value;
            var img = $('#imgsrc').attr('src');
            $http({
                method: "POST",
                url: _url,
                data: $.param({
                    phonr: storage.getItem("username"),
                    name: name, evaluation: even,
                    headimg: img
                }),
                async: false,
                dataType: 'json'
            }).success(function (data, status) {
                if (data.isLogon) {
                    $ionicPopup.alert({
                        title: '约票温馨提示！',
                        template: '保存成功'
                    });
                } else {
                    $ionicPopup.alert({
                        title: '约票温馨提示！',
                        template: '保存失败'
                    });
                }
                $ionicLoading.hide();
            }).error(function (data, status) {
                $ionicLoading.hide();
                alert('失败');

            });
        }
    })

     // 修改密码
    .controller('updatePasswordCtrl', function ($ionicPopup,$http, $ionicLoading, $scope, User) {
        $ionicLoading.show({
            template: 'Loading...'
        });

        //本地存储-username
        var storage = window.localStorage;
       
        //根据用户名获取用户信息
        var url = 'http://pengcheng.ngrok.cc/api/Account/updatePassword';
        $http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";
        $scope.subuserpassword=function(){
            $ionicLoading.show({
                template: 'Loading...'
            });
            var password= document.getElementById('password').value;
            var newpassword=document.getElementById('newpassword').value;
            var _newpassword=document.getElementById('_newpassword').value;
            if(password==newpassword||password==_newpassword){
                $ionicPopup.alert({
                    title: '约票温馨提示！',
                    template: '原密码不能与新密码相同'
                });
                $ionicLoading.hide();
            }else if(newpassword!=_newpassword){
                $ionicPopup.alert({
                    title: '约票温馨提示！',
                    template: '新密码要一致,请重新输入.'
                });
                $ionicLoading.hide();
            } else if (password == '' || newpassword == '' || _newpassword == '') {
                $ionicPopup.alert({
                    title: '约票温馨提示！',
                    template: '输入信息不能为空,请重新输入'
                });
                $ionicLoading.hide();
            } else if ( newpassword.length < 6 || _newpassword.length < 6) {
                $ionicPopup.alert({
                    title: '约票温馨提示！',
                    template: '密码起码要6位数,请重新输入'
                });
                $ionicLoading.hide();
            }
            else {
                $http({
                    method: "POST",
                    url: url,
                    data: $.param({
                        username: storage.getItem("username"),
                        password: password,
                        newpassword: newpassword
                    }),
                    async: false,
                    dataType: 'json'
                }).success(function (data, status) {
                    if (data.isLogon) {
                        $ionicPopup.alert({
                            title: '约票温馨提示！',
                            template: data.message
                        });
                        storage.clear();
                        window.location.href = '../index.html';
                    } else {
                        $ionicPopup.alert({
                            title: '约票温馨提示！',
                            template: data.message
                        });
                    }
                    $ionicLoading.hide();
                }).error(function (data, status) {
                    $ionicLoading.hide();
                    alert('失败');
                });
            }
            
        }
        $ionicLoading.hide();
       
    })

    // 看过电影
    .controller('SeenMovieCtrl', function ($http, $ionicLoading, $scope, User, $state) {
        $ionicLoading.show({
            template: 'Loading...'
        });
        //本地存储-username
        var storage = window.localStorage;
        //根据用户名获取用户信息
        var url = 'http://pengcheng.ngrok.cc/api/article/PostLookMovie';
        $http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";
            $http({
                method: "POST",
                url: url,
                data: $.param({ username: storage.getItem("username") }),
                async: false,
                dataType: 'json'
            }).success(function (data, status) {
                $scope.items = data;
                $ionicLoading.hide();
            }).error(function (data, status) {
                alert('失败');
            }).finally(function () {
                $scope.$broadcast('scroll.refreshComplete');
            });

        //查看电影信息
            $scope.seleMovieindex = function (id) {
                $state.go('tab.UDetail', { movieid: id });
            }
    })
    // 看过电影(列表)
    .controller('UDetailCtrl', function ($state, $stateParams, $http, $ionicLoading, $scope, User, Articles) {
        $ionicLoading.show({
            template: 'Loading...'
        });
        //获取信息
        Articles.getAllByTypeID($stateParams.movieid).then(function (respsone) {
            $scope.items = respsone.data;
        });
        $scope.shangyinginfo = function () {
            $state.go('tab.URelease', { movieid: $stateParams.movieid });
        }
        //测试页面加载完成事件
        var mySwiper3 = new Swiper('.swiper-container3', {
            resistanceRatio: 0.5,//抵抗率。边缘抵抗力的大小比例。值越小抵抗越大越难将slide拖离边缘，0时完全无法拖离。
            slidesPerView: 3,//可以设置为number或者 'auto'则自动根据slides的宽度来设定数量。
            spaceBetween: 10,//slide之间的距离（单位px）。
            resistance: true, //继续拖动Swiper会离开边界，释放后弹回。
            preloadImages: true,//默认为true，Swiper会强制加载所有图片。
            updateOnImagesReady: true,//当所有的内嵌图像（img标签）加载完成后Swiper会重新初始化。使用此选项需要先开启preloadImages: true
            observer: true,//修改swiper自己或子元素时，自动初始化swiper
            observeParents: true,//修改swiper的父元素时，自动初始化swiper
        });
        $ionicLoading.hide();
    })
    //想看电影
     .controller('WantMovieCtrl', function ($state, $stateParams, $http, $ionicLoading, $scope, User, Articles) {
         $ionicLoading.show({
             template: 'Loading...'
         });
         //获取信息
         //本地存储-username
         var storage = window.localStorage;
         //根据用户名获取用户信息
         var url = 'http://pengcheng.ngrok.cc/api/article/WantMovieList?username=' + storage.getItem("username");
         $http.get(url).success(function (data) {
             $scope.items = data;
         })
               .finally(function () {
                   $scope.$broadcast('scroll.refreshComplete');
               });
         $scope.seleMovieindex = function (id) {
             $state.go('tab.UDetail', { movieid: id });
         }
         $ionicLoading.hide();
     })
    // 上映信息
    .controller('UReleaseCtrl', function ($ionicLoading, $scope, $stateParams, Cinema, Articles) {
        $ionicLoading.show({
            template: 'Loading...'
        });

        $scope.title = "影院";
        Articles.getMovieByName($stateParams.movieid).then(function (respsone) {
            $scope.releaseInfo = respsone.data;
        });
        $ionicLoading.hide();
    })
    // 选座
    .controller('AbroadNewsCtrl', function ($scope) {
        $scope.$watch('$viewContentLoaded', function () {
            $('.place ul.clearfix li').click(function () {
                $(this).css('background','red');
            });
        });
    })
    // 选座
    .controller('SelectSeatCtrl', function ($state,$rootScope, $http, $ionicPopup, $scope, $stateParams) {
        $scope.$watch('$viewContentLoaded', function () {
            var attr = new Array();
            var pricevalue = '';
            
            var getString = 'http://pengcheng.ngrok.cc/api/article/SelectSeat';
            $http.get(getString + '?id=' + $stateParams.id).success(function (data) {
                //本地存储-username
                var storage = window.localStorage;
                $scope.items = data;
                attr = data[0].homePlace.split(',');
                $scope.price = data[0].price;
                pricevalue = data[0].price;
                $scope.username = storage.getItem("username");
                $rootScope.readid = $stateParams.id;
            })
            //选座
            $scope.zuowei = function () {
                //选座 start
                var ulcount = $('.place ul.clearfix');
                var licount = $('.place ul.clearfix li');
                var str = new Array();
                for (var i = 1; i <= ulcount.size() ; i++) {
                    for (var j = 1; j <= 12; j++) {
                        str.push(i + '排' + j + '座');
                    }
                }
                $.each(str, function (i, val) {
                    var txt = $('<div style="display:none"></div>').text(val);
                    licount.eq(i).append(txt);
                });
                $.each(attr, function (i,val) {
                    if (val == 'True') {
                        licount.eq(i).attr('disabled', "true");
                        licount.eq(i).css('background', '#666');
                    } else if (val == 'ok') {
                        licount.eq(i).addClass("select");
                        licount.eq(i).attr('disabled', "true");
                    }
                });

                $('.place ul.clearfix li').on('click', function (e) {
                    e.preventDefault();
                    if ($(this).attr('disabled') != 'disabled') {
                        $(this).toggleClass('hover');
                        $(this).find('div').stop().toggle();
                        if ($(".place ul.clearfix li[class='hover']").size() > 4) {
                            $(this).toggleClass('hover');
                            $(this).find('div').stop().hide();
                            //一个提示对话框
                            var alertPopup = $ionicPopup.alert({
                                title: '约票提醒您!',
                                template: '一个用户只能购4张票'
                            });
                        }
                    }
                    $('.money').html('票价' + $(".place ul.clearfix li[class='hover']").size() * pricevalue + '元');
                    $rootScope.price = ($(".place ul.clearfix li[class='hover']").size() * pricevalue) + ($(".place ul.clearfix li[class='hover']").size() * 3);
                });
            }
            //提交订单信息
            $scope.subSeat = function () {
                //获取座位 start
                var Sear = $(".place ul.clearfix li[class='hover']");
                var seararrty = new Array();
                Sear.each(function (e) {
                    seararrty.push($(this).find('div').html());
                });
                $rootScope.Sear = seararrty.join(',');
                //获取座位  end
                //位置选择录入 start
                var oliSear = new Array();
                var oli = $('.place ul.clearfix li');
                oli.each(function () {
                    if ($(this).attr('class') == 'hover' || $(this).attr('class')=='select') {
                        oliSear.push("ok");
                    } else if ($(this).attr('disabled') == 'disabled') {
                        oliSear.push("True");
                    } else {
                        oliSear.push("False");
                    }
                });
                $rootScope.selectSeart = oliSear.join(',');
                //位置选择录入 end
                $state.go('tab.Indentsub', { id: $stateParams.id });
            }
        });
    })

     // 提交订单
    .controller('IndentsubCtrl', function ($ionicLoading,$state, $rootScope, $http, $ionicPopup, $scope, $stateParams) {
        var getString = 'http://pengcheng.ngrok.cc/api/article/SelectSeat';
        var price = $rootScope.price;
        var selectSeart = $rootScope.selectSeart;
        var Sear = $rootScope.Sear;

        $rootScope.price = price;
        $rootScope.selectSeart = selectSeart;
        $rootScope.Sear = Sear;
        $rootScope.id = $stateParams.id;
        $http.get(getString + '?id=' + $stateParams.id).success(function (data) {
            //本地存储-username
            var storage = window.localStorage;
            $scope.items = data;
            $scope.Sear = $rootScope.Sear.replace(new RegExp(',', 'gm'), '');//座位
            $scope.count = $rootScope.Sear.split(',').length;
            $scope.selectSeart = $rootScope.selectSeart;//位置信息
            $scope.price = $rootScope.price;//价格
        })
        //本地存储-username
        var storage = window.localStorage;
        $scope.submSeart = function () {
            $ionicLoading.show({
                template: 'Loading...'
            });

            //提交
            var getString = 'http://pengcheng.ngrok.cc/api/article/subDealInfo';
            $http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";
            $http({
                method: "POST",
                url: getString,
                data: $.param({
                    RealityMone: price,
                    Site: Sear,
                    id: $stateParams.id,
                    username: storage.getItem("username"),
                    Place: selectSeart
                }),
                async: false,
                dataType: 'json'
            }).success(function (data, status) {
                if (data.isLogon) {
                    $rootScope.GainNumber = data.standby;
                    $state.go('tab.complete', { id: $stateParams.id });

                } else {
                    $ionicPopup.alert({
                        title: '约票温馨提示！',
                        template: data.message
                    });
                }
                $ionicLoading.hide();
            }).error(function (data, status) {
                $ionicLoading.hide();
                $ionicPopup.alert({
                    title: '请您检查网络！',
                    template: data.message
                });
            });
        }
    })
    // 完成订单
    .controller('completeCtrl', function ($ionicLoading, $state, $rootScope, $http, $ionicPopup, $scope, $stateParams) {
        var getString = 'http://pengcheng.ngrok.cc/api/article/SelectSeat';
        var price = $rootScope.price;
        var Sear = $rootScope.Sear;
        $http.get(getString + '?id=' + $rootScope.id).success(function (data) {
            //本地存储-username 
            var storage = window.localStorage;  
            $scope.items = data;
            $scope.Sear = Sear.replace(new RegExp(',', 'gm'), ' ');//座位
            $scope.price = price;//价格
            $scope.GainNumber = $rootScope.GainNumber.replace(new RegExp(',', 'gm'), '  |  ');//约票号
        })
    })
    // 科技前沿
    .controller('FrontLineOfTechnologyCtrl', function ($scope) {
        $scope.title = "<div>科技前沿</div>";
    })
   // 我的电影票
    .controller('MovieWritCtrl', function ($state,$rootScope,$http,$scope) {
        //本地存储-username
        var storage = window.localStorage;
        $scope.seleInfo = function (readid, dealid) {
            $rootScope.dealid = dealid;
            $state.go('tab.Ucompletel', { id: readid });
        }
        //提交
        var getString = 'http://pengcheng.ngrok.cc/api/article/MovieWrit';
        $http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";
        $http({
            method: "POST",
            url: getString,
            data: $.param({
                username: storage.getItem("username")
            }),
            async: false,
            dataType: 'json'
        }).success(function (data, status) {
            $scope.items = data;
        }).error(function (data, status) {
            alert('请检查您的网络');
        });
    })

 // 电影票详细
    .controller('UcompletelCtrl', function ($http, $scope, $rootScope, $stateParams) {
        //本地存储-username
        var storage = window.localStorage;
       
        //提交
        var getString = 'http://pengcheng.ngrok.cc/api/article/SelectDealInfo';
        $http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";
        $http({
            method: "POST",
            url: getString,
            data: $.param({
                id: parseInt($stateParams.id),
                Indent: parseInt($rootScope.dealid)
            }),
            async: false,
            dataType: 'json'
        }).success(function (data, status) {
            $scope.item = data;
            $scope.Seat = data.dealSite.replace(new RegExp(',', 'gm'), ' ');//座位
            $scope.GainNumber = data.dealGainNumber.replace(new RegExp(',', 'gm'), '  |  ');//票号
        }).error(function (data, status) {
            alert('请检查您的网络');
        });
    })
