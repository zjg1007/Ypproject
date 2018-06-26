using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Business;
using Dnc.ApiModel.Article;
//using Dnc.DataAccess.Context;

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IEntityRepository _Service;

        public ValuesController(IEntityRepository service)
        {
            this._Service = service;

        }
        // GET api/values
        //[HttpGet]
        //public IEnumerable<ApplyApiModel> Get()
        //{
        //    var boCollection = _Service.GetAll<Apply>(x => x.Recruitment).ToList();   // 提取所有的文章类型数据
        //    var boAMCollection = new List<ApplyApiModel>();
        //    foreach (var item in boCollection)
        //    {
        //        boAMCollection.Add(new ApplyApiModel(item));
        //    }
        //    return boAMCollection;
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
