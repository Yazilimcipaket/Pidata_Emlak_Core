using EmlakCore.Core.Dtos;
using EmlakCore.Core.Resources;
using EmlakCore.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakCore.Business.Abstract
{
    public interface IMusteriService
    {
        public Musteriler KayitOl(MusteriKayitOlResource resource);
        public Musteriler Get(string KullaniciNo);
        public Emlak EmlakEkle(MusteriEmlakEkleResource resource);
        public bool EmlakResimEkle(IFormFile file, int EmlakID);
        public List<EmlaklarDto> Emlaklarım(string KullaniciNo);
        
    }
}
