using AutoMapper;
using EmlakCore.Web.Models;
using EmlakCore.Web.Resource;
using EmlakCore.Web.Services;
using EmlakCore.Web.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmlakCore.Web.Controllers
{
    [Authorize(Roles = "Yetkili")]
    public class AdminController : Controller
    {
        private readonly IService _service;
        private readonly IMapper _mapper;
        private readonly IdecodeTokenService _decodeTokenService;
        public AdminController(IService service, IMapper mapper,IdecodeTokenService decodeTokenService)
        {
            _service = service;
            _mapper = mapper;
            _decodeTokenService = decodeTokenService;
        }
      
        public IActionResult Index()
        {
            AccessToken accessToken = _decodeTokenService.GetAccessToken();
            string musteriNo = _decodeTokenService.MusteriNo(accessToken.Token);
            HttpResponseMessage yetkiliProfilResponce = _service.Get("Yetkili/GetYetkiliProfil", accessToken);
            if (yetkiliProfilResponce.IsSuccessStatusCode)
            {
                YetkiliGetProfilResource yetkiliGetProfilResource = yetkiliProfilResponce.Content.ReadAsAsync<YetkiliGetProfilResource>().Result;
                return View(_mapper.Map<YetkiliProfilViewModel>(yetkiliGetProfilResource));
            }
            return View();
         
        }
      
        public IActionResult Ilanlar()
        {
            HttpResponseMessage emlkalarReponce = _service.Get("Emlak/GetAllEmlak");
            if (emlkalarReponce.IsSuccessStatusCode)
            {
                List<GetAllEmlakResource> emlaklar = emlkalarReponce.Content.ReadAsAsync<List<GetAllEmlakResource>>().Result;
                List<EmlaklarModel> model = _mapper.Map<List<EmlaklarModel>>(emlaklar);
                return View(model);
            }
            return View();
        }
     
        public IActionResult IlanSil(int ilanID)
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult KayitOl(YetkiliKayitOlModel model)
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
            HttpResponseMessage kayitOlMessage = _service.Post("Yetkili/Kayitol", model);
            if (kayitOlMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GirisYap","Kullanici");
            }
            return View();
        }
    }
}
