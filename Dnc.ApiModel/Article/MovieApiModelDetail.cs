using Dnc.Entities.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ApiModel.Article
{
    public class MovieApiModelDetail
    {
        public List<Actor> Actor { get; set; }//演员
        public MovieApiModel MovieApiModel { get; set; }//电影详细信息
    }
}
