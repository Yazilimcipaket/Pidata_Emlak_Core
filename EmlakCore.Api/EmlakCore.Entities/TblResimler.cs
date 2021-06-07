using EmlakCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmlakCore.Entities
{
  public  class TblResimler:IEntity
    {
        [Key]
        public int ResimID { get; set; }
        public string ResimYol { get; set; }
        public string PublicID { get; set; }

    }
}
