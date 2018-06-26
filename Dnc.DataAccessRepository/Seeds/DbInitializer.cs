using Dnc.DataAccessRepository.Context;
using Dnc.Entities.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dnc.Entities.Business;

namespace Dnc.DataAccessRepository.Seeds
{
    public static class DbInitializer
    {
        public static void Initialze(EntityDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.ApplicationGroup.Any())
                return;

            var appGroups = new List<ApplicationGroup> {
                new ApplicationGroup {Name="超级管理员", Decription="最高权限" },
                new ApplicationGroup {Name="普通用户", Decription="仅仅注册资料的，具有常规的公开业务模块使用权限的用户组。" },
                new ApplicationGroup {Name="黑名单", Decription="违反系统规则，此账号处于冻结状态，不能登陆！" },
                new ApplicationGroup {Name="管理员", Decription="具备使用系统全部权限的用户组" },
                 new ApplicationGroup {Name="会员", Decription="app经过付费获取的会员,购买产品更优惠" }
            };
            foreach (var item in appGroups)
            {
                context.ApplicationGroup.Add(item);
            }

            var appMovieTyoe = new List<MType> {
                new MType { Name="动作"},
                new MType { Name="冒险"},
                new MType { Name="喜剧"},
                new MType { Name="爱情"},
                new MType { Name="战争"},
                new MType { Name="恐怖"},
                new MType { Name="犯罪"},
                new MType { Name="悬疑"},
                new MType { Name="惊悚"},
                new MType { Name="武侠"},
                new MType { Name="科幻"},
                new MType { Name="音乐"},
                new MType { Name="动画"},
                new MType { Name="奇幻"},
                new MType { Name="家庭"},
                new MType { Name="剧情"},
                new MType { Name="伦理"},
                new MType { Name="记录"},
                new MType { Name="历史"},
                new MType { Name="青春"},
                new MType { Name="励志"}
            };
            foreach (var item in appMovieTyoe)
            {
                context.MType.Add(item);
            }
            context.SaveChanges();
            var grp = context.ApplicationGroup.First(m => m.Name == "超级管理员");
            var usb = new ApplicationUser { Headimg = "sdsd", Phonr = "18877936354",Password="123", Name = "超级管理员", Evaluation = "超级管理员就是这么牛逼，你不管。", ApplicationGroupId = grp.ID, Group = grp };
            context.ApplicationUser.Add(usb);
            context.SaveChanges();
            //var appUsers = new List<ApplicationUser>
            //{

            //};
            //foreach (var item in appUsers)
            //{
            //    context.ApplicationUser.Add(item);
            //}
            //context.SaveChanges();

            //var articleTypes = new List<ArticleType>
            //{
            //    new ArticleType {Name="行业要闻", Decription="", SortCode="" },
            //    new ArticleType {Name="意见领袖", Decription="", SortCode="" },
            //    new ArticleType {Name="观点专题", Decription="", SortCode="" },
            //    new ArticleType {Name="海外来风", Decription="", SortCode="" },
            //    new ArticleType {Name="科技前沿", Decription="", SortCode="" },
            //};
            //foreach (var item in articleTypes)
            //    context.ArticleTypes.Add(item);
            //context.SaveChanges();

