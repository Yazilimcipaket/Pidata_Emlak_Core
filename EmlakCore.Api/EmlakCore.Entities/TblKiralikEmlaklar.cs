using EmlakCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmlakCore.Entities
{
   public class TblKiralikEmlaklar:IEntity
    {
        [Key]
        public int KiralikEmlakID { get; set; }
        public decimal Ucret { get; set; }
    }
}
