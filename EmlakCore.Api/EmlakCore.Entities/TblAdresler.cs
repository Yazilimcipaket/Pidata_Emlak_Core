using EmlakCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmlakCore.Entities
{
   public class TblAdresler:IEntity
    {
        [Key]
        public int AdresID { get; set; }
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string PostaCode { get; set; }
        public string AdresDetay { get; set; }

    }
}
