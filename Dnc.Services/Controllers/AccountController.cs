using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IEntityRepository _DbService;

        public AccountController(IEntityRepository service)
        {
            this._DbService = service;
        }
        /// <summary>
        /// 用户注册（默认权限普通会员）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Register"), HttpPost]
        public LogonUserStatus Register(string username,string password)
        {
            //var model = JsonConvert.DeserializeObject<ApplicationUser>(logonQueryJson);
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "注册失败。"
            };
            ApplicationUser user = null;
            user = _DbService.GetSingleBy<ApplicationUser>(x => x.Phonr == username);

            if (user != null)
            {
                // 处理重复用户名问题
                logonStatus.IsLogon = false;
                logonStatus.Message = "此账号已被注册.";
            }
            else
            {
                user = new ApplicationUser();
                var group = _DbService.GetSingleBy<ApplicationGroup>(x => x.Name == "普通用户");
                user.Group = group;
                user.ApplicationGroupId = group.ID;
                user.Phonr = username;
                user.Password = password;
                user.Name = "";
                user.Evaluation = "";
                user.Headimg = "1";
                user.Isvip = false;
                user.Time = DateTime.Now;
                _DbService.AddAndSave<ApplicationUser>(user);
                var and = _DbService.GetSingleBy<ApplicationUser>(x => x.Phonr == username && x.Password==password);
                HttpContext.Session.SetString("username",username);
                logonStatus.Userid = and.ID;
                logonStatus.IsLogon = true;
                logonStatus.Message = "注册成功";
            }
            return logonStatus;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="username">用户名</param>
        /// <param name="password">原密码</param>
        /// <param name="newpassword">新密码</param>
        /// <returns></returns>
        [HttpPost]
        public LogonUserStatus PostUpdatePassword(dynamic model)
        {
            //string userName = Convert.ToString(model.username);
            string userName = HttpContext.Session.GetString("username");
            string passWord = Convert.ToString(model.password);
            string newPassword = Convert.ToString(model.newpassword);
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "请检查您的网络是否有问题."
            };
            var data= _DbService.GetSingleBy<ApplicationUser>(m => m.Phonr == userName && m.Password == passWord);
            if (data != null)
            {
                data.Password = newPassword;
                _DbService.EditAndSave<ApplicationUser>(data);
                logonStatus.IsLogon = true;
                logonStatus.Message = "修改成功";
            }
            else
            {
                logonStatus.IsLogon = false;
                logonStatus.Message = "原密码不正确,请重新输入";
            }
            return logonStatus;
        }
        /// <summary>
        /// 查看个人信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [Route("PostSelectInfo"),HttpPost]
        public ApplicationUser PostSelectInfo(string username)
        {
            var data= _DbService.GetSingleBy<ApplicationUser>(m => m.Phonr == username);
            return data;
        }
        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("subUserInfo"), HttpPost]
        public LogonUserStatus subUserInfo(ApplicationUser model)
        {
            var LogonUserStatus = new LogonUserStatus()
            {
                IsLogon = false,
                Message = "保存失败"
            };
            try
            {
                var data = _DbService.GetSingleBy<ApplicationUser>(m => m.Phonr == model.Phonr);
                data.Name = model.Name;
                data.Evaluation = model.Evaluation;
                data.Headimg = model.Headimg;
                _DbService.EditAndSave<ApplicationUser>(data);
                LogonUserStatus.IsLogon = true;
                LogonUserStatus.Message = "保存成功";
            }
            catch (Exception)
            {
                LogonUserStatus.IsLogon = false;
            }
            
            return LogonUserStatus;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [Route("updatePassword"), HttpPost]
        public LogonUserStatus updatePassword(string username,string password,string newpassword)
        {
            var LogonUserStatus = new LogonUserStatus()
            {
                IsLogon = false,
                Message = "修改失败"
            };
            try
            {
                var data = _DbService.GetSingleBy<ApplicationUser>(m => m.Phonr == username);
                if (data.Password == password)
                {
                    data.Password = newpassword;
                    _DbService.EditAndSave<ApplicationUser>(data);
                    LogonUserStatus.IsLogon = true;
                    LogonUserStatus.Message = "修改成功";
                }
                else
                {
                    LogonUserStatus.IsLogon = true;
                    LogonUserStatus.Message = "原密码不正确,请重新输入.";
                }
              
            }
            catch (Exception)
            {
                LogonUserStatus.IsLogon = false;
            }

            return LogonUserStatus;
        }
    }
}
