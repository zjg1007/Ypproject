using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.Entities.Business;
using Dnc.ViewModels.Article;
using Dnc.DataAccessRepository.Repositories;
using Microsoft.AspNetCore.Hosting;
using Dnc.MvcApp.Filters;
using Dnc.Entities.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Dnc.MvcApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly IEntityRepository _DbService;
        private IHostingEnvironment hostingEnv;
        public MovieController(IEntityRepository service, IHostingEnvironment env)
        {
            this._DbService = service;
            this.hostingEnv = env;
            
        }
        // GET: /<controller>/
        /// <summary>
        /// 电影列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(int id=1)
        {
            List<Movie> data = new List<Movie>();
            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Movie>().ToList();
            });
            var actor = new List<MovieVM>();
            foreach (var item in data)
            {
                    actor.Add(new MovieVM(item));
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
            return View(actor.OrderByDescending(b => b.LaunchName).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Index(Guid guid, int id = 1)
        {
            var Sbstring = Request.Form["name"].ToString();
            List<Movie> data = new List<Movie>();
            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Movie>().Where(m=>m.Name.Contains(Sbstring)).ToList();
            });
            var actor = new List<MovieVM>();
            foreach (var item in data)
            {
                    actor.Add(new MovieVM(item));
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
            return View(actor.OrderByDescending(b => b.LaunchName).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToList());
        }
        /// <summary>
        /// 电影新增
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["TypeList"] = _DbService.GetAllID<MType>().ToList();
            ActorInfo();
            return View();
        }
        public void ActorInfo() {
            string str = HttpContext.Session.GetString("addActor") ?? "";
            var actor = new List<Actor>();
            if (str != ""&&str!=null)
            {
                string[] and = str.Split(',');
                for (int i = 0; i < and.Length; i++)
                {
                    if (and[i] != "") {
                        var data = _DbService.GetSingleByID<Actor>(m => m.Id == int.Parse(and[i]));
                        actor.Add(data);
                    }
                }
                ViewData["ActorList"] = actor.ToList();
            }
            else
            {
                ViewData["ActorList"] = null;
            }
        }
        [HttpPost]
        public async Task<LogonUserStatus> Create(MovieSubclass model, IList<IFormFile> files, string returnUrl = null)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = ""
            };
            try
            {
                ViewData["ReturnUrl"] = returnUrl;
                await Task.Run(() =>
                {
                    string typeList = HttpContext.Request.Form["typelist"].ToString();
                    string acstr = HttpContext.Session.GetString("addActor") ?? "";
                    SubImg(files);
                    Movie mo = new Movie();
                    mo.Referral = model.Referral;
                    mo.Name = model.Name;
                    mo.Country = model.Country;
                    mo.Nickname = model.Nickname;
                    mo.Duration = model.Duration + "分钟";
                    mo.Protagonist = model.Protagonist;
                    mo.Actor = acstr;
                    mo.Picture = HttpContext.Session.GetString("img");
                    mo.Mass = model.Mass;
                    mo.TypeId = typeList;
                    mo.LaunchName = model.LaunchName;
                    _DbService.AddAndSaveID<Movie>(mo);
                });
                logonStatus.IsLogon = true;
            }
            catch (Exception)
            {
                logonStatus.IsLogon = false;
            }
            return logonStatus;
        }
        private void SubImg(IList<IFormFile> files)
        {
            long size = 0;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                               .Parse(file.ContentDisposition)
                                .FileName
                               .Trim();
                HttpContext.Session.SetString("img", "/" + filename);
                filename = hostingEnv.WebRootPath + $@"\{filename}";
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename.ToString()))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            string img = $"{files.Count} file(s) / { size} bytes uploaded successfully!";
            ViewBag.Message = img;
        }
        /// <summary>
        /// 电影演员添加
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> MovieActorInsert(int id=1)
        {
            List<Actor> data = new List<Actor>();
            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Actor>().ToList();
            });
            var actor = new List<ActorVM>();
            foreach (var item in data)
            {
                actor.Add(new ActorVM(item));
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
            return View(actor.OrderByDescending(b => b.BirthDate).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> MovieActorInsert(Guid guid, int id = 1)
        {
            var Sbstring = Request.Form["name"].ToString();
            List<Actor> data = new List<Actor>();
            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Actor>().Where(m => m.Name.Contains(Sbstring)).ToList();
            });
            var actor = new List<ActorVM>();
            foreach (var item in data)
            {
                actor.Add(new ActorVM(item));
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
            return View(actor.OrderByDescending(b => b.BirthDate).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToList());
        }
        /// <summary>
        /// 添加演员
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public LogonUserStatus AddActor(int Id)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = ""
            };
            string str= HttpContext.Session.GetString("addActor") ??"";
            string[] strList;
            bool istrue = true;
            string id = Convert.ToString(Id);
            if (str.Contains(",") || str != "")
            {
                strList = str.Split(',');
                for (int i = 0; i < strList.Length; i++)
                {
                    if (strList[i] == id)
                    {
                        istrue = false;
                        break;
                    }
                }
                if (istrue == true)
                {
                    str += "," + id;
                }
            }
            else 
            {
                str += id;
            }
            
            
            if (istrue)
            {
                HttpContext.Session.SetString("addActor", str);
                logonStatus.IsLogon = true;
                logonStatus.Message = "添加成功";
            }
            else {
                logonStatus.IsLogon = false;
                logonStatus.Message = "不能重复添加,请重新选择";
            }
            return logonStatus;
        }
        /// <summary>
        /// 删除演员
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<LogonUserStatus> RemoveActor(int Id)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = ""
            };
            try
            {
                await Task.Run(() =>
            {
                string strlist = "";

            string str = HttpContext.Session.GetString("addActor") ?? "";
            string[] sttr = str.Split(',');
            for (int i = 0; i < sttr.Length; i++)
            {
                if (sttr[i] != Id.ToString()&&sttr[i]!="")
                {
                        strlist += sttr[i] + ",";
                }
            }
                strlist = strlist.Substring(0, strlist.Length - 1);
            HttpContext.Session.SetString("addActor", strlist);
            });
                logonStatus.IsLogon = true;
            }
            catch (Exception)
            {
                logonStatus.IsLogon = false;
            }
            return logonStatus;
        }
        /// <summary>
        /// 删除电影信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
                await Task.Run(() =>
                {
                    var del = _DbService.GetSingleByID<Movie>(m => m.Id == id);
                    _DbService.DeleteAndSaveID<Movie>(del);
                });
            return Redirect("/Movie/Index");
        }
        /// <summary>
        /// 电影编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            MovieSubclass model=new MovieSubclass();
            await Task.Run(() =>
            {
                var cmd=new Movie();
                if (id != 0)
                {
                    HttpContext.Session.SetString("id", id.ToString());
                    cmd = _DbService.GetSingleByID<Movie>(m => m.Id == id);
                }
                else {
                    cmd = _DbService.GetSingleByID<Movie>(m => m.Id == int.Parse(HttpContext.Session.GetString("id")));
                    cmd.Actor = "";
                }
                var actor = new List<Actor>();
            if (cmd.Actor != "" && cmd.Actor != null)
                {
                    string[] and = cmd.Actor.Split(',');
                    for (int i = 0; i < and.Length; i++)
                    {
                        var data = _DbService.GetSingleByID<Actor>(m => m.Id == int.Parse(and[i]));
                        actor.Add(data);
                    }
                    HttpContext.Session.SetString("addActor", cmd.Actor);
                    ViewData["ActorList"] = actor.ToList();
                }
                else
                {
                        ActorInfo();
                }
                ViewData["TypeList"] = _DbService.GetAllID<MType>().ToList();
                cmd.Duration = cmd.Duration.Substring(0, cmd.Duration.Length - 2);
                model = new MovieSubclass()
                {
                    Id = cmd.Id,
                    Referral = cmd.Referral,
                    Name = cmd.Name,
                    Country = cmd.Country,
                    Nickname = cmd.Nickname,
                    Duration = cmd.Duration,
                    Actor = cmd.Actor,
                    Grade = cmd.Grade,
                    Picture = cmd.Picture,
                    Mass = cmd.Mass,
                    TypeId = cmd.TypeId,
                    Protagonist = cmd.Protagonist,
                    LaunchName=cmd.LaunchName
                };
            });
            return View(model);
        }
        [HttpPost]
        public async Task<LogonUserStatus> Edit(MovieSubclass model, IList<IFormFile> files, string returnUrl = null)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = ""
            };
            try
            {
                ViewData["ReturnUrl"] = returnUrl;
                await Task.Run(() =>
                {
                    string typeList = HttpContext.Request.Form["typelist"].ToString();
                    string acstr = HttpContext.Session.GetString("addActor") ?? "";
                    var mo = _DbService.GetSingleByID<Movie>(m => m.Id == int.Parse(HttpContext.Session.GetString("id")));
                    if (files.Count>0)
                    {
                        SubImg(files);
                        mo.Picture = HttpContext.Session.GetString("img");
                    }
                        mo.Referral = model.Referral;
                        mo.Name = model.Name;
                        mo.Country = model.Country;
                        mo.Nickname = model.Nickname;
                        mo.Duration = model.Duration+"分钟";
                        mo.Actor = acstr;
                        mo.Mass = model.Mass;
                        mo.TypeId = typeList;
                        mo.Protagonist = model.Protagonist;
                        mo.LaunchName = model.LaunchName;
                        _DbService.EditAndSaveID<Movie>(mo);
                logonStatus.IsLogon = true;
                });
            }
            catch (Exception)
            {
                logonStatus.IsLogon = false;
            }
            return logonStatus;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id = 1)
        {
            List<Actor> data = new List<Actor>();
            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Actor>().ToList();
            });
            var actor = new List<ActorVM>();
            foreach (var item in data)
            {
                actor.Add(new ActorVM(item));
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
            return View(actor.OrderByDescending(b => b.BirthDate).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Detail(Guid guid, int id = 1)
        {
            var Sbstring = Request.Form["name"].ToString();
            List<Actor> data = new List<Actor>();
            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Actor>().Where(m => m.Name.Contains(Sbstring)).ToList();
            });
            var actor = new List<ActorVM>();
            foreach (var item in data)
            {
                actor.Add(new ActorVM(item));
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
            return View(actor.OrderByDescending(b => b.BirthDate).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToList());
        }
        [HttpGet]
        public async Task<IActionResult> LaunchInfo(int id)
        {
            List<Launch> data = new List<Launch>();
            Movie lan = new Movie();
            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Launch>().Where(m=>m.MovieID==id).ToList();
                lan = _DbService.GetSingleByID<Movie>(m => m.Id==id);
            });
            ViewData["name"] = lan.Name;
            HttpContext.Session.SetString("launchID", id.ToString());
            HttpContext.Session.SetString("addActor", "");
            HttpContext.Session.SetString("id", "");
            //数据
            return View(data.ToList());
        }
        [HttpPost]
        public async Task<LogonUserStatus> LaunchInfo()
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = ""
            };
            try
            {
                string str = "";
                string str_1 = "";
                string[] arrtStr;
                string[] arrtend;
                int laid = int.Parse(HttpContext.Session.GetString("launchID"));
                await Task.Run(() =>
                {
                    var data = _DbService.GetAllID<Launch>().Where(m => m.MovieID == laid);
                    foreach (var item in data)
                    {
                        _DbService.DeleteID<Launch>(item);
                    }
                    _DbService.Save();
                    str = HttpContext.Request.Form["strdate"];
                    str_1 = HttpContext.Request.Form["str"];
                    arrtStr = str.Split(',');
                    arrtend = str_1.Split(',');
                    for (int i = 0; i < arrtStr.Length; i++)
                    {
                        Launch land = new Launch();
                        DateTime dt = DateTime.ParseExact(arrtStr[i], "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                        land.Date = dt;
                        land.Country = arrtend[i];
                        land.Region = arrtend[i];
                        var moData = _DbService.GetSingleByID<Movie>(m => m.Id == laid);
                        land.Movie = moData;
                        land.MovieID = moData.Id;
                        _DbService.AddID<Launch>(land);
                    }
                    _DbService.Save();
                    logonStatus.IsLogon = true;
                    logonStatus.Message = "成功";
                });
            }
            catch (Exception)
            {
                logonStatus.IsLogon = false;
                logonStatus.Message = "失败";
            }
            return logonStatus;
            }
        }
}
