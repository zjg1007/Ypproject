using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Business
{
    /// <summary>
    /// 放映时段
    /// </summary>
    public class Readyplay : IEntityId
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StatTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        public int CinemaID { get; set; }
        public int MovieID { get; set; }
        public int HomeID { get; set; }
        public virtual Cinema Cinema { get; set; }//影院
        public virtual Movie Movie { get; set; }//电影
        public virtual Home Home { get; set; }//厅号
    }
}
