using AutoMapper;
using EmlakCore.Api.Security.Token;
using EmlakCore.Business.Abstract;
using EmlakCore.Core.Dtos;
using EmlakCore.Core.Resources;
using EmlakCore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly IMusteriService _musteriService;
        private readonly IKullaniciService _kullaniciService;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMapper _mapper;
         
        public KullaniciController(IMusteriService musteriService,IKullaniciService kullaniciService,ITokenHandler tokenHandler,IMapper mapper)
        {
            _musteriService = musteriService;
            _kullaniciService = kullaniciService;
            _tokenHandler = tokenHandler;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult GirisYap(KullaniciGirisYapResource resource)
        {
            TblKullaniciler kullanici = _kullaniciService.GirisYap(resource);
            if (kullanici == null)
                return BadRequest();
            AccessToken accessToken = _tokenHandler.CreateAccessToken(kullanici);
            kullanici.RefreshTokenEndDate = accessToken.Expiration;
            kullanici.RefreshToken = accessToken.RefreshToken;
            _kullaniciService.Duzenle(kullanici);
            AccessTokenDto dto = _mapper.Map<AccessTokenDto>(accessToken);
            return Ok(dto);
        }
        public IActionResult Default()
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult Kayitol(MusteriKayitOlResource resource)
        {
            TblMusteriler musteri = _musteriService.KayitOl(resource);
            if (musteri == null)
                return BadRequest();
            return Ok();
        }
     
    }
}
