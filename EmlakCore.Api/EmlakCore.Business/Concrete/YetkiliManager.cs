using AutoMapper;
using EmlakCore.Business.Abstract;
using EmlakCore.Business.Tools;
using EmlakCore.Core.Dtos;
using EmlakCore.Core.Resources;
using EmlakCore.DataAccsess.Abstract;
using EmlakCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakCore.Business.Concrete
{
    public class YetkiliManager : IYetkiliService
    {
        private readonly IYetkililerDal _yetkililerDal;
        private readonly IKullanicilarDal _kullanicilarDal;
        private readonly IIsyeriDal _ısyeriDal;
        private readonly IMapper _mapper;
        private readonly IAdreslerDal _adreslerDal;
        public YetkiliManager(IYetkililerDal yetkililerDal,IKullanicilarDal kullanicilarDal,IIsyeriDal ısyeriDal,IMapper mapper,IAdreslerDal adreslerDal)
        {
            _yetkililerDal = yetkililerDal;
            _kullanicilarDal = kullanicilarDal;
            _ısyeriDal = ısyeriDal;
            _mapper = mapper;
            _adreslerDal = adreslerDal;
        }

        public YetkiliProfilDto GetYetkiliProfil(string kullaniciNo)
        {
            TblKullaniciler kullanici = _kullanicilarDal.Get(x => x.KullaniciNo == kullaniciNo);
            TblYetkililer yetkili = _yetkililerDal.Get(x => x.YetkiliID == kullanici.KullaniciID);
            TblIsyeri isyeri = _ısyeriDal.Get(x => x.Yetkili == yetkili.YetkiliID);
            TblAdresler adres = _adreslerDal.Get(x => x.AdresID == isyeri.AdresID);
            return new YetkiliProfilDto
            {
                IsyeriAdi = isyeri.IsyeriAdi,
                IsyeriAdresDetay = adres.AdresDetay,
                IsyeriAdresIl = adres.Il,
                IsyeriAdresIlce = adres.Ilce,
                IsyeriFax = isyeri.Fax,
                IsyeriTelefonNo = isyeri.TelefonNo,
                YetkiliAd=yetkili.YetkiliAd,
                YetkiliSoyad=yetkili.YetkiliSoyad,
                Eposta=kullanici.Eposta
            };
        }

        public bool KayitOl(YetkiliKayitResource resource)
        {
            TblKullaniciler kullanici = _mapper.Map<TblKullaniciler>(resource);
            kullanici.KullaniciNo = Araclar.MusteriNoUret(3);
            _kullanicilarDal.Add(kullanici);
            TblIsyeri Isyeri = _mapper.Map<TblIsyeri>(resource);
            TblAdresler adres = _mapper.Map<TblAdresler>(resource);
            _adreslerDal.Add(adres);
            Isyeri.AdresID = adres.AdresID;
            Isyeri.Yetkili = kullanici.KullaniciID;
            TblYetkililer yetkili = _mapper.Map<TblYetkililer>(resource);
            yetkili.YetkiliID = kullanici.KullaniciID;
            _yetkililerDal.Add(yetkili);
            _ısyeriDal.Add(Isyeri);
            return true;
        }
    }
}
