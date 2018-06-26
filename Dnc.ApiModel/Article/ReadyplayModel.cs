using Dnc.Entities.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ApiModel.Article
{
    public class ReadyplayModel
    {
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
        /// 长时间类型
        /// </summary>
        public string longTime { get; set; }
        /// <summary>
        /// 短时间类型
        /// </summary>
        public string shortTime { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        public int CinemaID { get; set; }
        public string CinemaName { get; set; }
        public string CinemaSite { get; set; }
        public string CinemaRegion { get; set; }
        public string CinemaServe { get; set; }
        public int MovieID { get; set; }
        public string MovieReferral { get; set; }//介绍
        public string MovieName { get; set; }
        public string MovieCountry { get; set; }
        public string MovieNickname { get; set; }
        public string MovieDuration { get; set; }
        public string MovieActor { get; set; }
        public decimal MovieGrade { get; set; }
        public string MoviePicture { get; set; }
        public string MovieMass { get; set; }
        public string MovieTypeId { get; set; }
        public string MovieLaunchName { get; set; }
         public string MovieProtagonist { get; set; }
        public int HomeID { get; set; }
        public string HomePlace { get; set; }
        public string HomeName { get; set; }
        public string HomeQuality { get; set; }
        public int HomeCinemaID { get; set; }
        /// <summary>
        /// 座位
        /// </summary>
        public string DealSite { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal DealRealityMone { get; set; }
        /// <summary>
        /// 取票号
        /// </summary>
        public string DealGainNumber { get; set; }
        public ReadyplayModel(Readyplay bo) {
            this.Id = bo.Id;
            this.StatTime = bo.StatTime;
            this.EndTime = bo.EndTime;
            this.longTime = bo.StatTime.ToString("MM月dd日");
            this.shortTime = bo.StatTime.ToString("HH:mm");
            this.Price = bo.Price;
            if (bo.Cinema != null)
            {
                this.CinemaID = bo.Cinema.Id;
                this.CinemaName = bo.Cinema.Name;
                this.CinemaRegion = bo.Cinema.Region;
                this.CinemaServe = bo.Cinema.Serve;
                this.CinemaSite = bo.Cinema.Site;
            }
            if (bo.Movie != null)
            {
                this.MovieID = bo.Movie.Id;
                this.MovieActor = bo.Movie.Actor;
                this.MovieCountry = bo.Movie.Country;
                this.MovieDuration = bo.Movie.Duration;
                this.MovieGrade = bo.Movie.Grade;
                this.MovieLaunchName = bo.Movie.LaunchName;
                this.MovieMass = bo.Movie.Mass;
                this.MovieName = bo.Movie.Name;
                this.MovieNickname = bo.Movie.Nickname;
                this.MoviePicture = bo.Movie.Picture;
                this.MovieReferral = bo.Movie.Referral;
                this.MovieTypeId = bo.Movie.TypeId;
                this.MovieProtagonist = bo.Movie.Protagonist;
            }
            if (bo.Home != null)
            {
                this.HomeID = bo.Home.Id;
                this.HomeCinemaID = bo.Home.CinemaID;
                this.HomeName = bo.Home.Name;
                this.HomePlace = bo.Home.Place;
                this.HomeQuality = bo.Home.Quality;
            }
        }
        public void MapBo(Readyplay bo)
        {
            bo.Id = this.Id;
            bo.StatTime = this.StatTime;
            bo.EndTime = this.EndTime;
            bo.Price = this.Price;
            if (bo.Cinema != null)
            {
                bo.Cinema.Id=this.CinemaID  ;
                bo.Cinema.Name=this.CinemaName  ;
                bo.Cinema.Region=this.CinemaRegion  ;
                bo.Cinema.Serve=this.CinemaServe  ;
                bo.Cinema.Site=this.CinemaSite ;
            }
            if (bo.Movie != null)
            {
                bo.Movie.Id=this.MovieID ;
                bo.Movie.Actor=this.MovieActor  ;
                bo.Movie.Country=this.MovieCountry  ;
                bo.Movie.Duration=this.MovieDuration ;
                bo.Movie.Grade=this.MovieGrade ;
                bo.Movie.LaunchName=this.MovieLaunchName ;
                bo.Movie.Mass=this.MovieMass  ;
                bo.Movie.Name=this.MovieName ;
                bo.Movie.Nickname=this.MovieNickname ;
                bo.Movie.Picture=this.MoviePicture ;
                bo.Movie.Referral=this.MovieReferral ;
                bo.Movie.TypeId=this.MovieTypeId ;
                bo.Movie.Protagonist = this.MovieProtagonist;
            }
            if (bo.Home != null)
            {
                bo.Home.Id=this.HomeID  ;
                bo.Home.CinemaID=this.HomeCinemaID ;
                bo.Home.Name=this.HomeName ;
                bo.Home.Place=this.HomePlace  ;
                bo.Home.Quality=this.HomeQuality ;
            }
        }
    }
}
