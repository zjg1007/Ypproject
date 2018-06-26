using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Application;
using Dnc.ViewModels.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Dnc.MvcApp.Filters;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using System.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Dnc.MvcApp.Controllers
{
    public class ActorController : Controller
    {
        private readonly IEntityRepository _DbService;
        private IHostingEnvironment hostingEnv;
        public ActorController(IEntityRepository service, IHostingEnvironment env)
        {
            this._DbService = service;
            this.hostingEnv = env;
        }
        // GET: /<controller>/
        /// <summary>
        /// 演员列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(int id=1)
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
                 Total =  actor.Count(),
                 RouteUrl = "/Actor/Index"
             };

            //分页参数
            ViewBag.PagerOption = pageOption;

            //数据
            return View( actor.OrderByDescending(b => b.BirthDate).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToList());
        }
        /// <summary>
        /// 演员列表（根据演员名称模糊查询）
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index(Guid guid, int id=1)
        {
            var Sbstring = Request.Form["name"].ToString();
            List<Actor> data = new List<Actor>();
            await Task.Run(() =>
            {
                data = _DbService.GetAllID<Actor>().Where(m=>m.Name.Contains(Sbstring)).ToList();
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
        /// 演员新增
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        /// <summary>
        /// 判断是否有同名演员（确定-添加 取消-不添加）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<LogonUserStatus> ActorInsert(ActorStatus model, IList<IFormFile> files, string returnUrl = null)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = ""
            };
            ViewData["ReturnUrl"] = returnUrl;
            Actor user = null;
            var data = 0;
            if (ModelState.IsValid)
            {
                await Task.Run(() =>
                {
                     data = _DbService.GetAllID<Actor>().Where(m => m.Name == model.Name).ToList().Count;
                });
                if (data>0)
                {
                    logonStatus.IsLogon = false;
                    logonStatus.Message = "";
                }
                else
                {
                    SubImg(files);
                    user = new Actor();
                    user.Birthplace = model.Birthplace;
                    user.Name = model.Name;
                    user.BirthDate = model.BirthDate.Date;
                    user.ElseName = model.ElseName;
                    user.Intro = model.Intro;
                    user.Photo = HttpContext.Session.GetString("img").ToString();
                    user.Profession = model.Profession;
                    _DbService.AddAndSaveID<Actor>(user);
                    logonStatus.IsLogon = true;
                }
                return logonStatus;
            }
            return logonStatus;
        }
        [HttpPost]
        public async Task<IActionResult> Create(ActorStatus model, IList<IFormFile> files, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
                Actor user = null;
            SubImg(files);
            await Task.Run(() =>
            {
                user = new Actor();
                user.Birthplace = model.Birthplace;
                user.Name = model.Name;
                user.BirthDate = model.BirthDate.Date;
                user.ElseName = model.ElseName;
                user.Intro = model.Intro;
                user.Profession = model.Profession;
                user.Photo = HttpContext.Session.GetString("img");
                _DbService.AddAndSaveID<Actor>(user);
            });
            return Redirect("../../Actor/Index");
        }

        private void SubImg( IList<IFormFile> files)
        {
            long size = 0;
                foreach (var file in files)
                     {
                        var filename = ContentDispositionHeaderValue
                                       .Parse(file.ContentDisposition)
                                        .FileName
                                       .Trim();
                HttpContext.Session.SetString("img", "/"+filename);
                filename = hostingEnv.WebRootPath + $@"\{filename}";
                         size += file.Length;
                       using (FileStream fs = System.IO.File.Create(filename.ToString()))
                             {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
            }
             string img= $"{files.Count} file(s) / { size} bytes uploaded successfully!";
            ViewBag.Message = img;
        }
        /// <summary>
        /// 删除演员信息
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int Id,string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var data= _DbService.GetSingleByID<Actor>(m=>m.Id== Id);
            _DbService.DeleteAndSaveID<Actor>(data);
            return Redirect("../../Actor/Index");
        }
        /// <summary>
        /// 演员信息更改
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int Id, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var data= _DbService.GetSingleByID<Actor>(m => m.Id == Id);
            HttpContext.Session.SetString("acid", Id.ToString());
            ActorStatus acinto = new ActorStatus();
            acinto.Birthplace = data.Birthplace;
            acinto.BirthDate = data.BirthDate;
            acinto.ElseName = data.ElseName;
            acinto.Name = data.Name;
            acinto.Intro = data.Intro;
            acinto.Profession = data.Profession;
            acinto.Photo = data.Photo;
            return View(acinto);
        }
        [HttpPost]
        public IActionResult Edit(ActorStatus model, IList<IFormFile> files, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var data = _DbService.GetSingleByID<Actor>(m => m.Id == int.Parse( HttpContext.Session.GetString("acid")));
            data.Birthplace = model.Birthplace;
            data.BirthDate = model.BirthDate;
            data.ElseName = model.ElseName;
            data.Name = model.Name;
            data.Intro = model.Intro;
            data.Profession = model.Profession;
            if (files.Count > 0)
            {
                SubImg(files);
                data.Photo = HttpContext.Session.GetString("img").ToString();
            }
            _DbService.EditAndSaveID<Actor>(data);
            ActorVM acinto = new ActorVM(data);
            return Redirect("../../Actor/Index");
        }
    }
}
