using EmlakCore.Core.Resources;
using EmlakCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakCore.Business.Abstract
{
   public interface IKullaniciService
    {
        Kullaniciler GirisYap(KullaniciGirisYapResource resource);
     
        void Duzenle(Kullaniciler tblKullaniciler);
        Kullaniciler Get(string KullaniciNo);
    }
}
