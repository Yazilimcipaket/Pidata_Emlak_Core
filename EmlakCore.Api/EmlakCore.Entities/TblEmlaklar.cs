using EmlakCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmlakCore.Entities
{
    public class TblEmlaklar : IEntity
    {
        [Key]
        public int EmlakID { get; set; }
        public int EmlakTuru { get; set; }
        public int Metrekare { get; set; }
        public int OdaSayisi { get; set; }
        public int Kat { get; set; }
        public string IsinmaTuru { get; set; }
        public int Adres { get; set; }
        public int Sahibi { get; set; }
    }
}
