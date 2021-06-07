using EmlakCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmlakCore.Entities
{
  public  class EmlakTurleri:IEntity
    {
        [Key]
        public int EmlakTurID { get; set; }
        public string EmlakTurAdi { get; set; }

    }
}
