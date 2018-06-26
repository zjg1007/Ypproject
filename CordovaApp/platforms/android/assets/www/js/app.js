angular.module('starter', ['ionic', 'starter.controllers', 'starter.services'])
.run(function ($ionicPlatform, $ionicHistory, $ionicPopup) {
    //设备加载检查完成之后做的一些基本初始化工作
    $ionicPlatform.ready(function () {
        // 缺省情况下隐藏设备的操作条
        if (cordova.platformId === 'ios' && window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
            cordova.plugins.Keyboard.disableScroll(true);
        }
        // 关闭设备缺省的状态条
        if (window.StatusBar) {
            //StatusBar.hide();
        }

        $ionicPlatform.registerBackButtonAction(function (event) {
            if ($ionicHistory.currentStateName() === 'tab.currentAfairs') {
                showConfirm();
                function showConfirm() {
                    var confirmPopup = $ionicPopup.confirm({
                        title: '<strong>退出提示</strong>',
                        template: '请你确定是否退出应用程序',
                        okText: '退出',
                        cancelText: '取消'
                    });

                    confirmPopup.then(function (res) {
                        if (res) {
                            ionic.Platform.exitApp();
                        }
                        else {
                        }
                    });
                }
                return false;
            } else {
                $ionicHistory.goBack();
            }
        }, 100);

    });
})

