using EmlakCore.Core.Dtos;
using EmlakCore.Core.Resources;
using EmlakCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakCore.Business.Abstract
{
   public interface IEmlakService
    {
       List<EmlakTurleri> GetAllEmlakTur();
        List<EmlaklarDto> GetAllEmlak();
        List<EmlaklarDto> EmlakAra(EmlakAraResource resource);
    }
}
