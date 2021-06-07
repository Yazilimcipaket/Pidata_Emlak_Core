using AutoMapper;
using EmlakCore.Web.Models;
using EmlakCore.Web.Resource;
using EmlakCore.Web.Services;
using EmlakCore.Web.Tools;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.PowerBI.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmlakCore.Web.Controllers
{
    public class AnaSayfaController : Controller
    {
        private IService _service;
        private IidenttyService _IidenttyService;
        private IMapper _mapper;
        public AnaSayfaController(IService service, IidenttyService IidenttyService, IMapper mapper)
        {
            _service = service;
            _IidenttyService = IidenttyService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            HttpResponseMessage emlkalarReponce = _service.Get("Emlak/GetAllEmlak");
            HttpResponseMessage emlakTurleriResponce = _service.Get("Emlak/GetAllEmlakTur");
            AnaSayfaModel model = new AnaSayfaModel();
            if (emlkalarReponce.IsSuccessStatusCode&emlakTurleriResponce.IsSuccessStatusCode)
            {
                List<EmlakTurleriResource> resource = emlakTurleriResponce.Content.ReadAsAsync<List<EmlakTurleriResource>>().Result;
                List<GetAllEmlakResource> emlaklar = emlkalarReponce.Content.ReadAsAsync<List<GetAllEmlakResource>>().Result;
                model.Emlaklar= _mapper.Map<List<EmlaklarModel>>(emlaklar);
                model.EmlakTurleri= _mapper.Map<List<EmlakTurleriModel>>(resource);
                return View(model);
            }
            return View();
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
