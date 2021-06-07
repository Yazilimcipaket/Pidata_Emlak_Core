using EmlakCore.Business.Abstract;
using EmlakCore.Core.Dtos;
using EmlakCore.Core.Resources;
using EmlakCore.Entities;
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
    public class EmlakController : ControllerBase
    {
        private readonly IEmlakService _emlakService;
        public EmlakController(IEmlakService emlakService)
        {
            _emlakService = emlakService;
        }
        public IActionResult GetAllEmlakTur()
        {
            return Ok(_emlakService.GetAllEmlakTur());
        }
        public IActionResult GetAllEmlak()
        {
            return Ok(_emlakService.GetAllEmlak());
        }
       
        [HttpPost]
        public IActionResult EmlakAra(EmlakAraResource resource)
        {
            List<EmlaklarDto> dtos = _emlakService.EmlakAra(resource);
            return Ok(dtos);
        }
    }
}
