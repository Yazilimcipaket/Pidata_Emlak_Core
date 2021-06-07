using EmlakCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmlakCore.Entities
{
   public class SatilikEmlaklar:IEntity
    {
        [Key]
        public int SatilikEmlakID { get; set; }
        public decimal Ucret { get; set; }
    }
}
