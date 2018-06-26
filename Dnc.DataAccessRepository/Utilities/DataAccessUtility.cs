using Dnc.DataAccessRepository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.Utilities
{
    public static class DataAccessUtility
    {
        private static EntityDbContext _Context;

        static DataAccessUtility()
        {
        }

        public static void InitialDBContext(EntityDbContext context)
        {
            _Context = context;
        }

        public static string GetGroupNameByUserID(String id)
        {
            var result = "";
            var user = _Context.ApplicationUser.FirstOrDefault(x => x.ID == id);
            if (user != null)
            {
                var group = _Context.ApplicationGroup.FirstOrDefault(x => x.ID == user.ApplicationGroupId);
                if (group != null)
                {
                    result = group.Name;
                }
            }
            return result;
        }
    }
}
