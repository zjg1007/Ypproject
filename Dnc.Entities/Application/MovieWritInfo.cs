using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{
    public class MovieWritInfo
    {
        /// <summary>
        /// 电影名称
        /// </summary>
        public string MovieName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 影院名称
        /// </summary>
        public string CinemaName { get; set; }
        /// <summary>
        /// 上映id
        /// </summary>
        public int Readplayid { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public int Indent { get; set; }
        /// <summary>
        /// 订购票数
        /// </summary>
        public int MovieCount { get; set; }
        /// <summary>
        /// 座位
        /// </summary>
        public string Site { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal RealityMone { get; set; }
        /// <summary>
        /// 取票号
        /// </summary>
        public string GainNumber { get; set; }
    }
}
