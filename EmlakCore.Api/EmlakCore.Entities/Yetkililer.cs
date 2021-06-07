using EmlakCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmlakCore.Entities
{
   public class Yetkililer:IEntity
    {
        [Key]
        public int YetkiliID { get; set; }
        public string YetkiliAd { get; set; }
        public string YetkiliSoyad { get; set; }

    }
}
