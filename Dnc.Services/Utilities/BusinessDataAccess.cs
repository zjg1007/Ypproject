using Dnc.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dnc.Entities.Application;

namespace Dnc.Services.Utilities
{
    public static class BusinessDataAccess
    {
        public static List<ApplicationUser> Recruitments = new List<ApplicationUser>();

        public static void Set()
        {
            Recruitments.Add(new ApplicationUser() { Name = "李玉华",Phonr="sdsad",Password="123"});
            Recruitments.Add(new ApplicationUser() { Name = "钟博琳", Phonr = "sdsad", Password = "123" });
            Recruitments.Add(new ApplicationUser() { Name = "黄小立", Phonr = "sdsad", Password = "123" });
            Recruitments.Add(new ApplicationUser() { Name = "王卫红", Phonr = "sdsad", Password = "123" });
            Recruitments.Add(new ApplicationUser() { Name = "张富贵", Phonr = "sdsad", Password = "123" });
            Recruitments.Add(new ApplicationUser() { Name = "陈东风", Phonr = "sdsad", Password = "123" });
            Recruitments.Add(new ApplicationUser() { Name = "黄复礼", Phonr = "sdsad", Password = "123" });
        }
    }
}
