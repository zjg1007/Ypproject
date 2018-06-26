using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Business
{
    /// <summary>
    /// 电影表
    /// </summary>
    public class Movie : IEntityId
    {
        [Key]
        public int Id { get; set; }
        [StringLength(300)]
        public string Referral { get; set; }//介绍
        [StringLength(100)]
        public string Name { get; set; }//名称
        [StringLength(50)]
        public string Country { get; set; }//国家
        [StringLength(100)]
        public string Nickname { get; set; }//国家别称
        [StringLength(50)]
        public string Duration { get; set; }//时长
        public string Actor { get; set; }//演员
        public string Protagonist { get; set; }//导演
        public decimal Grade { get; set; }//评分
        [StringLength(200)]
        public string Picture { get; set; }//照片
        [StringLength(20)]
        public string Mass { get; set; }//银幕支持的最高质量
        public string TypeId { get; set; }//类型
        public string LaunchName { get; set; }//上映时间
        public virtual MType TypeInfo { get; set; }

    }
}
