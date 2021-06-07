using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Web.Models
{
    public class EmlakResimEkleModel
    {
        public IFormFile Resim { get; set; }
        public int EmlakID { get; set; }
    }
}
