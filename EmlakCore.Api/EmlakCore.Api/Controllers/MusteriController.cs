using EmlakCore.Business.Abstract;
using EmlakCore.Core.Dtos;
using EmlakCore.Core.Resources;
using EmlakCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Musteri")]
    public class MusteriController : ControllerBase
    {
        private readonly IMusteriService _musteriService;
        private readonly IKullaniciService _kullaniciService;
        public MusteriController(IMusteriService musteriService, IKullaniciService kullaniciService)
        {
            _musteriService = musteriService;
            _kullaniciService = kullaniciService;
        }
        public IActionResult Get()
        {
            string KullaniciNo = User.Claims.FirstOrDefault(x => x.Type == "KullaniciNo").Value;
            TblMusteriler musteri = _musteriService.Get(KullaniciNo);
            TblKullaniciler kullanici = _kullaniciService.Get(KullaniciNo);
            if (musteri != null & kullanici != null)
            {
                return Ok(new MusteriDto
                {
                    Ad = musteri.Ad,
                    Soyad = musteri.Soyad,
                    Eposta = kullanici.Eposta,
                    CepTelefonu=musteri.CepTelefonu
                });
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult EmlakEkle(MusteriEmlakEkleResource resource)
        {
            resource.KullaniciNo = User.Claims.FirstOrDefault(x => x.Type == "KullaniciNo").Value;
            TblEmlaklar emlak = _musteriService.EmlakEkle(resource);
            if (emlak != null)
            {
                return Ok(emlak);
            }
            return BadRequest();
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult EmlakResimEkle([FromForm]EmlakResimEkleResource files)
        {
            _musteriService.EmlakResimEkle(files.Resim ,files.EmlakID);
            return Ok();
        }
        public IActionResult Ilanlarim()
        {
            string KullaniciNo = User.Claims.FirstOrDefault(x => x.Type == "KullaniciNo").Value;
            return Ok(_musteriService.Emlaklarım(KullaniciNo));
        }
    }
}
