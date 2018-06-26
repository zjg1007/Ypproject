using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dnc.Entities;

namespace Dnc.ViewModels.Article
{
    public class CommentsVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "评论不能为空")]
        [Display(Name = "评论")]
        public string Title { get; set; }//正文
        public CommentsVM() { }
        public CommentsVM(CommentsVM bo)
        {
            this.ID = bo.ID;
            this.Title = bo.Title;
        }
        public void MapBo(CommentsVM bo)
        {
            bo.Title=this.Title;
        }
    }
}
