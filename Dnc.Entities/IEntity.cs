using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities
{
    /// <summary>
    /// 所有业务实体都需要继承实现的接口规约
    /// </summary>
    public interface IEntity
    {
        String ID { get; set; }
    }
    public interface IEntityId
    {
        int Id { get; set; }
    }
}
