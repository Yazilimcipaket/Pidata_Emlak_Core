using AutoMapper;
using EmlakCore.Web.Models;
using EmlakCore.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Web.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MusteriResource, MusteriProfilViewModel>();
            CreateMap<EmlakTurleriResource, EmlakTurleriModel>();
            CreateMap<GetAllEmlakResource, EmlaklarModel>();
            CreateMap<YetkiliGetProfilResource, YetkiliProfilViewModel>();
        }
    }
}
