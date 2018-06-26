using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Business
{
    /// <summary>
    /// 电影上映信息表
    /// </summary>
    public class Launch : IEntityId
    {
        [Key]
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }//上映时间
        [StringLength(50)]
        public string Country { get; set; }//国家
        [StringLength(50)]
        public string Region { get; set; }//地区
        public int MovieID { get; set; }//电影
        public virtual Movie Movie { get; set; }
    }
}
