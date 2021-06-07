using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Web.Models
{
    public class EmlakAraModel
    {
        public bool Kiralik { get; set; }
        public decimal AltFiyat { get; set; }
        public decimal UstFiyat { get; set; }
        public string Fiyat { get; set; }
        public int EmlakTuru { get; set; }
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string PotaKod { get; set; }
        public string Metrekare { get; set; }
        public int AltMetreKare { get; set; }
        public int UstMetreKare { get; set; }
    }
}
