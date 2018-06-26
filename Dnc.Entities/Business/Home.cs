using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Business
{
    /// <summary>
    /// 放映厅信息表
    /// </summary>
    public class Home : IEntityId
    {
        [Key]
        public int Id { get; set; }
        public string Place { get; set; }//位置（True-未开放 False-已开放 ok-座位以被选购）
        [StringLength(20)]
        public string Name { get; set; }//名称
        [StringLength(20)]
        public string Quality { get; set; }//银幕质量 
        public int CinemaID { get; set; }

        
    }
}
