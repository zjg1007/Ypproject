using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dnc.Entities.Application;

namespace Dnc.ViewModels.Article
{
    public class UserVm
    {
        public String ID { get; set; }
        [Display(Name = "头像")]
        public string Headimg { get; set; }//头像
        [Display(Name = "手机号码")]
        public string Phonr { get; set; }//手机号码
        [Display(Name = "昵称")]
        [Required(ErrorMessage = "昵称不能为空")]
        public string Name { get; set; }//昵称
        [Display(Name = "自我介绍")]
        public string Evaluation { get; set; }//自我介绍
        [Display(Name = "是否会员")]
        public bool Isvip { get; set; }//是否会员
        [Display(Name = "注册时间")]
        public DateTime Time { get; set; }//注册时间
        public String ApplicationGroupId { get; set; }//权限
        public string ApplicationGroupName { get; set; }// 权限名称
        public UserVm() { }
        public UserVm( ApplicationUser bo) {
            this.ID = bo.ID;
            this.Headimg = bo.Headimg;
            this.Phonr = bo.Phonr;
            this.Name = bo.Name;
            this.Evaluation = bo.Evaluation;
            this.Isvip = bo.Isvip;
            this.Time = bo.Time;
            if (bo.Group != null)
            {
                this.ApplicationGroupId = bo.Group.ID;
                this.ApplicationGroupName = bo.Group.Name;
            }
        }
        public void MapBo(ApplicationUser bo)
        {
            bo.ID = this.ID;
            bo.Headimg = this.Headimg;
            bo.Phonr = this.Phonr;
            bo.Name = this.Name;
            bo.Evaluation = this.Evaluation;
            bo.Isvip = this.Isvip;
            bo.Time = this.Time;
            bo.ApplicationGroupId = this.ApplicationGroupId;
            bo.Group.Name = this.ApplicationGroupName;
        }
        
    }
}
