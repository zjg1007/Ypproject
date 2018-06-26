using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.ViewsModels
{
    public class RecruitmentViewModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "工资不能为空")]
        [Display(Name = "工资")]
        public int Wage { get; set; }
        [Display(Name = "发布日期")]
        public DateTime ReleaseTime { get; set; }
        [Required(ErrorMessage = "工作经验不能为空")]
        [Display(Name = "工作经验")]
        public int WorkExperience { get; set; }
        [Required(ErrorMessage = "招聘人数不能为空")]
        [Display(Name = "招聘人数")]
        public int Hiring { get; set; }
        [Required(ErrorMessage = "工作地点不能为空")]
        [Display(Name = "工作地点")]
        public string WorkingPlace { get; set; }
        [Required(ErrorMessage = "工作性质不能为空")]
        [Display(Name = "工作性质")]
        public string JobCategory { get; set; }
        [Required(ErrorMessage = "职业描述不能为空")]
        [Display(Name = "职业描述")]
        public string JobDescription { get; set; }
        [Required(ErrorMessage = "公司名称不能为空")]
        [Display(Name = "公司（组织）名称")]
        public string CompanyIntroduction { get; set; }
        [Required(ErrorMessage = "面向学校不能为空")]
        [Display(Name = "面向学校")]
        public string ForSchool { get; set; }
        [Display(Name = "是否取消")]
        public string Cancel { get; set; }
        public virtual CommentsViewModel Comments { get; set; }
       
    }
}
