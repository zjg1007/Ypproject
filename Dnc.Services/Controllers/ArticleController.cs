using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Application;
using Dnc.ApiModel.Article;
using Dnc.Entities.Business;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using Dnc.Services.Utilities;

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        private readonly IEntityRepository _DbService;

        public ArticleController(IEntityRepository service)
        {
            this._DbService = service;
        }
        /// <summary>
        /// 提取全部电影信息
        /// 访问方式：http://site:port/Article  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("Get")]
        public async Task<IEnumerable<MovieApiModel>> Get()
        {
            List<MovieApiModel> model = new List<MovieApiModel>();
            await Task.Run(() =>
            {
                var data = _DbService.GetAllID<Movie>(m => m.TypeInfo).ToList();

                foreach (var item in data)
                {
                    if (item.Grade != 0)
                    {
                        int count = _DbService.GetAllID<Comments>(m => m.Movie).Where(m => m.Movie.Id == item.Id).Count();
                        item.Grade = count / item.Grade * 10;
                    }
                    MovieApiModel movieapi = new MovieApiModel(item);
                    string[] actor = movieapi.Actor.Split(',');
                    movieapi.Director = _DbService.GetSingleByID<Actor>(m => m.Id == int.Parse(actor[0]));
                    model.Add(movieapi);
                }
            });
            return model.ToList();
        }
        /// <summary>
        /// 根据电影id查询放映的所有信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [HttpGet("GetReadyplayByID")]
        public async Task<IEnumerable<ReadyplayList>> GetReadyplayByID(int id)
        {
            List<Readyplay> data = new List<Readyplay>();
            List<Readyplay> data1 = new List<Readyplay>();
            List<ReadyplayList> rem = new List<ReadyplayList>();
            List<ReadyplayModel> renModle = new List<ReadyplayModel>();
            List<ReadyplayModel> NewrenModle = new List<ReadyplayModel>();

            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Readyplay>(m => m.Cinema, m => m.Home, m => m.Movie).Where(m => m.Movie.Id == id && m.StatTime > DateTime.Now).DistinctBy(m => m.CinemaID).OrderBy(m => m.StatTime).Take(7).ToList();
                data1 = data = _DbService.GetAllID<Readyplay>(m => m.Cinema, m => m.Home, m => m.Movie).Where(m => m.Movie.Id == id && m.StatTime > DateTime.Now).DistinctBy(m => m.StatTime).Take(7).ToList();
                foreach (var item in data)
                {
                    renModle.Add(new ReadyplayModel(item));
                }
                foreach (var item in data1)
                {
                    NewrenModle.Add(new ReadyplayModel(item));
                }
                var longTime = NewrenModle.OrderBy(m => m.longTime).Select(m => m.longTime).Distinct().ToList();
                foreach (var item in longTime)
                {
                    rem.Add(new ReadyplayList() { Time = item, GetReadyplayList = NewrenModle.Where(m => m.longTime == item).DistinctBy(m => m.CinemaID).ToList() });
                }
                foreach (var item in rem)
                {
                    foreach (var and in item.GetReadyplayList)
                    {
                        List<ReadyplayModel> EndrenModle = new List<ReadyplayModel>();
                        var end = _DbService.GetAllID<Readyplay>(y => y.Cinema, y => y.Home, y => y.Movie).Where(m => m.Movie.Id == id && m.Cinema.Id == and.CinemaID).ToList();
                        foreach (var item1 in end)
                        {
                            EndrenModle.Add(new ReadyplayModel(item1));
                        }
                        item.ReadList = EndrenModle.Where(m => m.longTime == item.Time).Distinct().Take(4).ToList();
                    }
                }

            });
            return rem.Take(7).ToList();
        }

        /// <summary>
        /// 根据电影id和影院id查上映时间段信息
        /// </summary>
        /// <param name="movieid"></param>
        /// <param name="cinemid"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("GettMoList")]
        public async Task<IEnumerable<ReadyplayList>> GettMoList(int movieid, int cinemid)
        {
            List<Readyplay> data = new List<Readyplay>();
            List<ReadyplayList> rem = new List<ReadyplayList>();
            List<ReadyplayModel> renModle = new List<ReadyplayModel>();
            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Readyplay>(m => m.Cinema, m => m.Home, m => m.Movie).Where(m => m.Movie.Id == movieid && m.Cinema.Id == cinemid && m.StatTime > DateTime.Now).OrderBy(m => m.StatTime).ToList();
                foreach (var item in data)
                {
                    renModle.Add(new ReadyplayModel(item));
                }
                var longTime = renModle.Select(m => m.longTime).Distinct().ToList();
                foreach (var item in longTime)
                {
                    rem.Add(new ReadyplayList() { Time = item, GetReadyplayList = renModle.Where(m => m.longTime == item).ToList() });
                }
                foreach (var item in rem)
                {
                    foreach (var and in item.GetReadyplayList)
                    {

                        List<ReadyplayModel> EndrenModle = new List<ReadyplayModel>();
                        var end = _DbService.GetAllID<Readyplay>(y => y.Cinema, y => y.Home, y => y.Movie).Where(m => m.Movie.Id == movieid && m.Cinema.Id == cinemid).ToList();
                        foreach (var item1 in end)
                        {
                            EndrenModle.Add(new ReadyplayModel(item1));
                        }
                        item.ReadList = EndrenModle.Where(m => m.longTime == item.Time).Distinct().ToList();
                    }
                }

            });
            return rem.Take(5).ToList();
        }
        /// <summary>
        /// 根据电影id查信息
        /// </summary>
        /// <param name="movieid"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("movieDetail")]
        public async Task<MovieApiModelDetail> movieDetail(int movieid)
        {
            MovieApiModel model = new MovieApiModel();
            MovieApiModelDetail modelDetail = new MovieApiModelDetail();
            await Task.Run(() =>
            {
                var data = _DbService.GetSingleByID<Movie>(m => m.Id == movieid);
                int count = _DbService.GetAllID<Comments>(m => m.Movie).Where(m => m.Movie.Id == movieid).Count();
                if (count > 0&&data.Grade!=0) {
                    data.Grade = count / data.Grade * 10;
                }
                model = new MovieApiModel(data);
                modelDetail.MovieApiModel = model;
                modelDetail.Actor = new List<Actor>();
                string[] actor = model.Actor.Split(',');
                for (int i = 0; i < actor.Length; i++)
                {
                    Actor and = new Actor();
                    and = _DbService.GetSingleByID<Actor>(m => m.Id == int.Parse(actor[i]));
                    modelDetail.Actor.Add(and);
                }
            });
            return modelDetail;
        }
        /// <summary>
        /// 根据电影名称搜索所有的电影信息
        /// </summary>
        /// <param name="movieName"></param>
        /// <returns></returns>
        [Route("GetSearchMovieAll"), HttpPost]
        public async Task<IEnumerable<MovieApiModel>> GetSearchMovieAll(string moviename)
        {
            List<MovieApiModel> model = new List<MovieApiModel>();
            await Task.Run(() =>
            {
                var data = _DbService.GetAllID<Movie>().Where(m => m.Name.Contains(moviename)).ToList();
                foreach (var item in data)
                {
                    if (item.Grade != 0)
                    {
                        int count = _DbService.GetAllID<Comments>(m => m.Movie).Where(m => m.Movie.Id == item.Id).Count();
                        item.Grade = count / item.Grade * 10;
                    }
                    MovieApiModel movieapi = new MovieApiModel(item);
                    string[] actor = movieapi.Actor.Split(',');
                    movieapi.Director = _DbService.GetSingleByID<Actor>(m => m.Id == int.Parse(actor[0]));
                    model.Add(movieapi);
                }
            });
            return model;
        }
        /// <summary>
        /// 查电影上映信息
        /// </summary>
        /// <param name="movieid"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("movieLaunch")]
        public async Task<IEnumerable<Launch>> movieLaunch(int movieid)
        {
            var data = new List<Launch>();
            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Launch>(m => m.Movie).Where(m => m.MovieID == movieid).ToList();
            });
            return data;
        }

        /// <summary>
        /// 看过的电影
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [Route("PostLookMovie"), HttpPost]
        public async Task<IEnumerable<MovieApiModel>> PostLookMovie(string username)
        {
            var data = new List<Deal>();
            List<MovieApiModel> model = new List<MovieApiModel>();
            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Deal>(m => m.Cinema, m => m.Movie, m => m.User).Where(m => m.User.Phonr == username).DistinctBy(m => m.MovieID).ToList();
                foreach (var item in data)
                {
                    if (item.Movie.Grade != 0)
                    {
                        int count = _DbService.GetAllID<Comments>(m => m.Movie).Where(m => m.Movie.Id == item.Movie.Id).Count();
                        item.Movie.Grade = count / item.Movie.Grade * 10;
                    }
                    MovieApiModel movieapi = new MovieApiModel(item.Movie);
                    string[] actor = movieapi.Actor.Split(',');
                    movieapi.Director = _DbService.GetSingleByID<Actor>(m => m.Id == int.Parse(actor[0]));
                    model.Add(movieapi);
                }

            });
            return model;
        }

        /// <summary>
        /// 影院列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("GetCinemaList")]
        public IEnumerable<ReadyplayModel> GetCinemaList()
        {
            var data = _DbService.GetAllID<Readyplay>(y => y.Cinema, y => y.Movie, y => y.Home).DistinctBy(y => y.CinemaID).ToList();
            List<ReadyplayModel> rem = new List<ReadyplayModel>();
            foreach (var item in data)
            {
                rem.Add(new ReadyplayModel(item));
            }
            return rem;
        }

        /// <summary>
        /// 根据影院id查该影院的所有电影
        /// </summary>
        /// <param name="cinemaid"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("GetcinemaInfo")]
        public IEnumerable<MovieApiModel> GetcinemaInfo(int cinemaid)
        {
            List<MovieApiModel> model = new List<MovieApiModel>();
            var data = _DbService.GetAllID<Readyplay>(m => m.Movie, m => m.Cinema, m => m.Home).Where(m => m.Cinema.Id == cinemaid && m.StatTime > DateTime.Now).DistinctBy(m => m.MovieID).ToList();
            foreach (var item in data)
            {
                if (item.Movie.Grade != 0)
                {
                    int count = _DbService.GetAllID<Comments>(m => m.Movie).Where(m => m.Movie.Id == item.Movie.Id).Count();
                    item.Movie.Grade = count / item.Movie.Grade * 10;
                }
                MovieApiModel movieapi = new MovieApiModel(item.Movie);
                string[] actor = movieapi.Actor.Split(',');
                movieapi.Director = _DbService.GetSingleByID<Actor>(m => m.Id == int.Parse(actor[0]));
                model.Add(movieapi);
            }
            return model;
        }
        /// <summary>
        /// 根据id查信息（选座）
        /// 访问方式：http://site:port/Article/18d64e28-7c32-4aec-a66c-4c0590fd8ce4
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("SelectSeat")]
        public IEnumerable<ReadyplayModel> SelectSeat(int id)
        {
            var data = _DbService.GetAllID<Readyplay>(y => y.Cinema, y => y.Movie, y => y.Home).Where(m => m.Id == id).ToList();
            List<ReadyplayModel> list = new List<ReadyplayModel>();
            foreach (var item in data)
            {
                list.Add(new ReadyplayModel(item));
            }
            return list;
        }
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="RealityMone">实际金额</param>
        /// <param name="Site">影院的位置</param>
        /// <param name="id">放映时间段id</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        [Route("subDealInfo"), HttpPost]
        public LogonUserStatus subDealInfo(string RealityMone,string Site,int id,string username, string Place)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "提交失败!"
            };
            try
            {
                //取票号
                string guainnum = "";
                guainnum = username.Substring(username.Length - 4);
                int Result = 0;
                

                //确定用户选择了多少张票
                string[] strList = Site.Split(',');
                string[] numphone = new string[strList.Length];
                for (int i = 0; i < strList.Length; i++)
                {
                    Random r = new Random();
                    Result = r.Next(1000, 9999);
                    numphone[i] = guainnum+"-"+ Result.ToString();
                }
                Deal model = new Deal();
                model.RealityMone = Convert.ToDecimal( RealityMone);
                model.Site = Site;
                model.GainNumber = string.Join(",", numphone);
                model.OrderNb = id.ToString();
                var and=_DbService.GetSingleByID<Readyplay>(m=>m.Id==id,m=>m.Cinema,m=>m.Home,m=>m.Movie);
                var usermodel =  _DbService.GetSingleBy<ApplicationUser>(m => m.Phonr == username, m => m.Group);
                model.User = usermodel;
                model.UserID = usermodel.ID;
                model.Movie = _DbService.GetSingleByID<Movie>(m=>m.Id==and.Movie.Id,m=>m.TypeInfo);
                model.Cinema = _DbService.GetSingleByID<Cinema>(m=>m.Id==and.Cinema.Id);
                var homeList= _DbService.GetSingleByID<Home>(m=>m.Id==and.Home.Id);
                homeList.Place = Place;
                model.MovieID = and.Movie.Id;
                model.CinemaID = and.Cinema.Id;
                _DbService.AddAndSaveID<Deal>(model);
                _DbService.EditAndSaveID<Home>(homeList);
                logonStatus.IsLogon = true;
                logonStatus.Message = "提交成功";
                logonStatus.Standby = string.Join(",", numphone);
            }
            catch (Exception ex)
            {
                logonStatus.IsLogon = false;
                logonStatus.Message =ex.Message;
            }
            return logonStatus;
        }
        private DateTime seleDate(string name)
        {
            DateTime dt = DateTime.ParseExact(name, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
            return dt;
        }
        /// <summary>
        /// 正在热映
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("IsHitList")]
        public IEnumerable<MovieApiModel> IsHitList()
        {
            List<MovieApiModel> model = new List<MovieApiModel>();
            var data= _DbService.GetAllID<Movie>(m=>m.TypeInfo).Where(m => seleDate(m.LaunchName) < seleDate(DateTime.Now.ToString("yyyy-MM-dd"))).OrderByDescending(m=> seleDate(m.LaunchName)).ToList();
            foreach (var item in data)
            {
                if (item.Grade != 0)
                {
                    int count = _DbService.GetAllID<Comments>(m => m.Movie).Where(m => m.Movie.Id == item.Id).Count();
                    item.Grade = count / item.Grade * 10;
                }
                MovieApiModel movieapi = new MovieApiModel(item);
                string[] actor = movieapi.Actor.Split(',');
                movieapi.Director = _DbService.GetSingleByID<Actor>(m => m.Id == int.Parse(actor[0]));
                model.Add(movieapi);
            }
            return model.Take(12);
        }
       /// <summary>
       /// 即将上映
       /// </summary>
       /// <returns></returns>
            [HttpGet]
        [HttpGet("ComingSoon")]
        public IEnumerable<MovieApiModel> ComingSoon()
        {
            List<MovieApiModel> model = new List<MovieApiModel>();
            var data = _DbService.GetAllID<Movie>(m=>m.TypeInfo).Where(m => seleDate(m.LaunchName) > seleDate(DateTime.Now.ToString("yyyy-MM-dd"))).OrderBy(m => seleDate(m.LaunchName)).ToList();//即将上映
            foreach (var item in data)
            {
                if (item.Grade != 0)
                {
                    int count = _DbService.GetAllID<Comments>(m => m.Movie).Where(m => m.Movie.Id == item.Id).Count();
                    item.Grade = count / item.Grade * 10;
                }
                MovieApiModel movieapi = new MovieApiModel(item);
                string[] actor = movieapi.Actor.Split(',');
                movieapi.Director = _DbService.GetSingleByID<Actor>(m => m.Id == int.Parse(actor[0]));
                model.Add(movieapi);
            }
            return model;
        }
        /// <summary>
        /// 想看电影
        /// </summary>
        /// <param name="moviename"></param>
        /// <returns></returns>
        [Route("WantMovie"), HttpPost]
        public LogonUserStatus WantMovie(int id,string username)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "提交失败!"
            };
            try
            {
                var end=_DbService.GetSingleByID<Comments>(m => m.Movie.Id == id && m.User.Phonr == username, m => m.Movie, m => m.User);
                if (end != null)
                {
                    _DbService.DeleteAndSaveID<Comments>(end);
                    logonStatus.IsLogon = true;
                    logonStatus.Message = "标记取消";
                }
                else {
                    Comments model = new Comments();
                    model.Content = "";
                    model.State = true;
                    model.Grade = 0;
                    var data = _DbService.GetSingleBy<ApplicationUser>(m => m.Phonr == username, m => m.Group);
                    model.UserId = data.ID;
                    model.User = data;
                    var movie = _DbService.GetSingleByID<Movie>(m => m.Id == id, m => m.TypeInfo);
                    model.MovieID = movie.Id;
                    model.Movie = movie;
                    _DbService.AddAndSaveID<Comments>(model);
                    logonStatus.IsLogon = true;
                    logonStatus.Message = "标记成功";
                }
               
            }
            catch (Exception)
            {
                logonStatus.IsLogon = false;
            }
            return logonStatus;
        }
        /// <summary>
        /// 想看电影（列表）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("WantMovieList")]
        public IEnumerable<MovieApiModel> WantMovieList(string username)
        {
            var data=_DbService.GetAllID<Comments>( m => m.User, m => m.Movie).Where(m => m.User.Phonr == username).ToList();
            List<MovieApiModel> model = new List<MovieApiModel>();
            foreach (var item in data)
            {
                if (item.Movie.Grade != 0)
                {
                    int count = _DbService.GetAllID<Comments>(m => m.Movie).Where(m => m.Movie.Id == item.Id).Count();
                    item.Grade = count / item.Grade * 10;
                }
                MovieApiModel movieapi = new MovieApiModel(item.Movie);
                string[] actor = movieapi.Actor.Split(',');
                movieapi.Director = _DbService.GetSingleByID<Actor>(m => m.Id == int.Parse(actor[0]));
                model.Add(movieapi);
            }
            return model;
        }

        [Route("MovieWrit"), HttpPost]
        public IEnumerable<MovieWritInfo> MovieWrit( string username)
        {
          var data=  _DbService.GetAllID<Deal>(m => m.Cinema, m => m.Movie, m => m.User).Where(m => m.User.Phonr == username);
            List<MovieWritInfo> and = new List<MovieWritInfo>();
            foreach (var item in data)
            {
               var obox= _DbService.GetSingleByID<Readyplay>(m => m.Id == int.Parse(item.OrderNb), y => y.Cinema, y => y.Movie, y => y.Home);
                string []str= item.Site.Split(',');

                and.Add(new MovieWritInfo() {
                    MovieName = item.Movie.Name,
                    CinemaName = item.Cinema.Name,
                    Readplayid = int.Parse(item.OrderNb),
                    Date = obox.StatTime,
                    MovieCount = str.Count(),
                    Site = item.Site,
                    RealityMone = item.RealityMone,
                    GainNumber = item.GainNumber,
                    Indent = item.Id
                });
            }
            return and;
        }
        [Route("SelectDealInfo"), HttpPost]
        public ReadyplayModel SelectDealInfo(int id,int Indent)
        {
            var data= _DbService.GetSingleByID<Readyplay>(m => m.Id == id,m => m.Cinema, m => m.Movie, m => m.Home);
            var dealinfo= _DbService.GetSingleByID<Deal>(m => m.Id == Indent, y => y.Cinema, y => y.Movie, y => y.User);
            ReadyplayModel model = new ReadyplayModel(data);
            model.DealSite = dealinfo.Site;
            model.DealRealityMone = dealinfo.RealityMone;
            model.DealGainNumber = dealinfo.GainNumber;
            return model;
        }
            //    [HttpGet]
            //    [HttpGet("SelectSeat")]
            //    public IEnumerable<ReadyplayModel> (int id)
            //    {

            //    }
            //[HttpPut("{id}")]
            //public void Put(int id, [FromBody]string value)
            //{
            //}

            //// DELETE api/values/5
            //[HttpDelete("{id}")]
            //public void Delete(int id)
            //{
            //} 
        }
}
