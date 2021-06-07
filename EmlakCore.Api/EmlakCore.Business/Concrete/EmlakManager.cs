using EmlakCore.Business.Abstract;
using EmlakCore.Core.Dtos;
using EmlakCore.Core.Resources;
using EmlakCore.DataAccsess.Abstract;
using EmlakCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmlakCore.Business.Concrete
{
    public class EmlakManager : IEmlakService
    {
        private readonly IEmlakTurleriDal _emlakTurleriDal;
        private readonly IEmlaklarDal _emlaklarDal;
        private readonly IAdreslerDal _adreslerDal;
        private readonly IMusterilerDal _musterilerDal;
        private readonly IEmlakResimleriDal _emlakResimleriDal;
        private readonly IResimlerDal _resimlerDal;
        private readonly IKiralikEmlaklarDal _kiralikEmlaklarDal;
        private readonly ISatilikEmlaklarDal _satilikEmlaklarDal;
        public EmlakManager(IEmlakTurleriDal emlakTurleriDal, IEmlaklarDal emlaklarDal,
            IAdreslerDal adreslerDal, IMusterilerDal musterilerDal,
            IEmlakResimleriDal emlakResimleriDal, IResimlerDal resimlerDal, IKiralikEmlaklarDal kiralikEmlaklarDal, ISatilikEmlaklarDal satilikEmlaklarDal)
        {
            _emlakTurleriDal = emlakTurleriDal;
            _adreslerDal = adreslerDal;
            _musterilerDal = musterilerDal;
            _emlaklarDal = emlaklarDal;
            _emlakResimleriDal = emlakResimleriDal;
            _resimlerDal = resimlerDal;
            _kiralikEmlaklarDal = kiralikEmlaklarDal;
            _satilikEmlaklarDal = satilikEmlaklarDal;
        }

        public List<EmlaklarDto> GetAllEmlak()
        {
            List<Emlak> emlaklar = _emlaklarDal.GetList();
            List<EmlaklarDto> dtos = new List<EmlaklarDto>();
            foreach (Emlak emlak in emlaklar)
            {
                Adres adres = _adreslerDal.Get(x => x.AdresID == emlak.Adres);
                EmlakTurleri emlakTurleri = _emlakTurleriDal.Get(x => x.EmlakTurID == emlak.EmlakTuru);
                Musteriler musteri = _musterilerDal.Get(x => x.MusteriID == emlak.Sahibi);
                decimal fiyat = 0;
                bool kiralik = true;
                KiralikEmlaklar kiralikEmlak = _kiralikEmlaklarDal.Get(x => x.KiralikEmlakID == emlak.EmlakID);
                SatilikEmlaklar satilikEmlak = _satilikEmlaklarDal.Get(x => x.SatilikEmlakID == emlak.EmlakID);
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
                foreach (EmlakResimleri emlakResim in _emlakResimleriDal.GetList(x => x.EmlakID == emlak.EmlakID))
                {
                    dto.Resimler.Add(_resimlerDal.Get(x => x.ResimID == emlakResim.ResimID).ResimYol);
                }
                dtos.Add(dto);
            }
            return dtos;
        }

        public List<EmlakTurleri> GetAllEmlakTur()
        {
            return _emlakTurleriDal.GetList();
        }
        public List<EmlaklarDto> EmlakAra(EmlakAraResource resource)
        {
            List<int> emlaklarID = new List<int>();
            List<EmlaklarDto> dtos = new List<EmlaklarDto>();

            if (resource.Kiralik)
                emlaklarID = _kiralikEmlaklarDal.GetList(
                    x => x.Ucret > resource.AltFiyat & x.Ucret < resource.UstFiyat)
                    .Select(x => x.KiralikEmlakID).ToList();
            else
                emlaklarID = _satilikEmlaklarDal.GetList(
                    x => x.Ucret > resource.AltFiyat & x.Ucret < resource.UstFiyat)
                    .Select(x => x.SatilikEmlakID).ToList();
            List<Adres> adresler = new List<Adres>();
            if (resource.Ilce != null)
                adresler = _adreslerDal.GetList(x => x.Ilce == resource.Ilce);
            else if (resource.Il != null)
                adresler = _adreslerDal.GetList(x => x.Il == resource.Il);

            foreach (int emlakID in emlaklarID)
            {
                Emlak emlak = _emlaklarDal.Get(x => x.EmlakID == emlakID);
                Adres adres = _adreslerDal.Get(x => x.AdresID == emlak.Adres);
                if (adresler.FirstOrDefault(x => x.AdresID == adres.AdresID) == null)
                    continue;
                Musteriler musteri = _musterilerDal.Get(x => x.MusteriID == emlak.Sahibi);
                EmlakTurleri emlakTuru = _emlakTurleriDal.Get(x => x.EmlakTurID == emlak.EmlakTuru);
                int resimID = _emlakResimleriDal.Get(x => x.EmlakID == emlak.EmlakID).ResimID;
                string resimYol = _resimlerDal.Get(x => x.ResimID == resimID).ResimYol;
                decimal fiyat = 00;
                bool kiralik = true;
                KiralikEmlaklar kiralikEmlak = _kiralikEmlaklarDal.Get(x => x.KiralikEmlakID == emlak.EmlakID);
                SatilikEmlaklar satilikEmlak = _satilikEmlaklarDal.Get(x => x.SatilikEmlakID == emlak.EmlakID);
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
                    MetreKare = emlak.Metrekare,
                    OdaSayisi = emlak.OdaSayisi,
                    AdresDetay = adres.AdresDetay,
                    AdresIl = adres.Il,
                    AdresIlce = adres.Ilce,
                    IlanSahibiAdi = musteri.Ad,
                    IlanSahibiSoyadi = musteri.Soyad,
                    IlanSahibiTelefonu = musteri.CepTelefonu,
                    EmlakTuru = emlakTuru.EmlakTurAdi,
                    Resimler = new List<string> { resimYol },
                    Fiyat=fiyat,
                    Kiralik=kiralik

                };
                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
//if (adresler != null)
//{
//    foreach (TblAdresler adres in adresler)
//    {
//        TblEmlaklar emlak = _emlaklarDal.Get(x => x.Adres == adres.AdresID);
//        if (emlaklarID.FirstOrDefault(emlak.EmlakID) != null)
//            TblMusteriler musteri = _musterilerDal.Get(x => x.MusteriID == emlak.Sahibi);
//        TblEmlakTurleri emlakTuru = _emlakTurleriDal.Get(x => x.EmlakTurID == emlak.EmlakTuru);
//        int resimID = _emlakResimleriDal.Get(x => x.EmlakID == emlak.EmlakID).ResimID;
//        string resimYol = _resimlerDal.Get(x => x.ResimID == resimID).ResimYol;
//        EmlaklarDto dto = new EmlaklarDto
//        {
//            IsinmaTuru = emlak.IsinmaTuru,
//            Kat = emlak.Kat,
//            MetreKare = emlak.Metrekare,
//            OdaSayisi = emlak.OdaSayisi,
//            AdresDetay = adres.AdresDetay,
//            AdresIl = adres.Il,
//            AdresIlce = adres.Ilce,
//            IlanSahibiAdi = musteri.Ad,
//            IlanSahibiSoyadi = musteri.Soyad,
//            IlanSahibiTelefonu = musteri.CepTelefonu,
//            EmlakTuru = emlakTuru.EmlakTurAdi,
//            Resimler = new List<string> { resimYol }

//        };
//        dtos.Add(dto);
//    }
//}