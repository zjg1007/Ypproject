using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Business
{
    /// <summary>
    /// 影院
    /// </summary>
    public class Cinema : IEntityId
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }//名称
        [StringLength(100)]
        public string Site { get; set; }//地址
        [StringLength(100)]
        public string Region { get; set; }//地区
        [StringLength(100)]
        public string Serve { get; set; }//特色服务（以"，"隔开）
    }
}
