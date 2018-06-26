using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.ViewsModels
{
    public class ApplyViewsModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "状态不能为空")]
        [Display(Name = "状态")]
        public string State { get; set; }
    }
}
