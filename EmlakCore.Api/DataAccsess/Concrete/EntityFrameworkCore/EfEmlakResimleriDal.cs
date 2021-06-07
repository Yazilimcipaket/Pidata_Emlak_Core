using EmlakCore.Core.DataAccess.EntityFrameworkCore;
using EmlakCore.DataAccsess.Abstract;
using EmlakCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakCore.DataAccsess.Concrete.EntityFrameworkCore
{
   public class EfEmlakResimleriDal: EfEntityRepositoryBase<EmlakResimleri, EmlakCoreContext>, IEmlakResimleriDal
    {
    }
}
