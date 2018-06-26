using Dnc.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ApiModel.Article
{
    public class ReadyplayList
    {
        public string Time { get; set; }
        public List<ReadyplayModel> GetReadyplayList { get; set; }
        public List<ReadyplayModel> ReadList { get; set; }
    }
}