.config(function ($httpProvider,$stateProvider, $urlRouterProvider) {
    //修改配置访问后台可以用对象接受数据 start


    // Ionic 应用 AngularUI Router 中所使用的状态机概念，设置 app 所处的不同状态，
    // 每个状态的控制器可以在 controllers.js 中找到。
    // 参考资料：https://github.com/angular-ui/ui-router
    $stateProvider
        // 配置抽象的的入口路由，这样，后续定义的控制器路由都将自动加上这个前缀：#/tab
        .state('tab', {
            url: '/tab',
            abstract: true,
            templateUrl: 'templates/tabs.html'
        })

        // 电影列表
        .state('tab.currentAfairs', {
            url: '/currentAfairs',
            views: {
                'tab-currentAfairs': {  // 视图名称
                    templateUrl: 'templates/currentAfairs.html', // 视图对应的 html 文件
                    controller: 'CurrentAfairsCtrl' // 控制器名称
                }
            }
        })
        // 电影的影院列表
        .state('tab.currentAfairs-detail', {
            url: '/currentAfairs/:currentAfairsId',
            views: {
                'tab-currentAfairs': {
                    templateUrl: 'templates/currentAfairs-detail.html',
                    controller: 'currentAfairsDetailCtrl'
                }
            }
        })
       
         // 根据电影名搜索电影
        .state('tab.movieSearch', {
            url: '/currentAfairs/:moviename',
            views: {
                'tab-currentAfairs': {
                    templateUrl: 'templates/Movie/movieSearch.html',
                    controller: 'movieSearchCtrl'
                }
            }
        })

        //电影详细信息
        .state('tab.movieIndex', {
            url: '/currentAfairs-detail/:movieid',
            views: {
                'tab-currentAfairs': {
                    templateUrl: 'templates/Movie/index.html',
                    controller: 'movieIndexCtrl'
                }
            }
        })
        //根据id查电影信息-index
         .state('tab.MovieDetailCtrl', {
             url: '/currentAfairs/:movieid',
             views: {
                 'tab-currentAfairs': {
                     templateUrl: 'templates/Movie/Detail.html',
                     controller: 'MovieDetailCtrl'
                 }
             }
         })
        //上映信息
         .state('tab.Release', {
             url: '/MovieDetailCtrl/:movieid',
             views: {
                 'tab-currentAfairs': {
                     templateUrl: 'templates/Movie/Release.html',
                     controller: 'ReleaseCtrl'
                 }
             }
         })
         //选座
         .state('tab.SelectSeat', {
             url: '/movieIndex/:id',
             views: {
                 'tab-currentAfairs': {
                     templateUrl: 'templates/Movie/SelectSeat.html',
                     controller: 'SelectSeatCtrl'
                 }
             }
         })


          // 影院列表（提交订单）
        .state('tab.Indentsub', {
            url: '/SelectSeat/:id',
            views: {
                'tab-currentAfairs': {  // 视图名称
                    templateUrl: 'templates/Movie/Indentsub.html', // 视图对应的 html 文件
                    controller: 'IndentsubCtrl' // 控制器名称
                }
            }
        })

            //完成订单
         .state('tab.complete', {
             url: '/complete',
             views: {
                 'tab-currentAfairs': {  // 视图名称
                     templateUrl: 'templates/Movie/complete.html', // 视图对应的 html 文件
                     controller: 'completeCtrl' // 控制器名称
                 }
             }
         })
        // 影院列表（首页）
        .state('tab.opinionLeaders', {
            url: '/opinionLeaders',
            views: {
                'tab-opinionLeaders': {  // 视图名称
                    templateUrl: 'templates/opinionLeaders.html', // 视图对应的 html 文件
                    controller: 'OpinionLeadersCtrl' // 控制器名称
                }
            }
        })
        
        // 根据影院id查改影院所有电影
        .state('tab.MovieByCinemaID', {
            url: '/opinionLeaders/:cinemaid',
            views: {
                'tab-opinionLeaders': {  // 视图名称
                    templateUrl: 'templates/Cinema/MovieByCinemaID.html', // 视图对应的 html 文件
                    controller: 'MovieByCinemaIDCtrl' // 控制器名称
                }
            }
        })
        // 影院列表（购票）
        .state('tab.CinemaIndex', {
            url: '/MovieByCinemaID/:movieid',
            views: {
                'tab-opinionLeaders': {  // 视图名称
                    templateUrl: 'templates/Cinema/index.html', // 视图对应的 html 文件
                    controller: 'CinemaIndexCtrl' // 控制器名称
                }
            }
        })
   // 影院列表（选座）
        .state('tab.CnSelectSeat', {
            url: '/CinemaIndex/:id',
            views: {
                'tab-opinionLeaders': {  // 视图名称
                    templateUrl: 'templates/Cinema/CnSelectSeat.html', // 视图对应的 html 文件
                    controller: 'CnSelectSeatCtrl' // 控制器名称
                }
            }
        })
         // 影院列表（提交订单）
        .state('tab.CnIndentsub', {
            url: '/CnSelectSeat/:id',
            views: {
                'tab-opinionLeaders': {  // 视图名称
                    templateUrl: 'templates/Cinema/CnIndentsub.html', // 视图对应的 html 文件
                    controller: 'CnIndentsubCtrl' // 控制器名称
                }
            }
        })
         // 影院列表（完成订单）
        .state('tab.Cncomplete', {
            url: '/Cncomplete',
            views: {
                'tab-opinionLeaders': {  // 视图名称
                    templateUrl: 'templates/Cinema/Cncomplete.html', // 视图对应的 html 文件
                    controller: 'CncompleteCtrl' // 控制器名称
                }
            }
        })
        //个人中心
        .state('tab.topic', {
            url: '/topic',
            views: {
                'tab-topic': {  // 视图名称
                    templateUrl: 'templates/topic.html', // 视图对应的 html 文件
                    controller: 'TopicCtrl' // 控制器名称
                }
            }
        })
        //查看个人信息及修改
        .state('tab.UserIndex', {
            url: '/UserIndex',
            views: {
                'tab-topic': {  // 视图名称
                    templateUrl: 'templates/User/index.html', // 视图对应的 html 文件
                    controller: 'UserIndexCtrl' // 控制器名称
                }
            }
        })
        //修改密码模块
        .state('tab.updatePassword', {
            url: '/updatePassword',
            views: {
                'tab-topic': {  // 视图名称
                    templateUrl: 'templates/User/updatePassword.html', // 视图对应的 html 文件
                    controller: 'updatePasswordCtrl' // 控制器名称
                }   
            }
        })
         //看过电影
        .state('tab.SeenMovie', {
            url: '/SeenMovie',
            views: {
                'tab-topic': {  // 视图名称
                    templateUrl: 'templates/User/SeenMovie.html', // 视图对应的 html 文件
                    controller: 'SeenMovieCtrl' // 控制器名称
                }
            }
        })
         //看过电影（列表）
        .state('tab.UDetail', {
            url: '/SeenMovie/:movieid',
            views: {
                'tab-topic': {  // 视图名称
                    templateUrl: 'templates/User/UDetail.html', // 视图对应的 html 文件
                    controller: 'UDetailCtrl' // 控制器名称
                }
            }
        })
          //看过电影（点击查看上映信息）
        .state('tab.URelease', {
            url: '/UDetail/:movieid',
            views: {
                'tab-topic': {  // 视图名称
                    templateUrl: 'templates/User/URelease.html', // 视图对应的 html 文件
                    controller: 'UReleaseCtrl' // 控制器名称
                }
            }
        })
          //想看的电影
        .state('tab.WantMovie', {
            url: '/WantMovie',
            views: {
                'tab-topic': {  // 视图名称
                    templateUrl: 'templates/User/WantMovie.html', // 视图对应的 html 文件
                    controller: 'WantMovieCtrl' // 控制器名称
                }
            }
        })
          //我的电影(买过)
        .state('tab.MovieWrit', {
            url: '/MovieWrit',
            views: {
                'tab-topic': {  // 视图名称
                    templateUrl: 'templates/User/MovieWrit.html', // 视图对应的 html 文件
                    controller: 'MovieWritCtrl' // 控制器名称
                }
            }
        })
           //电影票详细
        .state('tab.Ucompletel', {
            url: '/MovieWrit/:id',
            views: {
                'tab-topic': {  // 视图名称
                    templateUrl: 'templates/User/Ucompletel.html', // 视图对应的 html 文件
                    controller: 'UcompletelCtrl' // 控制器名称
                }
            }
        })
        // 海外来风
        .state('tab.abroadNews', {
            url: '/abroadNews',
            views: {
                'tab-abroadNews': {  // 视图名称
                    templateUrl: 'templates/abroadNews.html', // 视图对应的 html 文件
                    controller: 'AbroadNewsCtrl' // 控制器名称
                }
            }
        })

        // 科技前沿
        .state('tab.frontLineOfTechnology', {
            url: '/frontLineOfTechnology',
            views: {
                'tab-frontLineOfTechnology': {  // 视图名称
                    templateUrl: 'templates/frontLineOfTechnology.html', // 视图对应的 html 文件
                    controller: 'FrontLineOfTechnologyCtrl' // 控制器名称
                }
            }
        })

        .state('tab.chats', {
            url: '/chats',
            views: {
                'tab-chats': {
                    templateUrl: 'templates/tab-chats.html',
                    controller: 'ChatsCtrl'
                }
            }
        })
        .state('tab.chat-detail', {
        url: '/chats/:chatId',
        views: {
            'tab-chats': {
                templateUrl: 'templates/chat-detail.html',
                controller: 'ChatDetailCtrl'
            }
        }
        })
        .state('tab.account', {
      url: '/account',
      views: {
          'tab-account': {
              templateUrl: 'templates/tab-account.html',
              controller: 'AccountCtrl'
          }
      }
        });
    // 缺省的路由的首页
    $urlRouterProvider.otherwise('/tab/currentAfairs');

});

