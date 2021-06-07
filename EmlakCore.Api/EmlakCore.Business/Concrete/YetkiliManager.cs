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
            Kullaniciler kullanici = _kullanicilarDal.Get(x => x.KullaniciNo == kullaniciNo);
            Yetkililer yetkili = _yetkililerDal.Get(x => x.YetkiliID == kullanici.KullaniciID);
            Isyeri isyeri = _ısyeriDal.Get(x => x.Yetkili == yetkili.YetkiliID);
            Adres adres = _adreslerDal.Get(x => x.AdresID == isyeri.AdresID);
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
            Kullaniciler kullanici = _mapper.Map<Kullaniciler>(resource);
            kullanici.KullaniciNo = Araclar.MusteriNoUret(3);
            _kullanicilarDal.Add(kullanici);
            Isyeri Isyeri = _mapper.Map<Isyeri>(resource);
            Adres adres = _mapper.Map<Adres>(resource);
            _adreslerDal.Add(adres);
            Isyeri.AdresID = adres.AdresID;
            Isyeri.Yetkili = kullanici.KullaniciID;
            Yetkililer yetkili = _mapper.Map<Yetkililer>(resource);
            yetkili.YetkiliID = kullanici.KullaniciID;
            _yetkililerDal.Add(yetkili);
            _ısyeriDal.Add(Isyeri);
            return true;
        }
    }
}
