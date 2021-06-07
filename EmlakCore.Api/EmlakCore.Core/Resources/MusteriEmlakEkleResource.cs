using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakCore.Core.Resources
{
   public class MusteriEmlakEkleResource
    {
        public int EmlakTuru { get; set; }
        public string Metrekare { get; set; }
        public string OdaSayisi { get; set; }
        public string Kat { get; set; }
        public string IsinmaTuru { get; set; }
        public string AdresIl { get; set; }
        public string AdresIlce { get; set; }
        public string AdresDetay { get; set; }
        public string Tip { get; set; }
        public decimal Fiyat { get; set; }
        public string KullaniciNo{ get; set; }
    }
}
