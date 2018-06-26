using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dnc.Entities;
namespace Dnc.Entities.Application
{
    /// <summary>
    /// 演员信息表
    /// </summary>
    public class Actor : IEntityId
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Birthplace { get; set; }//出生地
        public DateTime BirthDate { get; set; }//出生日期
        [StringLength(50)]
        public string ElseName { get; set; }//别名
        [StringLength(50)]
        public string Name { get; set; }//姓名
        [StringLength(2000)]
        public string Intro { get; set; }//简介
        [StringLength(100)]
        public string Profession { get; set; }//职业（多数请以"，"隔开）
        public string Photo { get; set; }//头像


    }
}
