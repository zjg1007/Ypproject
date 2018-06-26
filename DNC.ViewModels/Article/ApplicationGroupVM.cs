using Dnc.Entities.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ViewModels.Article
{
    public class ApplicationGroupVM
    {
        public String ID { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public ApplicationGroupVM() { }
        public ApplicationGroupVM(ApplicationGroup bo) {
            this.ID = bo.ID;
            this.Name = bo.Name;
            this.Decription = bo.Decription;
        }
        public void MapBo(ApplicationGroupVM bo)
        {
            bo.ID = this.ID;
            bo.Name = this.Name;
            bo.Decription = this.Decription;
        }
    }
}
