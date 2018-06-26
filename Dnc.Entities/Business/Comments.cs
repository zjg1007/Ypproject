using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dnc.Entities.Application;

namespace Dnc.Entities.Business
{
    /// <summary>
    /// 用户评分表
    /// </summary>
    public class Comments : IEntityId
    {
        [Key]
        public int Id { get; set; }
        [StringLength(1000)]
        public string Content { get; set; }//内容
        public int Likebtn { get; set; }//点赞
        public DateTime Time { get; set; }//评论时间
        public bool State { get; set; }//状态(True-想看 False 看过)
        public int Grade { get; set; }//评分
        public String UserId { get; set; }//用户
        public int MovieID { get; set; }//电影
        public virtual ApplicationUser  User { get; set; }
        public virtual Movie Movie { get; set; }
        public Comments() {
            this.Time = DateTime.Now;
            this.Likebtn = 0;
        }
    }
}
