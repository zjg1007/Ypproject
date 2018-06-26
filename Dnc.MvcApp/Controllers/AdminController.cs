using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dnc.MvcApp.Filters;
using Dnc.DataAccessRepository.Repositories;
using System.IO;

namespace Dnc.MvcApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IEntityRepository _context;

        public AdminController(IEntityRepository context)
        {
            _context = context;
        }
      
        [HttpGet]
        //[AppAuthorityFilter(new string[] { "管理员", "超级管理员" })]
        public IActionResult Index()
        {
            return View();
        }

    }
}
