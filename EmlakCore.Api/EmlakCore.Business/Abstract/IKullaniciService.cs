using EmlakCore.Core.Resources;
using EmlakCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakCore.Business.Abstract
{
   public interface IKullaniciService
    {
        TblKullaniciler GirisYap(KullaniciGirisYapResource resource);
     
        void Duzenle(TblKullaniciler tblKullaniciler);
        TblKullaniciler Get(string KullaniciNo);
    }
}
