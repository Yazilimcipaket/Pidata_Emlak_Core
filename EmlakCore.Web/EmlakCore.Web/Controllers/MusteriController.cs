using AutoMapper;
using EmlakCore.Web.Models;
using EmlakCore.Web.Resource;
using EmlakCore.Web.Services;
using EmlakCore.Web.StaticClasses;
using EmlakCore.Web.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmlakCore.Web.Controllers
{
    [Authorize(Roles = "Musteri")]
    public class MusteriController : Controller
    {
        
        private readonly IdecodeTokenService _decodeTokenService;
        private readonly IService _service;
        private readonly IMapper _mapper;
        public MusteriController(IdecodeTokenService decodeTokenService,IService service,IMapper mapper)
        {
            _decodeTokenService = decodeTokenService;
            _service = service;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            AccessToken accessToken = _decodeTokenService.GetAccessToken();
            string musteriNo = _decodeTokenService.MusteriNo(accessToken.Token);
            HttpResponseMessage musteriResponce = _service.Get("Musteri/Get", accessToken);
            if (musteriResponce.IsSuccessStatusCode)
            {
                MusteriResource musteriResource = musteriResponce.Content.ReadAsAsync<MusteriResource>().Result;
                return View(_mapper.Map<MusteriProfilViewModel>(musteriResource));
            }
            return View();
        }
        public IActionResult IlanVer()
        {
            EmlakIlanVerModel model = new EmlakIlanVerModel();
            HttpResponseMessage  responce= _service.Get("Emlak/GetAllEmlakTur");
            if (responce.IsSuccessStatusCode)
            {
               List<EmlakTurleriResource> resource = responce.Content.ReadAsAsync<List<EmlakTurleriResource>>().Result;
                model.EmlakTurleri = _mapper.Map<List<EmlakTurleriModel>>(resource);
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public IActionResult IlanVer(EmlakIlanVerModel model)
        {
            if (ModelState.IsValid)
            {
              
                HttpResponseMessage emlakEkleReponce = _service.Post("Musteri/EmlakEkle", model,_decodeTokenService.GetAccessToken());
                if (emlakEkleReponce.IsSuccessStatusCode)
                {
                    EmlakResource emlak = emlakEkleReponce.Content.ReadAsAsync<EmlakResource>().Result;
                    return RedirectToAction("IlanResimEkle", emlak);
                }
            }
            return View();
        }
        public IActionResult IlanResimEkle(EmlakResource emlak)
        {
            EmlakResimEkleID.EmlakID = emlak.EmlakID;
            return View();
        }
        [HttpPost]
        public IActionResult IlanResimEkle(IFormFile file)
        {
            HttpResponseMessage mesaj = _service.Post("Musteri/EmlakResimEkle", file, EmlakResimEkleID.EmlakID, _decodeTokenService.GetAccessToken());
            if (mesaj.IsSuccessStatusCode)
                return Ok();
            return BadRequest();
        }
        [AllowAnonymous]
        public IActionResult EmlakSonucCikti()
        {
            return View(EmlakSonucCiktiAl.Emlaklar);
        }
        public IActionResult Ilanlarım()
        {
            AccessToken accessToken = _decodeTokenService.GetAccessToken();
            HttpResponseMessage emlakResponce = _service.Get("Musteri/Ilanlarim", accessToken);
            if (emlakResponce.IsSuccessStatusCode)
            {
                List<GetAllEmlakResource> emlakResource = emlakResponce.Content.ReadAsAsync<List<GetAllEmlakResource>>().Result;
                return View(_mapper.Map<List<EmlaklarModel>>(emlakResource));
            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult Kayitol()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Kayitol(KayitOlModel model)
        {
            if (!ModelState.IsValid)
            {
                string hataMesajlari = "";
                foreach (var item in ModelState.Root.Children)
                {
                    if (item.ValidationState == ModelValidationState.Invalid)
                    {
                        foreach (var hata in item.Errors)
                        {
                            hataMesajlari += hata.ErrorMessage + ",";
                        }
                    }
                }
                TempData.Add("Mesaj", hataMesajlari);
                return View();
            }
            HttpResponseMessage kayitOlMessage = _service.Post("Kullanici/KayitOl", model);
            if (kayitOlMessage.IsSuccessStatusCode)
            {
                TempData.Add("Mesaj", "Kayıt Başarılı");
                return RedirectToAction("GirisYap");
            }
            TempData.Add("Mesaj", "Hata!");
            return View();
        }
    }
}
