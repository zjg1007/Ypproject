using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.Entities.Business;
using Dnc.MvcApp.Filters;
using Dnc.DataAccessRepository.Repositories;
using Dnc.ViewModels.Article;
using Dnc.Entities.Application;
using Microsoft.AspNetCore.Authorization;
using Dnc.Entities;
using Microsoft.AspNetCore.Http;
using Dnc.MvcApp;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Dnc.MvcApp.ViewsModels;

namespace Dnc.MvcApp.Controllers
{
    /// <summary>
    /// 文章类型管理控制器
    /// </summary>
    public class ArticleTypeController : Controller
    {
        private readonly IEntityRepository _DbService;

        public ArticleTypeController(IEntityRepository service)
        {
            this._DbService = service;
        }

        [HttpGet]
        [AppAuthorityFilter(new string[] { "管理员","超级管理员"})]
        public IActionResult Index(List<ApplicationUser> data, int id = 1)
        {
            String userid =HttpContext.Session.GetString("LogonSystemUserID");
            var boCollection = _DbService.GetAll<ApplicationUser>(m=>m.Group).Where(m=>m.ID!= userid&&m.Group.Name!= "超级管理员").ToList();   // 提取所有的文章类型数据
            foreach (var item in boCollection)
            {
                var user = _DbService.GetSingleBy<ApplicationGroup>(m => m.ID == item.ApplicationGroupId);
                item.Group = user;
            }
            var pageOption = new MoPagerOption
            {
                CurrentPage = id,
                PageSize = 10,
                Total = boCollection.Count(),
                RouteUrl = "/Acticle/Index"
            };

            //分页参数
            ViewBag.PagerOption = pageOption;

            //数据
            return View(boCollection.OrderByDescending(b => b.Time).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToList());
        }
        [HttpPost]
        public IActionResult Index(Guid Id, int id = 1)
        {
            var Sbstring = Request.Form["name"].ToString();
            var data = _DbService.GetAll<ApplicationUser>().Where(k=>k.Phonr.Contains(Sbstring)) ;
            foreach (var item in data)
            {
                var user = _DbService.GetSingleBy<ApplicationGroup>(m => m.ID == item.ApplicationGroupId);
                item.Group = user;
            }
            var pageOption = new MoPagerOption
            {
                CurrentPage = id,
                PageSize = 10,
                Total = data.Count(),
                RouteUrl = "/Acticle/Index"
            };

            //分页参数
            ViewBag.PagerOption = pageOption;

            //数据
            return View(data.OrderByDescending(b => b.Time).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToList());
        }
        [HttpGet]
        [AppAuthorityFilter(new string[] { "管理员", "超级管理员" })]
        public IActionResult Detail(Guid ID)
        {
            var user = _DbService.GetSingle<ApplicationUser>(ID, x=>x.Group);
            var uservm = new UserVm(user); 
            return View(uservm);
        }
        [HttpPost]
        public IActionResult Detail(UserVm us)
        {
            string usergrp = HttpContext.Session.GetString("userGruop");
            string radius = Request.Form["grup"].ToString();
            if (usergrp == radius)
            {
                return Content("您的不足以完成此操作,如需操作请联系管理员");
            }
            else
            {
                var gru = _DbService.GetSingleBy<ApplicationGroup>(m => m.Name == radius);
                var user = _DbService.GetSingle<ApplicationUser>(Guid.Parse(us.ID));
                user.Name = us.Name;
                user.Evaluation = us.Evaluation;
                user.Group = gru;
                _DbService.EditAndSave<ApplicationUser>(user);
                us = new UserVm(user);
                return View(us);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            var boVM = new UserVm(new ApplicationUser());
            return View(boVM);
        }
       
        //[HttpPost]
        //public async Task<IActionResult> Save(ArticleTypeVM boVM)
        //{
        //    var isOK = false;
        //    await Task.Run(() => {
        //        if (ModelState.IsValid)
        //        {
        //            // 检查是否已经存在 ArticleType 实例
        //            var id = Guid.Parse(boVM.ID);
        //            var bo = _DbService.GetSingle<ArticleType>(id);
        //            if (bo == null)
        //            {
        //                bo = new ArticleType();
        //                boVM.MapBo(bo);
        //                _DbService.AddAndSave(bo);
        //            }
        //            else
        //            {
        //                boVM.MapBo(bo);
        //                _DbService.EditAndSave(bo);
        //            }

        //            isOK = true;
        //        }
        //    });
        //    if (isOK)
        //        return RedirectToAction("Index");
        //    else
        //        return View();
        //}

    

        //public IActionResult TalentRecruitment(string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    return View();
        //}

        ///// <summary>
        ///// 公司数据信息处理
        ///// </summary>
        ///// <param name="model">前端视图提供的 model 数据</param>
        ///// <param name="returnUrl"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> TalentRecruitment(RecruitmentViewModel model, string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    if (ModelState.IsValid)
        //    {
        //        Recruitment recruitment = null;
        //        //if (recruitment == null)
        //        //{
        //        //    ModelState.AddModelError("输入错误", "请输入要填写的资料");
        //        //    return View("../../Views/ArticleTypeController/TalentRecruitment", model);
        //        //}
        //        await Task.Run(() =>
        //        {
        //            recruitment = _Service.GetSingleBy<Recruitment>(x => x.CompanyIntroduction == model.CompanyIntroduction);
        //        });
        //        if (recruitment != null)
        //        {
        //            // 处理重复用户名问题
        //            ModelState.AddModelError("逻辑错误", "该公司已经注册了，请重新输入。");
        //            return View("../../Views/ArticleTypeController/TalentRecruitment", model);
        //        }
        //        else
        //        {
        //            recruitment = new Recruitment();
        //            recruitment.Wage = model.Wage;
        //            recruitment.ReleaseTime = model.R_ReleaseTime;
        //            recruitment.WorkExperience = model.WorkExperience;
        //            recruitment.Hiring = model.Hiring;
        //            recruitment.WorkingPlace = model.WorkingPlace;
        //            recruitment.JobCategory = model.JobCategory;
        //            recruitment.JobDescription = model.JobDescription;
        //            //recruitment.ApplicationGroupId = group.ID;
        //            recruitment.CompanyIntroduction =model . CompanyIntroduction;
        //            recruitment.ForSchool = model.ForSchool;
        //            _Service.AddAndSave<Recruitment>(recruitment);
        //            return Redirect("../../TalentRecruitment");
        //        }
        //    }
        //    return View("../../Views/ArticleTypeController/TalentRecruitment", model);
        //}
    }
}
