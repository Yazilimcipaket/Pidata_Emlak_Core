using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Web.Models
{
    public class AnaSayfaModel
    {
        public AnaSayfaModel()
        {
            EmlakTurleri = new List<EmlakTurleriModel>();
            Emlaklar = new List<EmlaklarModel>();
        }
        public List<EmlakTurleriModel> EmlakTurleri { get; set; }
        public List<EmlaklarModel> Emlaklar { get; set; }
    }
}
