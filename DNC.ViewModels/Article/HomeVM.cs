using Dnc.Entities.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ViewModels.Article
{
    public class HomeVM
    {
        public int Id { get; set; }
        public string Place { get; set; }//位置（0-未开放 1-已开放 2-座位以被选购）
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Name { get; set; }//名称
        [Display(Name = "银幕质量")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Quality { get; set; }//银幕质量 
        public int CinemaID { get; set; }

        public HomeVM() { }
        public HomeVM(Home bo)
        {
            this.Id = bo.Id;
            this.Place = bo.Place;
            this.Name = bo.Name;
            this.Quality = bo.Quality;
            this.CinemaID = bo.CinemaID;
        }
        public void MapBo(Home bo)
        {
            bo.Place = this.Place;
            bo.Name = this.Name;
            bo.Quality = this.Quality;
            bo.CinemaID = this.CinemaID;
        }
    }
}
