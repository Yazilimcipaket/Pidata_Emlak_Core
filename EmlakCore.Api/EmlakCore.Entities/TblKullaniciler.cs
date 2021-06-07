using EmlakCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmlakCore.Entities
{
   public class TblKullaniciler:IEntity
    {
        [Key]
        public int KullaniciID { get; set; }
        public string Eposta { get; set; }
        public string Sifre { get; set; }
        public string KullaniciNo { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenEndDate { get; set; }

    }
}
