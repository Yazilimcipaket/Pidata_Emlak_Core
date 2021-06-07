using EmlakCore.Business.Abstract;
using EmlakCore.Core.Resources;
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
    public class YetkiliController : ControllerBase
    {
        private IYetkiliService _yetkiliService;
        public YetkiliController(IYetkiliService yetkiliService)
        {
            _yetkiliService = yetkiliService;
        }
        [HttpPost]
        public IActionResult Kayitol(YetkiliKayitResource resource)
        {
            if (_yetkiliService.KayitOl(resource))
                return Ok();
            return BadRequest();
        }
        [Authorize(Roles = "Yetkili")]
        public IActionResult GetYetkiliProfil()
        {
            string KullaniciNo = User.Claims.FirstOrDefault(x => x.Type == "KullaniciNo").Value;
            return Ok(_yetkiliService.GetYetkiliProfil(KullaniciNo));

        }
    }
}
