using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EmlakCore.Business.Abstract;
using EmlakCore.Business.Helpers;
using EmlakCore.Business.Tools;
using EmlakCore.Core.Dtos;
using EmlakCore.Core.Resources;
using EmlakCore.DataAccsess.Abstract;
using EmlakCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakCore.Business.Concrete
{
    public class MusteriManager : IMusteriService
    {
        private readonly IMusterilerDal _musterilerDal;
        private readonly IKullanicilarDal _kullanicilarDal;
        private readonly IMapper _mapper;
        private readonly IAdreslerDal _adreslerDal;
        private readonly IEmlaklarDal _emlaklarDal;
        private readonly IKiralikEmlaklarDal _kiralikEmlaklarDal;
        private readonly ISatilikEmlaklarDal _satilikEmlaklarDal;
        IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        private readonly IResimlerDal _resimlerDal;
        private readonly IEmlakResimleriDal _emlakResimleriDal;
        private readonly IEmlakTurleriDal _emlakTurleriDal;
        public MusteriManager(IMusterilerDal musterilerDal, IKullanicilarDal kullanicilarDal, IMapper mapper,
            IAdreslerDal adreslerDal, IEmlaklarDal emlaklarDal, IKiralikEmlaklarDal kiralikEmlaklarDal,
            ISatilikEmlaklarDal satilikEmlaklarDal, IOptions<CloudinarySettings> cloudinaryConfig,
            IResimlerDal resimlerDal, IEmlakResimleriDal emlakResimleriDal,IEmlakTurleriDal emlakTurleriDal)
        {
            _musterilerDal = musterilerDal;
            _kullanicilarDal = kullanicilarDal;
            _mapper = mapper;
            _adreslerDal = adreslerDal;
            _emlaklarDal = emlaklarDal;
            _kiralikEmlaklarDal = kiralikEmlaklarDal;
            _satilikEmlaklarDal = satilikEmlaklarDal;
            _cloudinaryConfig = cloudinaryConfig;
            Account account = new Account(_cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey, _cloudinaryConfig.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
            _resimlerDal = resimlerDal;
            _emlakResimleriDal = emlakResimleriDal;
            _emlakTurleriDal = emlakTurleriDal;
        }

        public TblEmlaklar EmlakEkle(MusteriEmlakEkleResource resource)
        {
            TblAdresler adres = _mapper.Map<TblAdresler>(resource);
            _adreslerDal.Add(adres);
            TblKullaniciler kullanici = _kullanicilarDal.Get(x => x.KullaniciNo == resource.KullaniciNo);
            TblEmlaklar emlak = _mapper.Map<TblEmlaklar>(resource);
            emlak.Adres = adres.AdresID;
            emlak.Sahibi = kullanici.KullaniciID;
            _emlaklarDal.Add(emlak);
            if (resource.Tip == "Kiralık")
            {
                _kiralikEmlaklarDal.Add(new TblKiralikEmlaklar
                {
                    KiralikEmlakID = emlak.EmlakID,
                    Ucret = resource.Fiyat
                });
            }
            else if (resource.Tip == "Satılık")
            {
                _satilikEmlaklarDal.Add(new TblSatilikEmlaklar
                {
                    SatilikEmlakID = emlak.EmlakID,
                    Ucret = resource.Fiyat
                });
            }
            else
                _emlaklarDal.Delete(emlak);
            return emlak;
        }

        public TblMusteriler Get(string KullaniciNo)
        {
            TblKullaniciler kullanici = _kullanicilarDal.Get(x => x.KullaniciNo == KullaniciNo);
            return _musterilerDal.Get(x => x.MusteriID == kullanici.KullaniciID);
        }

        public TblMusteriler KayitOl(MusteriKayitOlResource resource)
        {
            TblKullaniciler kullanici = _mapper.Map<TblKullaniciler>(resource);
            kullanici.KullaniciNo = Araclar.MusteriNoUret(7);
            _kullanicilarDal.Add(kullanici);
            TblMusteriler musteriler = _mapper.Map<TblMusteriler>(resource);
            musteriler.MusteriID = kullanici.KullaniciID;
            _musterilerDal.Add(musteriler);
            return musteriler;
        }
        public bool EmlakResimEkle(IFormFile file,int EmlakID)
        {
            var uploadResult = new ImageUploadResult();

            using (var stream = file.OpenReadStream())
            {
                var a = new ImageUploadParams
                {
                    File = new FileDescription(file.Name, stream)
                };
                uploadResult = _cloudinary.Upload(a);
            }
            string url = uploadResult.Url.ToString();
            string publicID = uploadResult.PublicId;
            TblResimler tblResimler = new TblResimler
            {
                PublicID = publicID,
                ResimYol = url
            };
            _resimlerDal.Add(tblResimler);
            _emlakResimleriDal.Add(
                new TblEmlakResimleri
                {
                    EmlakID = EmlakID,
                    ResimID = tblResimler.ResimID
                });
            return true;
        }

        public List<EmlaklarDto> Emlaklarım(string KullaniciNo)
        {
            List<EmlaklarDto> dtos = new List<EmlaklarDto>();
            TblKullaniciler kullanici = _kullanicilarDal.Get(x => x.KullaniciNo == KullaniciNo);
            List<TblEmlaklar> emlaklar = _emlaklarDal.GetList(x => x.Sahibi == kullanici.KullaniciID);
            foreach (TblEmlaklar emlak in emlaklar)
            {
                TblAdresler adres = _adreslerDal.Get(x => x.AdresID == emlak.Adres);
                TblEmlakTurleri emlakTurleri = _emlakTurleriDal.Get(x => x.EmlakTurID == emlak.EmlakTuru);
                TblMusteriler musteri = _musterilerDal.Get(x => x.MusteriID == emlak.Sahibi);
                decimal fiyat = 0;
                bool kiralik = true;
                TblKiralikEmlaklar kiralikEmlak = _kiralikEmlaklarDal.Get(x => x.KiralikEmlakID == emlak.EmlakID);
                TblSatilikEmlaklar satilikEmlak = _satilikEmlaklarDal.Get(x => x.SatilikEmlakID == emlak.EmlakID);
                if (kiralikEmlak != null)
                    fiyat = kiralikEmlak.Ucret;
                else
                {
                    fiyat = satilikEmlak.Ucret;
                    kiralik = false;
                }
                EmlaklarDto dto = new EmlaklarDto
                {
                    IsinmaTuru = emlak.IsinmaTuru,
                    Kat = emlak.Kat,
                    OdaSayisi = emlak.OdaSayisi,
                    MetreKare = emlak.Metrekare,
                    AdresDetay = adres.AdresDetay,
                    AdresIl = adres.Il,
                    AdresIlce = adres.Ilce,
                    EmlakTuru = emlakTurleri.EmlakTurAdi,
                    IlanSahibiAdi = musteri.Ad,
                    IlanSahibiSoyadi = musteri.Soyad,
                    IlanSahibiTelefonu = musteri.CepTelefonu,
                    Kiralik = kiralik,
                    Fiyat = fiyat
                };
                foreach (TblEmlakResimleri emlakResim in _emlakResimleriDal.GetList(x => x.EmlakID == emlak.EmlakID))
                {
                    dto.Resimler.Add(_resimlerDal.Get(x => x.ResimID == emlakResim.ResimID).ResimYol);
                }
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
