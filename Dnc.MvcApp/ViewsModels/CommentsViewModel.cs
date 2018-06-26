using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.ViewsModels
{
    public class CommentsViewModel
    {
        [Required(ErrorMessage = "评论不能为空")]
        [Display(Name = "评论行业")]
        public string Title { get; set; }//正文
    }
}
