using EmlakCore.Business.Abstract;
using EmlakCore.Core.Resources;
using EmlakCore.DataAccsess.Abstract;
using EmlakCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakCore.Business.Concrete
{
    public class KullaniciManager : IKullaniciService
    {
        private IKullanicilarDal _kullanicilarDal;
        public KullaniciManager(IKullanicilarDal kullanicilarDal)
        {
            _kullanicilarDal = kullanicilarDal;
        }

        public void Duzenle(TblKullaniciler tblKullaniciler)
        {
            _kullanicilarDal.Update(tblKullaniciler);
        }

        public TblKullaniciler Get(string KullaniciNo)
        {
            return _kullanicilarDal.Get(x => x.KullaniciNo == KullaniciNo);
        }

        public TblKullaniciler GirisYap(KullaniciGirisYapResource resource)
        {
            return _kullanicilarDal.Get(x => x.Eposta == resource.Eposta && x.Sifre == resource.Sifre);
        }

       
    }
}
