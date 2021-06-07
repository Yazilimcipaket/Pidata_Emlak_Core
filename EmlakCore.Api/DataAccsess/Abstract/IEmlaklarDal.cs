using EmlakCore.DataAccsess.DataAccess;
using EmlakCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EmlakCore.DataAccsess.Abstract
{
    public interface IEmlaklarDal : IEntityRepository<TblEmlaklar>
    {
        
    }
}