            //var articles = new List<NewsArticle>
            //{
            //    new NewsArticle {Name="关于行业要闻", SubName="小标题部分，用于呈现需要显示的东西", ArticleType=articleTypes[0], TitleImage="../../images/newsDemo.jpg", Content="据《福布斯》报道，受营收不断增长、Facebook Live日益受欢迎以及最近推出的订餐服务等众多利好消息的提振，Facebook股价周五上涨1.5%，一周内股价累计上涨3%。Facebook即将于11月2日发布第三季财报，而该公司股价也再创历史新高。显然，最大的受益者还是Facebook联合创始人及最大个人股东马克-扎克伯格。仅仅在过去的这一周里，扎克伯格的净资产就增长了16亿美元，达到566亿美元。据《福布斯》全球富豪榜实时排名，扎克伯格目前全球排名第五，领先于曾经的世界首富、墨西哥电信大亨卡洛斯-斯利姆(Carlos Slim)。虽然Facebook股价年初短暂低迷，但自今年1月1日以来已累计上涨25%。也正因如此，扎克伯格在2016年福布斯“美国400富豪榜”上跃居第四，去年他还排在第七名。如今，扎克伯格的排名仅次于微软创始人比尔-盖茨、亚马逊创始人杰夫-贝索斯以及伯克希尔哈撒韦公司创始人沃伦-巴菲特。" },
            //    new NewsArticle {Name="关于意见领袖", SubName="小标题部分，用于呈现需要显示的东西", ArticleType=articleTypes[1], TitleImage="../../images/newsDemo.jpg", Content="据《福布斯》报道，受营收不断增长、Facebook Live日益受欢迎以及最近推出的订餐服务等众多利好消息的提振，Facebook股价周五上涨1.5%，一周内股价累计上涨3%。Facebook即将于11月2日发布第三季财报，而该公司股价也再创历史新高。显然，最大的受益者还是Facebook联合创始人及最大个人股东马克-扎克伯格。仅仅在过去的这一周里，扎克伯格的净资产就增长了16亿美元，达到566亿美元。据《福布斯》全球富豪榜实时排名，扎克伯格目前全球排名第五，领先于曾经的世界首富、墨西哥电信大亨卡洛斯-斯利姆(Carlos Slim)。虽然Facebook股价年初短暂低迷，但自今年1月1日以来已累计上涨25%。也正因如此，扎克伯格在2016年福布斯“美国400富豪榜”上跃居第四，去年他还排在第七名。如今，扎克伯格的排名仅次于微软创始人比尔-盖茨、亚马逊创始人杰夫-贝索斯以及伯克希尔哈撒韦公司创始人沃伦-巴菲特。" },
            //    new NewsArticle {Name="关于观点专题", SubName="小标题部分，用于呈现需要显示的东西", ArticleType=articleTypes[2], TitleImage="../../images/newsDemo.jpg", Content="据《福布斯》报道，受营收不断增长、Facebook Live日益受欢迎以及最近推出的订餐服务等众多利好消息的提振，Facebook股价周五上涨1.5%，一周内股价累计上涨3%。Facebook即将于11月2日发布第三季财报，而该公司股价也再创历史新高。显然，最大的受益者还是Facebook联合创始人及最大个人股东马克-扎克伯格。仅仅在过去的这一周里，扎克伯格的净资产就增长了16亿美元，达到566亿美元。据《福布斯》全球富豪榜实时排名，扎克伯格目前全球排名第五，领先于曾经的世界首富、墨西哥电信大亨卡洛斯-斯利姆(Carlos Slim)。虽然Facebook股价年初短暂低迷，但自今年1月1日以来已累计上涨25%。也正因如此，扎克伯格在2016年福布斯“美国400富豪榜”上跃居第四，去年他还排在第七名。如今，扎克伯格的排名仅次于微软创始人比尔-盖茨、亚马逊创始人杰夫-贝索斯以及伯克希尔哈撒韦公司创始人沃伦-巴菲特。" },
            //    new NewsArticle {Name="关于海外来风", SubName="小标题部分，用于呈现需要显示的东西", ArticleType=articleTypes[3], TitleImage="../../images/newsDemo.jpg", Content="据《福布斯》报道，受营收不断增长、Facebook Live日益受欢迎以及最近推出的订餐服务等众多利好消息的提振，Facebook股价周五上涨1.5%，一周内股价累计上涨3%。Facebook即将于11月2日发布第三季财报，而该公司股价也再创历史新高。显然，最大的受益者还是Facebook联合创始人及最大个人股东马克-扎克伯格。仅仅在过去的这一周里，扎克伯格的净资产就增长了16亿美元，达到566亿美元。据《福布斯》全球富豪榜实时排名，扎克伯格目前全球排名第五，领先于曾经的世界首富、墨西哥电信大亨卡洛斯-斯利姆(Carlos Slim)。虽然Facebook股价年初短暂低迷，但自今年1月1日以来已累计上涨25%。也正因如此，扎克伯格在2016年福布斯“美国400富豪榜”上跃居第四，去年他还排在第七名。如今，扎克伯格的排名仅次于微软创始人比尔-盖茨、亚马逊创始人杰夫-贝索斯以及伯克希尔哈撒韦公司创始人沃伦-巴菲特。" },
            //    new NewsArticle {Name="关于科技前沿", SubName="小标题部分，用于呈现需要显示的东西", ArticleType=articleTypes[4], TitleImage="../../images/newsDemo.jpg", Content="据《福布斯》报道，受营收不断增长、Facebook Live日益受欢迎以及最近推出的订餐服务等众多利好消息的提振，Facebook股价周五上涨1.5%，一周内股价累计上涨3%。Facebook即将于11月2日发布第三季财报，而该公司股价也再创历史新高。显然，最大的受益者还是Facebook联合创始人及最大个人股东马克-扎克伯格。仅仅在过去的这一周里，扎克伯格的净资产就增长了16亿美元，达到566亿美元。据《福布斯》全球富豪榜实时排名，扎克伯格目前全球排名第五，领先于曾经的世界首富、墨西哥电信大亨卡洛斯-斯利姆(Carlos Slim)。虽然Facebook股价年初短暂低迷，但自今年1月1日以来已累计上涨25%。也正因如此，扎克伯格在2016年福布斯“美国400富豪榜”上跃居第四，去年他还排在第七名。如今，扎克伯格的排名仅次于微软创始人比尔-盖茨、亚马逊创始人杰夫-贝索斯以及伯克希尔哈撒韦公司创始人沃伦-巴菲特。" },
            //};
            //foreach (var item in articles)
            //    context.Articles.Add(item);
            //context.SaveChanges();

        }
    }
}
