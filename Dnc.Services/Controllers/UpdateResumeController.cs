using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.Entities.Application;
using Dnc.ApiModel.Article;
using Dnc.Entities.Business;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Dnc.DataAccessRepository.Repositories;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class UpdateResumeController : Controller
    {
        private readonly IEntityRepository _DbService;

        public UpdateResumeController(IEntityRepository service)
        {
            this._DbService = service;
        }

        /// <summary>
        /// 根据用户的 id 获取简历的详细数据
        /// 访问方式：http://site:port/Article/18d64e28-7c32-4aec-a66c-4c0590fd8ce4
        /// </summary>
        /// <param name="id">用户的 ID</param>
        /// <returns></returns>
        [HttpGet]
        public UserApiModel Get()
        {
            //var bo = _DbService.GetSingle<ApplicationUser>(id, x => x.Group);
            String userid;
            try
            {
                userid = HttpContext.Session.GetString("userID");
            }
            catch
            {
                userid = "e02f5176-4d7c-485c-ad09-31d2704e60ba";
            }
            var and = _DbService.GetSingleBy<ApplicationUser>(x => x.ID == userid);
            var boAM = new UserApiModel(and);
            return boAM;
        }

        // GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
