using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.Entities.Business;
using Microsoft.AspNetCore.Hosting;
using Dnc.DataAccessRepository.Repositories;
using Dnc.MvcApp.Filters;
using Dnc.Entities.Application;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Dnc.MvcApp.Controllers
{
    public class ProsceniumController : Controller
    {
        private readonly IEntityRepository _DbService;
        private IHostingEnvironment hostingEnv;
        public ProsceniumController(IEntityRepository service, IHostingEnvironment env)
        {
            this._DbService = service;
            this.hostingEnv = env;

        }
        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> Index(int id = 1,string returnUrl = null)
        {
            var data = new List<Readyplay>();
            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Readyplay>(m=>m.Cinema,m=>m.Movie,m=>m.Home).ToList();
            });
            var pageOption = new MoPagerOption
            {
                CurrentPage = id,
                PageSize = 10,
                Total = data.Count(),
                RouteUrl = "/Proscenium/Index"
            };

            //分页参数
            ViewBag.PagerOption = pageOption;

            //数据
            return View(data.OrderByDescending(b => b.StatTime).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Index(int id = 1)
        {
            string strlifo = HttpContext.Request.Form["selectInfo"].ToString() ?? "";
            string name = HttpContext.Request.Form["name"].ToString() ?? "";
            var rem = new List<Readyplay>();
            await Task.Run(() =>
            {
                if (strlifo == "影院")
                {
                    var data = _DbService.GetAllID<Cinema>().Where(m => m.Name.Contains(name)).ToList();
                    foreach (var item in data)
                    {
                        var and = _DbService.GetAllID<Readyplay>(m=>m.Cinema,m=>m.Movie,m=>m.Home).Where(m=>m.Cinema.Id==item.Id).ToList();
                        rem.AddRange(and);
                    }
                }
                else if (strlifo == "电影")
                {
                    var data = _DbService.GetAllID<Movie>(m=>m.TypeInfo).Where(m => m.Name.Contains(name)).ToList();
                    foreach (var item in data)
                    {
                        var and = _DbService.GetAllID<Readyplay>(m => m.Cinema, m => m.Movie, m => m.Home).Where(m => m.Movie.Id == item.Id).ToList();
                        rem.AddRange(and);
                    }
                }
                else
                {
                    var data = _DbService.GetAllID<Home>().Where(m => m.Name.Contains(name)).ToList();
                    foreach (var item in data)
                    {
                        var and = _DbService.GetAllID<Readyplay>(m => m.Cinema, m => m.Movie, m => m.Home).Where(m => m.Home.Id == item.Id).ToList();
                        rem.AddRange(and);
                    }
                }
               
            });
            var pageOption = new MoPagerOption
            {
                CurrentPage = id,
                PageSize = 10,
                Total = rem.Count(),
                RouteUrl = "/Actor/Index"
            };

            //分页参数
            ViewBag.PagerOption = pageOption;

            //数据
            return View(rem.OrderByDescending(b => b.StatTime).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToList());
        }
        /// <summary>
        ///上映信息新增
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create(int id = 1, string returnUrl = null)
        {
            return View();
        }
        [HttpPost]
        public async Task<LogonUserStatus> Create(Readyplay model, string returnUrl = null)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "添加失败,请稍后重试"
            };
                await Task.Run(() =>
                {
                   var ocine= _DbService.GetSingleByID<Cinema>(m => m.Id == model.CinemaID);
                    var oHome=_DbService.GetSingleByID<Home>(m=>m.Id==model.HomeID);
                    var oMovie = _DbService.GetSingleByID<Movie>(m => m.Id == model.MovieID);
                    string movieID = HttpContext.Request.Form["MovieID"];
                    string tarTime = HttpContext.Request.Form["startTime"];
                    string endTime = HttpContext.Request.Form["endendTime"];
                    if (tarTime.Contains(","))
                    {
                        string[] start;
                        string[] endDate;
                        start = tarTime.Split(',');
                        endDate = endTime.Split(',');
                        for (int i = 0; i < start.Length; i++)
                        {

                            Readyplay rem = new Readyplay();
                            rem.Cinema = ocine;
                            rem.Home = oHome;
                            rem.CinemaID = ocine.Id;
                            rem.HomeID = oHome.Id;
                            rem.Price = model.Price;
                            rem.Movie = oMovie;
                            rem.MovieID = oMovie.Id;
                            DateTime sta = Convert.ToDateTime(start[i]);
                            DateTime end = Convert.ToDateTime(endDate[i]);
                            rem.StatTime = sta;
                            rem.EndTime = end;
                            _DbService.AddID<Readyplay>(rem);
                        }
                    }
                    else
                    {
                        Readyplay rem = new Readyplay();
                        rem.Cinema = ocine;
                        rem.Home = oHome;
                        rem.CinemaID = ocine.Id;
                        rem.HomeID = oHome.Id;
                        rem.Price = model.Price;
                        rem.Movie = oMovie;
                        rem.MovieID = oMovie.Id;
                        DateTime sta = Convert.ToDateTime(tarTime);
                        DateTime end = Convert.ToDateTime(endTime);
                        rem.StatTime = sta;
                        rem.EndTime = end;
                        _DbService.AddID<Readyplay>(rem);
                    }
                    _DbService.Save();
                    logonStatus.IsLogon = true;
                    logonStatus.Message = "添加成功";
                });
            return logonStatus;
        }
        /// <summary>
        /// 上映信息修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id, string returnUrl = null)
        {
             var data=_DbService.GetSingleByID<Readyplay>(m => m.Id == id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Readyplay model, string returnUrl = null)
        {
            await Task.Run(() =>
            {
                var data = _DbService.GetSingleByID<Readyplay>(m => m.Id == model.Id);
                data.Price = model.Price;
                var ocine = _DbService.GetSingleByID<Cinema>(m => m.Id == model.CinemaID);
                var oHome = _DbService.GetSingleByID<Home>(m => m.Id == model.HomeID);
                var oMovie = _DbService.GetSingleByID<Movie>(m => m.Id == model.MovieID);
                data.Cinema = ocine;
                data.CinemaID = ocine.Id;
                data.Home = oHome;
                data.HomeID = oHome.Id;
                data.Movie = oMovie;
                data.MovieID = oMovie.Id;
                data.StatTime = model.StatTime;
                data.EndTime = model.EndTime;
                _DbService.EditAndSaveID<Readyplay>(data);
            });
            return Redirect("/Proscenium/index");
        }
        /// <summary>
        /// 上映信息删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int id, string returnUrl = null)
        {
            var data = _DbService.GetSingleByID<Readyplay>(m => m.Id == id);
            _DbService.DeleteAndSaveID<Readyplay>(data);
            return Redirect("/Proscenium/index");
        }

    }
}
