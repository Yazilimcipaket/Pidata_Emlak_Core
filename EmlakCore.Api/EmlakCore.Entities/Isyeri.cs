using EmlakCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmlakCore.Entities
{
  public  class Isyeri:IEntity
    {
        [Key]
        public int IsyeriID { get; set; }
        public string IsyeriAdi { get; set; }
        public int Yetkili { get; set; }
        public int AdresID { get; set; }
        public string TelefonNo { get; set; }
        public string Fax { get; set; }

    }
}
