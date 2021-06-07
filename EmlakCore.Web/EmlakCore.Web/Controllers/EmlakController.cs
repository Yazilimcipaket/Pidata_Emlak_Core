using AutoMapper;
using EmlakCore.Web.Models;
using EmlakCore.Web.Resource;
using EmlakCore.Web.Services;
using EmlakCore.Web.StaticClasses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmlakCore.Web.Controllers
{
    public class EmlakController : Controller
    {
        private readonly IService _service;
        private readonly IMapper _mapper;
        public EmlakController(IService service,IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult EmlakAra(EmlakAraModel model)
        {
            //[0-300]sqft
            //[ 0 - 500 ] $ fiyat
            model.AltMetreKare = Convert.ToInt32(model.Metrekare.Split('[')[1].Split(']')[0].Split('-')[0]);
            model.UstMetreKare = Convert.ToInt32(model.Metrekare.Split('[')[1].Split(']')[0].Split('-')[1]);
            model.AltFiyat = Convert.ToDecimal(model.Fiyat.Split('[')[1].Split(']')[0].Split('-')[0]);
            model.UstFiyat = Convert.ToDecimal(model.Fiyat.Split('[')[1].Split(']')[0].Split('-')[1]);
            HttpResponseMessage emlaklarResponce = _service.Post("Emlak/EmlakAra", model);
            if (emlaklarResponce.IsSuccessStatusCode)
            {
                List<GetAllEmlakResource> resource = emlaklarResponce.Content.ReadAsAsync<List<GetAllEmlakResource>>().Result;
                EmlakSonucCiktiAl.Emlaklar = _mapper.Map<List<EmlaklarModel>>(resource);
                return View(_mapper.Map<List<EmlaklarModel>>(resource));
            }
            return View();
        }
       
    }
}
