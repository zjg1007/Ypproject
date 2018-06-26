using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.DataAccessRepository.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Dnc.Entities.Business;
using Dnc.ViewModels.Article;
using Dnc.MvcApp.Filters;
using Dnc.Entities.Application;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Dnc.MvcApp.Controllers
{
    public class CinemaController : Controller
    {
        private readonly IEntityRepository _DbService;
        private IHostingEnvironment hostingEnv;
        private  BytesHelper bh=new BytesHelper();
        public CinemaController(IEntityRepository service, IHostingEnvironment env)
        {
            this._DbService = service;
            this.hostingEnv = env;

        }
        // GET: /<controller>/
        /// <summary>
        /// 影院信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(int id = 1)
        {
            List<Cinema> data = new List<Cinema>();
            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Cinema>().ToList();
            });
            var actor = new List<CinemaVM>();
            foreach (var item in data)
            {
                actor.Add(new CinemaVM(item));
            }
            var pageOption = new MoPagerOption
            {
                CurrentPage = id,
                PageSize = 10,
                Total = actor.Count(),
                RouteUrl = "/Actor/Index"
            };

            //分页参数
            ViewBag.PagerOption = pageOption;
            HttpContext.Session.SetString("addActor", "");
            HttpContext.Session.SetString("id", "");
            //数据
            return View(actor.Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Index(Guid guid, int id = 1)
        {
            var Sbstring = Request.Form["name"].ToString();
            List<Cinema> data = new List<Cinema>();
            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Cinema>().Where(m => m.Name.Contains(Sbstring)).ToList();
            });
            var actor = new List<CinemaVM>();
            foreach (var item in data)
            {
                actor.Add(new CinemaVM(item));
            }
            var pageOption = new MoPagerOption
            {
                CurrentPage = id,
                PageSize = 10,
                Total = actor.Count(),
                RouteUrl = "/Actor/Index"
            };

            //分页参数
            ViewBag.PagerOption = pageOption;

            //数据
            return View(actor.Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToList());
        }
        /// <summary>
        /// 影院添加
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Cinmaparse model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                Cinema cn = new Cinema();
                cn.Name = model.Name;
                cn.Site = model.Site;
                cn.Region = model.Region;
                cn.Serve = model.Serve;
                _DbService.AddAndSaveID<Cinema>(cn);
                return Redirect("/Cinema/index");
            }
            else
            {
                return View("../../Views/Cinema/Create", model);
            }

        }
        /// <summary>
        /// 影院信息编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var data = _DbService.GetSingleByID<Cinema>(m => m.Id == id);
            Cinmaparse cn = new Cinmaparse();
            cn.Id = data.Id;
            cn.Name = data.Name;
            cn.Site = data.Site;
            cn.Region = data.Region;
            cn.Serve = data.Serve;
            return View(cn);
        }
        [HttpPost]
        public IActionResult Edit(Cinmaparse model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var cnd = _DbService.GetSingleByID<Cinema>(m => m.Id == model.Id);
                cnd.Name = model.Name;
                cnd.Site = model.Site;
                cnd.Region = model.Region;
                cnd.Serve = model.Serve;
                _DbService.EditAndSaveID<Cinema>(cnd);
                return Redirect("/Cinema/index");
            }
            else
            {
                return View("../../Views/Cinema/Edit", model);
            }

        }
        /// <summary>
        /// 查看影院影厅信息（新增）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Detail(int id,string returnUrl = null)
        {
            if (id != 0) {
                HttpContext.Session.SetString("cinemaID", id.ToString());
            }
            int snd =int.Parse( HttpContext.Session.GetString("cinemaID"));
            await Task.Run(() =>
            {
                //影厅信息
                var data = _DbService.GetAllID<Home>().Where(m => m.CinemaID == snd).ToList();
                if (data.Count > 0)
                {
                    ViewData["homelist"] = data;
                }
                else
                {
                    ViewData["homelist"] = null;
                }
                //即将上映
                var dataInfo = _DbService.GetAllID<Readyplay>(m=>m.Movie).Where(m=>m.CinemaID== snd).ToList();
                if (dataInfo.Count > 0)
                {
                    ViewData["movielist"] = dataInfo;
                }
                else
                {
                    ViewData["movielist"] = null;
                }
                string name=_DbService.GetSingleByID<Cinema>(m => m.Id == snd).Name;
                ViewData["cinemaName"] = name;
               HttpContext.Session.SetString("cinemaName", name);
            });
            return View();
        }
        [HttpPost]
        public async Task<LogonUserStatus> Detail(HomeVM model)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "添加失败,请稍后重试"
            };
            if (ModelState.IsValid)
            {
                string strid = HttpContext.Session.GetString("cinemaID");
                string Place = HttpContext.Request.Form["columnList"];
                await Task.Run(() =>
                {
                    Home hm = new Home();
                    model.MapBo(hm);
                    hm.CinemaID = int.Parse(strid);
                    hm.Place = Place;
                    _DbService.AddAndSaveID<Home>(hm);
                });
                logonStatus.IsLogon = true;
                logonStatus.Message = "添加成功";
            }
            else {
                logonStatus.IsLogon = false;
            }
            return logonStatus;
            
        }
        /// <summary>
        /// 影厅管理
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> HomeEdit(int id, string returnUrl = null)
        {
            HomeVM hm = new HomeVM();
            await Task.Run(() =>
            {
                var data = _DbService.GetSingleByID<Home>(m => m.Id == id);
                ViewData["cinemaName"] = HttpContext.Session.GetString("cinemaName");
                hm = new HomeVM(data);
                ViewData["HomeName"] = data.Name;
            });
            return View(hm);
        }
        [HttpPost]
        public async Task<LogonUserStatus> HomeEdit(HomeVM model, string returnUrl = null)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "添加失败,请稍后重试"
            };
            if (ModelState.IsValid)
            {
                string strid = HttpContext.Session.GetString("cinemaID");
                string Place = HttpContext.Request.Form["columnList"];
                await Task.Run(() =>
                {
                    var data=_DbService.GetSingleByID<Home>(m => m.Id == model.Id);
                    data.Name = model.Name;
                    data.Quality = model.Quality;
                    data.Place = Place;
                    _DbService.EditAndSaveID<Home>(data);
                });
                logonStatus.IsLogon = true;
                logonStatus.Message = "保存成功";
            }
            else
            {
                logonStatus.IsLogon = false;
            }
            return logonStatus;
        }
      
    }
}
