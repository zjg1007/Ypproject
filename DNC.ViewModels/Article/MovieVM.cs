﻿using Dnc.Entities.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ViewModels.Article
{
    public class MovieVM
    {
        public int Id { get; set; }
        [Display(Name = "介绍")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Referral { get; set; }//介绍
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Name { get; set; }//名称
        [Display(Name = "国家")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Country { get; set; }//国家
        [Display(Name = "外名")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Nickname { get; set; }//国家别称
        [Display(Name = "时长")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Duration { get; set; }//时长
        [Display(Name = "演员")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Actor { get; set; }//演员
        [Display(Name = "评分")]
        public decimal Grade { get; set; }//评分
        [Display(Name = "照片")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Picture { get; set; }//照片
        [Display(Name = "银幕支持的最高质量")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Mass { get; set; }//银幕支持的最高质量
        [Display(Name = "类型")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string TypeId { get; set; }//类型
        [Display(Name = "上映时间")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string LaunchName { get; set; }//上映时间
        [Display(Name = "导演")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Protagonist { get; set; }//导演
        public MovieVM( Movie model) {
            this.Id = model.Id;
            this.Referral = model.Referral;
            this.Name = model.Name;
            this.Country = model.Country;
            this.Nickname = model.Nickname;
            this.Duration = model.Duration;
            this.Grade = model.Grade;
            this.Picture = model.Picture;
            this.Mass = model.Mass;
            this.TypeId = model.TypeId;
            this.LaunchName = model.LaunchName;
            this.Protagonist = model.Protagonist;
        }
        public void MapBo( Movie model) {
            model.Id = this.Id;
            model.Referral = this.Referral;
            model.Name = this.Name;
            model.Country = this.Country;
            model.Nickname = this.Nickname;
            model.Duration = this.Duration;
            model.Actor = this.Actor;
            model.Grade = this.Grade;
            model.Picture = this.Picture;
            model.Mass = this.Mass;
            model.TypeId = this.TypeId;
            model.LaunchName = this.LaunchName;
            model.Protagonist = this.Protagonist;
        }

    }
}
