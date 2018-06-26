using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using Dnc.Entities.Application;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities;
using System.Drawing;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Dnc.MvcApp.Filters;

namespace Dnc.MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEntityRepository _Service;

        public HomeController(IEntityRepository service)
        {
            _Service = service;
        }

        [Route("")]
        [Route("首页")]
        [Route("首页/Index")]
        //[AppAuthorityFilter(new string[] { "管理员", "超级管理员" })]
        public IActionResult Index()
        {
            return View();
        }
       

        [Route("首页/关于我们")]
        public IActionResult About()
        {
            ViewData["Message"] = "这是 关于我们 页面的内容。";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "这是 联系方式 的内容。";

            return View();
        }
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return View("../../Views/Home/Index");
        }
        
        public IActionResult Error(string errorMeg)
        {
            ViewBag.ErrorMessage = errorMeg;
            return View();
        }

    }
}