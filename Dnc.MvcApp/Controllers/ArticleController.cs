using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Application;
using Dnc.Entities.Business;
using Dnc.ViewModels.Article;
using Dnc.MvcApp.Filters;
using Microsoft.AspNetCore.Http;

namespace Dnc.MvcApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IEntityRepository _DbService;

        public ArticleController(IEntityRepository service)
        {
            this._DbService = service;
        }

        [HttpGet]
        [AppAuthorityFilter(new string[] { "普通用户", "管理员" })]
        public IActionResult Index()
        {
            //var boCollection = _DbService.GetAll<Enterprise>().ToList();   // 提取所有的文章类型数据
            return View();
        }
        [HttpPost]
        public IActionResult Index(Guid id)
        {
            //var Sbstring = Request.Form["name"].ToString();
            //var data = _DbService.GetAll<Enterprise>().Where(k => k.Account.Contains(Sbstring));
            return View();
        }
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    var boVM = new UserVm(new ApplicationUser());
        //    return View(boVM);
        //}
    }
}
