using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.Entities.Business;
using Dnc.Services.Utilities;
using Microsoft.AspNetCore.Cors;
using Dnc.Entities.Application;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Dnc.DataAccessRepository.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IEntityRepository _DbService;

        public PersonController(IEntityRepository service)
        {
            this._DbService = service;
        }
        //[HttpGet]
        ////[EnableCors("DncDemo")]
        //public IEnumerable<Recruitment> Get()
        //{
        //    return BusinessDataAccess.Recruitments;
        //}

        //[HttpGet("{id}")]
        ////[EnableCors("DncDemo")]
        //public Recruitment Get(string id)
        //{
        //    var gID = Guid.Parse(id);
        //    var persons = BusinessDataAccess.Recruitments;
        //    var person= persons.FirstOrDefault(x => x.ID == gID);

        //    return person;
        //}
        /// <summary>
        /// 用户登陆（提供参数（username,passdword））
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public LogonUserStatus Post([FromBody]LogonInformation model)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "<span class='mif-warning'></span> 用户名或密码错误。"
            };
            // 将遵循 json 规格定义的对象数据字符串转换为C#对象
            //var logonInfo = JsonConvert.DeserializeObject<LogonInformation>(logonQueryJson);
            var and = _DbService.GetSingleBy<ApplicationUser>(x => x.Phonr == model.username &&x.Password == model.password,x=>x.Group);
             if (and.Group.Name == "黑名单")
            {
                logonStatus.IsLogon = false;
                logonStatus.Message = "此账号已被封停,请与管理员联系";
            }
            else if (and != null)
            {
                HttpContext.Session.SetString("username",model.username);
                logonStatus.IsLogon = true;
                logonStatus.Userid = and.ID;
                logonStatus.Message = "登陆成功。";
            }
            
            else
            {
                logonStatus.IsLogon = false;
                logonStatus.Message = "账号或密码错误,请重新输入.";
            }
            return logonStatus;
        }
        //招聘者登录
        //[HttpPost]
        ////[EnableCors("DncDemo")]
        //public LogonUserStatus EnterpriseLogin(string logonQueryJson)
        //{
        //    var logonStatus = new LogonUserStatus
        //    {
        //        IsLogon = false,
        //        Message = "<span class='mif-warning'></span> 用户名或密码错误。"
        //    };
        //    // 将遵循 json 规格定义的对象数据字符串转换为C#对象
        //    var logonInfo = JsonConvert.DeserializeObject<LogonInformation>(logonQueryJson);
        //    var and = _DbService.GetSingleBy<Enterprise>(x => x.Account == logonInfo.username && x.Password == logonInfo.password);
        //    if (and!=null)
        //    {
        //        logonStatus.IsLogon = true;
        //        logonStatus.Userid = and.ID;
        //        logonStatus.Message = "登陆成功。";
        //    }
        //    else if (and.Certification==false)
        //    {
        //        logonStatus.IsLogon = false;
        //        logonStatus.Userid = and.ID;
        //        logonStatus.Message = "您的用户没有通过认证";
        //    }
        //    else
        //    {
        //        logonStatus.IsLogon = false;
        //        logonStatus.Message = "账号或密码错误,请重新输入.";
        //    }
        //    return logonStatus;
        //}
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
