using Dnc.Entities.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ViewModels.Article
{
    public class ActorVM
    {
        public int Id { get; set; }
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
        public ActorVM(Actor bo)
        {
            this.Id = bo.Id;
            this.Birthplace = bo.Birthplace;
            this.BirthDate = bo.BirthDate;
            this.Name = bo.Name;
            this.ElseName = bo.ElseName;
            this.Intro = bo.Intro;
            this.Profession = bo.Profession;
            this.Photo = bo.Photo;
        }
        public void MapBo(Actor bo)
        {
            bo.Id = this.Id;
            bo.Birthplace = this.Birthplace;
            bo.BirthDate = this.BirthDate;
            bo.Name = this.Name;
            bo.ElseName = this.ElseName;
            bo.Intro = this.Intro;
            bo.Profession = this.Profession;
            bo.Photo = this.Photo;
        }
    }
}
