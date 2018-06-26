using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dnc.Entities.Application;

namespace Dnc.Entities.Business
{
    /// <summary>
    /// 交易订单
    /// </summary>
    public class Deal : IEntityId
    {
        [Key]
        public int Id { get; set; }
        public string GainNumber { get; set; }//取票号
        public decimal RealityMone { get; set; }//实际金额
        public string Site { get; set; }//影院位置
        [StringLength(50)]
        public string OrderNb { get; set; }//订单号
       public DateTime BuyDate { get; set; }//购买时间
        public int CinemaID { get; set; }
        public int MovieID { get; set; }
        public String UserID { get; set; }
        public virtual Cinema Cinema { get; set; }//影院
        public virtual Movie Movie { get; set; }//电影
        public virtual  ApplicationUser User { get; set; }//用户
        public Deal()
        {
            this.BuyDate = DateTime.Now;
        }
    }
}
