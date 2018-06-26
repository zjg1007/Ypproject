/**
 * Created by Administrator on 2017/2/24.
 */
    var mySwiper = new Swiper('.swiper-container1', {
        autoplay: 1000,//可选选项，自动滑动
        speed:500,//滑动速度
        autoplayDisableOnInteraction : false,//用户操作swiper之后自动切换不会停止，每次都会重新启动autoplay
        touchAngle : 20, // 允许触发拖动的角度值。默认45度，即使触摸方向不是完全水平也能拖动slide。
        pagination : '.swiper-pagination', // 分页器
        paginationElement : 'li', // 分页器元素
        loop : true,
    })
    // 头条
    var mySwiper2 = new Swiper('.swiper-container2', {
        autoplay: 1000, //可选选项，自动滑动
        direction : 'vertical',
        loop : true,
    });
    //
    var mySwiper3 = new Swiper('.swiper-container3',{

        resistanceRatio : 0.5,//抵抗率。边缘抵抗力的大小比例。值越小抵抗越大越难将slide拖离边缘，0时完全无法拖离。
        slidesPerView : 3,//可以设置为number或者 'auto'则自动根据slides的宽度来设定数量。
        spaceBetween : 10,//slide之间的距离（单位px）。
        resistance : true, //继续拖动Swiper会离开边界，释放后弹回。
        preloadImages:true,//默认为true，Swiper会强制加载所有图片。
        updateOnImagesReady: true,//当所有的内嵌图像（img标签）加载完成后Swiper会重新初始化。使用此选项需要先开启preloadImages: true
        observer: true,//修改swiper自己或子元素时，自动初始化swiper
        observeParents: true,//修改swiper的父元素时，自动初始化swiper
    });
    slide('.swiper-container4');
    slide('.swiper-container5');
    slide1('.swiper-container6')
    function slide(obj){
        var mySwiper4 = new Swiper(obj, {

            autoplay: 1000, //可选选项，自动滑动
            speed:500, // slide 运动的速度
            autoplayDisableOnInteraction : false, //用户操作swiper之后，是否禁止autoplay。默认为true：停止。
            touchAngle : 20, // 允许触发拖动的角度值。默认45度，即使触摸方向不是完全水平也能拖动slide。
            pagination : '.swiper-pagination', // 分页器
            paginationElement : 'li', // 分页器元素
            loop : true,
        });
    };
    function slide1(obj){
        var mySwiper4 = new Swiper(obj, {

            autoplay: 1000, //可选选项，自动滑动
            speed:500, // slide 运动的速度
            autoplayDisableOnInteraction : true, //用户操作swiper之后，是否禁止autoplay。默认为true：停止。
            touchAngle : 20, // 允许触发拖动的角度值。默认45度，即使触摸方向不是完全水平也能拖动slide。
            pagination : '.swiper-pagination', // 分页器
            paginationElement : 'li', // 分页器元素
            loop : true,
        });
    };
    //倒计时  设置结束时间
    var endTime=new Date();
    var time;
    endTime.setFullYear(2017);
    endTime.setMonth(1);
    endTime.setDate(24);
    endTime.setHours(24);
    endTime.setMinutes(0);
    endTime.setSeconds(0);

    var endTimer=endTime.getTime();//获取结束时间毫秒数
    var iHour = document.querySelector('#hour');
    var iMin = document.querySelector('#min');
    var iSec = document.querySelector('#sec');
    changTime();
    function changTime(){
        //当前时间
        var nowTime=new Date();
        var sec=(endTime-nowTime.getTime())/1000; //时间差秒数
        if(sec>0)
        {
            var hour=Math.floor(sec/60/60);//小时
            sec %=3600;
            var min=Math.floor(sec/60);
            var sec=Math.floor(sec%60);
            iHour.innerHTML=zero(hour,2);
            iMin.innerHTML=zero(min,2);
            iSec.innerHTML=zero(sec,2);
        }else{
            clearInterval(time);
        }
    }
    time=setInterval(function(){
        changTime();
    },1000);
    function zero(time,n){
        var str=''+time;
        while (str.length<n){
            return '0'+time;
        }
        return str;
    }
    var logo=document.querySelector('.settop');
    logo.addEventListener("touchstart",function(){
        document.documentElement.scrollTop = document.body.scrollTop =0;
    })
    setInterval(function(){
        var scTop=document.body.scrollTop;
        if(scTop>=400){
            logo.style.display='block';
        }else{
            logo.style.display='none';
        }
    },10)

