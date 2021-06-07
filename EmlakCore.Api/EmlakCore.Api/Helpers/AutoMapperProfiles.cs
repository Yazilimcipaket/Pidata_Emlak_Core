using AutoMapper;
using EmlakCore.Api.Security.Token;
using EmlakCore.Core.Dtos;
using EmlakCore.Core.Resources;
using EmlakCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Api.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MusteriKayitOlResource, TblKullaniciler>();
            CreateMap<MusteriKayitOlResource, TblMusteriler>();
            CreateMap<AccessToken, AccessTokenDto>();
            CreateMap<MusteriEmlakEkleResource, TblAdresler>()
               .ForMember(x => x.Il, y => y.MapFrom(z => z.AdresIl))
               .ForMember(x => x.Ilce, y => y.MapFrom(z => z.AdresIlce));
            CreateMap<MusteriEmlakEkleResource, TblEmlaklar>();
            CreateMap<YetkiliKayitResource, TblKullaniciler>();
            CreateMap<YetkiliKayitResource, TblIsyeri>();
            CreateMap<YetkiliKayitResource, TblAdresler>()
                .ForMember(x => x.Il, y => y.MapFrom(z => z.AdresIl))
                .ForMember(x => x.Ilce, y => y.MapFrom(z => z.AdresIlce))
                .ForMember(x => x.AdresDetay, y => y.MapFrom(z => z.AdresDetay));
            CreateMap<YetkiliKayitResource, TblYetkililer>();

        }
    }
}
