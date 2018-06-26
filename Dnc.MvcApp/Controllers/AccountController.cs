using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dnc.Entities.Application;
using Dnc.Entities.Business;
using Microsoft.AspNetCore.Http;
using Dnc.MvcApp;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Dnc.DataAccessRepository.Repositories;
using Dnc.MvcApp.ViewsModels;
using System.Text;
using System.IO;
using System.DrawingCore;
using System.DrawingCore.Imaging;

namespace Dnc.MvcApp.Controllers
{
    /// <summary>
    /// 用户登录账号管理控制器
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IEntityRepository _Service;  // EF 数据配置映射上下文
        
        public AccountController(IEntityRepository service)
        {
            this._Service = service;
        }

        /// <summary>
        /// 导航到注册页面
        /// </summary>
        /// <param name="returnUrl">用于在前端处理成功后返回的 Url</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// 用户注册数据处理
        /// </summary>
        /// <param name="model">前端视图提供的 model 数据</param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(UserViewsModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                ApplicationUser user = null;
                await Task.Run(() =>
                {
                    user = _Service.GetSingleBy<ApplicationUser>(x => x.Phonr == model.Phonr);
                });
                if (user != null)
                {
                    // 处理重复用户名问题
                    ModelState.AddModelError("逻辑错误", "你输入的用户名已经被别人注册了。");
                    return View("../../Views/Account/Register", model);
                }
                else
                {
                    user = new ApplicationUser();
                    user.Phonr = model.Phonr;
                    user.Name = model.Name;
                    user.Evaluation = model.Evaluation;
                    var group = _Service.GetSingleBy<ApplicationGroup>(x => x.Name == "普通用户");
                    user.ApplicationGroupId = group.ID;
                    user.Group = group;
                    _Service.AddAndSave<ApplicationUser>(user);
                    return Redirect("../../");
                }

            }
            return View("../../Views/Account/Register",model);
        }
        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="model">与前端视图 Form 绑定的模型</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<LogonUserStatus> Login(LogonInformation model)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "用户名或密码错误。"
            };
            await Task.Run(() => {
                if (ModelState.IsValid)
                {
                    string yanzhen = HttpContext.Request.Form["yanzhen"];
                    var user = _Service.GetSingleBy<ApplicationUser>(x => x.Phonr == model.username&& x.Password == model.password,x=>x.Group);
                    string stryanz= HttpContext.Session.GetString("LoginValidateCode").ToString();
                    if (!String.Equals(yanzhen, stryanz, StringComparison.CurrentCultureIgnoreCase)) {
                        logonStatus.IsLogon = false;
                        logonStatus.Message = "您输入验证码有误。";
                    }
                    if (user != null&& String.Equals(yanzhen, stryanz, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // 处理登录的状态
                        logonStatus.IsLogon = true;
                        logonStatus.Message = "";
                        HttpContext.Session.SetString("LogonSystemUserID", user.ID.ToString());
                        HttpContext.Session.SetString("userGruop", user.Group.Name);
                        logonStatus.Message = "登陆成功";
                    }
                }
            });
            return logonStatus;
        }
        /// <summary>
        /// 图形验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult ValidateCode(VierificationCodeServices _vierificationCodeServices, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            string code = "";
            System.IO.MemoryStream ms = _vierificationCodeServices.Create(out code);
            HttpContext.Session.SetString("LoginValidateCode", code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }
        public class VierificationCodeServices
        {
            /// <summary>  
            /// 该方法用于生成指定位数的随机数  
            /// </summary>  
            /// <param name="VcodeNum">参数是随机数的位数</param>  
            /// <returns>返回一个随机数字符串</returns>  
            private string RndNum(int VcodeNum)
            {
                //验证码可以显示的字符集合  
                string Vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,p" +
                    ",q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,P,Q" +
                    ",R,S,T,U,V,W,X,Y,Z";
                string[] VcArray = Vchar.Split(new Char[] { ',' });//拆分成数组   
                string code = "";//产生的随机数  
                int temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数  

                Random rand = new Random();
                //采用一个简单的算法以保证生成随机数的不同  
                for (int i = 1; i < VcodeNum + 1; i++)
                {
                    if (temp != -1)
                    {
                        rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));//初始化随机类  
                    }
                    int t = rand.Next(61);//获取随机数  
                    if (temp != -1 && temp == t)
                    {
                        return RndNum(VcodeNum);//如果获取的随机数重复，则递归调用  
                    }
                    temp = t;//把本次产生的随机数记录起来  
                    code += VcArray[t];//随机数的位数加一  
                }
                return code;
            }

            /// <summary>  
            /// 该方法是将生成的随机数写入图像文件  
            /// </summary>  
            /// <param name="code">code是一个随机数</param>
            /// <param name="numbers">生成位数（默认4位）</param>  
            public MemoryStream Create(out string code, int numbers = 4)
            {
                code = RndNum(numbers);
                Bitmap Img = null;
                Graphics g = null;
                MemoryStream ms = null;
                Random random = new Random();
                //验证码颜色集合  
                Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

                //验证码字体集合
                string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };


                //定义图像的大小，生成图像的实例  
                Img = new Bitmap((int)code.Length * 18, 32);

                g = Graphics.FromImage(Img);//从Img对象生成新的Graphics对象    

                g.Clear(Color.White);//背景设为白色  

                //在随机位置画背景点  
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(Img.Width);
                    int y = random.Next(Img.Height);
                    g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
                }
                //验证码绘制在g中  
                for (int i = 0; i < code.Length; i++)
                {
                    int cindex = random.Next(7);//随机颜色索引值  
                    int findex = random.Next(5);//随机字体索引值  
                    Font f = new Font(fonts[findex], 15, FontStyle.Bold);//字体  
                    Brush b = new SolidBrush(c[cindex]);//颜色  
                    int ii = 4;
                    if ((i + 1) % 2 == 0)//控制验证码不在同一高度  
                    {
                        ii = 2;
                    }
                    g.DrawString(code.Substring(i, 1), f, b, 3 + (i * 12), ii);//绘制一个验证字符  
                }
                ms = new MemoryStream();//生成内存流对象  
                Img.Save(ms, ImageFormat.Jpeg);//将此图像以Png图像文件的格式保存到流中  

                //回收资源  
                g.Dispose();
                Img.Dispose();
                return ms;
            }
        }


    }
}
