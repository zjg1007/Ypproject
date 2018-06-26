using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{
    public class ActorStatus
    {
        [Display(Name = "地区：")]
        [Required(ErrorMessage = "{0}必须填写")]
        public string Birthplace { get; set; }//出生地
        [Display(Name = "出生日期：")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0}必须填写")]
        public DateTime BirthDate { get; set; }//出生日期
        [Display(Name = "外名：")]
        [Required(ErrorMessage = "{0}必须填写")]
        public string ElseName { get; set; }//别名
        [Display(Name = "姓名：")]
        [Required(ErrorMessage = "{0}必须填写")]
        public string Name { get; set; }//姓名
        [Display(Name = "简介：")]
        [Required(ErrorMessage = "{0}必须填写")]
        public string Intro { get; set; }//简介
        [Display(Name = "职业：")]
        [Required(ErrorMessage = "{0}必须填写")]
        public string Profession { get; set; }//职业（多数请以"，"隔开）
        public string Photo { get; set; }//头像
    }
}
