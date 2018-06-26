using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{
    public class Cinmaparse
    {
        public int Id { get; set; }
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Name { get; set; }//名称
        [Display(Name = "地址")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Site { get; set; }//地址
        [Display(Name = "地区")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Region { get; set; }//地区
        [Display(Name = "特色服务")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Serve { get; set; }//特色服务（以"，"隔开）
    }
}
