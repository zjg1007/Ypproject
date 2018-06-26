using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class ApplicationUser: IEntity
    {
        [Key]
        public String ID { get; set; }
        [StringLength(200)]
        public string Headimg { get; set; }//头像
        public string Phonr { get; set; }//手机号码
        [StringLength(50)]
        public string Password { get; set; }//密码
        [StringLength(50)]
        public string Name { get; set; }//昵称
        [StringLength(300)]
        public string Evaluation { get; set; }//自我介绍
        public bool Isvip { get; set; }//是否会员
        public DateTime Time { get; set; }//注册时间
        public String ApplicationGroupId { get; set; }
        public virtual ApplicationGroup Group { get; set; }
        public ApplicationUser()
        {
            this.ID = Guid.NewGuid().ToString();
            this.Isvip = false;
            this.Time = DateTime.Now;
        }

    }
}
