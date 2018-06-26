using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.ViewsModels
{
    public class UserViewsModel
    {
        public Guid ID { get; set; }
        [Display(Name = "头像")]
        public string Headimg { get; set; }//头像
        [Display(Name = "手机号码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Phonr { get; set; }//手机号码
        [Display(Name = "密码")]
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }//密码
        [Display(Name = "昵称")]
        [Required(ErrorMessage = "昵称不能为空")]
        public string Name { get; set; }//昵称
        [Display(Name = "自我介绍")]
        public string Evaluation { get; set; }//自我介绍
        [Display(Name = "是否会员")]
        public bool Isvip { get; set; }//是否会员
        [Display(Name = "注册时间")]
        public DateTime Time { get; set; }//注册时间
    }
}
