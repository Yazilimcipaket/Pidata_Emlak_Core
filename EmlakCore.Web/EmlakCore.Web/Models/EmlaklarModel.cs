using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Web.Models
{
    public class EmlaklarModel
    {
        public EmlaklarModel()
        {
            Resimler = new List<string>();
        }
        public bool Kiralik { get; set; }
        public decimal Fiyat { get; set; }
        public string EmlakTuru { get; set; }
        public int MetreKare { get; set; }
        public int OdaSayisi { get; set; }
        public int Kat { get; set; }
        public string IsinmaTuru { get; set; }
        public string AdresIl { get; set; }
        public string AdresIlce { get; set; }
        public string AdresDetay { get; set; }
        public string IlanSahibiAdi { get; set; }
        public string IlanSahibiSoyadi { get; set; }
        public string IlanSahibiTelefonu { get; set; }
        public List<string> Resimler { get; set; }
    }
}
