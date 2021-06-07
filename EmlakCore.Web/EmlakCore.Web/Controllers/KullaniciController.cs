using EmlakCore.Web.Models;
using EmlakCore.Web.Services;
using EmlakCore.Web.Tools;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmlakCore.Web.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly IService _service;
        private readonly IidenttyService _identtyService;
        public KullaniciController(IService service, IidenttyService identtyService)
        {
            _service = service;
            _identtyService = identtyService;
        }
        public IActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GirisYap(GirisYapModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData.Add("Mesasj", "Lütfen Alanları Kurallara Uygun Doldurun");
                return View();
            }
            HttpResponseMessage girisYapResponce = _service.Post("Kullanici/GirisYap", model);
            if (girisYapResponce.IsSuccessStatusCode)
            {
                string token = girisYapResponce.Content.ReadAsStringAsync().Result;
                AccessToken deserilizeToken = JsonConvert.DeserializeObject<AccessToken>(token);
                if (_identtyService.GirisYap(deserilizeToken))
                    return RedirectToAction("Yonlendir", new RouteValueDictionary(
                                      new { controller = "Kullanici", action = "Yonlendir", returnUrl = model.ReturnUrl }));
            }
            return View();
        }
        public IActionResult CikisYap()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index","AnaSayfa");
        }
        public IActionResult Yonlendir(string returnUrl)
        {

            if (returnUrl.Contains("ReturnUrl"))
                returnUrl = returnUrl.Split("ReturnUrl")[1].Substring(1, returnUrl.Split("ReturnUrl")[1].Length - 1);
            if (returnUrl.Contains("?"))
            {
                return Redirect(returnUrl);
            }
            else
            {
                string[] donUrl = returnUrl.Split("/");
                if (donUrl.Length == 4)
                {
                    return Redirect(returnUrl);

                }
                else if (donUrl.Length == 3)
                    return RedirectToAction(donUrl[2], donUrl[1]);
                else return RedirectToAction("Index", "AnaSayfa");
            }
        }
    }
}
