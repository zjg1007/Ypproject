using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{
    public class Homeparse
    {
        public int Id { get; set; }
        public int Place { get; set; }//位置（0-未开放 1-已开放 2-座位以被选购）
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Name { get; set; }//名称
        [Display(Name = "银幕质量")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Quality { get; set; }//银幕质量 
        public int CinemaID { get; set; }
    }
}
