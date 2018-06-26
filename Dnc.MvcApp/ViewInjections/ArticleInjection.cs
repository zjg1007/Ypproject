using Dnc.DataAccessRepository.Context;
using Dnc.Entities.Business;
using Dnc.Entities.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.ViewInjections
{
    /// <summary>
    /// 提取文章数据的一个简单的页面注入器
    /// </summary>
    public class ArticleInjection
    {
        private readonly EntityDbContext _DbContext;  // EF 数据配置映射上下文

        public ArticleInjection(EntityDbContext context)
        {
            _DbContext = context;
        }
        private DateTime seleDate(string name)
        {
            DateTime dt = DateTime.ParseExact(name, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
            return dt;
        }
        public List<Movie> GetArticleCollection() => _DbContext.Movie.Where(m=>seleDate(m.LaunchName)<seleDate(DateTime.Now.ToString("yyyy-MM-dd"))).OrderByDescending(m=> seleDate(m.LaunchName)).ToList();//正在热映
        public List<Movie> GetMovieCollection() => _DbContext.Movie.Where(m => seleDate(m.LaunchName) >seleDate(DateTime.Now.ToString("yyyy-MM-dd"))).OrderBy(m => seleDate(m.LaunchName)).ToList();//即将上映

    }
}