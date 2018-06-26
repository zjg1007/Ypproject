using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Business
{
    /// <summary>
    /// 电影类型表
    /// </summary>
    public class MType : IEntityId
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }//类型名
    }
}
