using EmlakCore.Core.Dtos;
using EmlakCore.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakCore.Business.Abstract
{
    public interface IYetkiliService
    {
        bool KayitOl(YetkiliKayitResource resource);
        YetkiliProfilDto GetYetkiliProfil(string kullaniciNo);
    }
}
