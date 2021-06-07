using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakCore.Core.Resources
{
    public class EmlakResimEkleResource
    {
        public IFormFile Resim { get; set; }
        public int EmlakID { get; set; }
    }
}
