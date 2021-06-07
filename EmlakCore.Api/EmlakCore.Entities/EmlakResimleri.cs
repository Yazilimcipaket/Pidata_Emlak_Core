using EmlakCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmlakCore.Entities
{
   public class EmlakResimleri:IEntity
    {
        [Key]
        public int EmlakID { get; set; }
        [Key]
        public int ResimID { get; set; }

    }
}
