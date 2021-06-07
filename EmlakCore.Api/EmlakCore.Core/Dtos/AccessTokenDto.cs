using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakCore.Core.Dtos
{
   public class AccessTokenDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
