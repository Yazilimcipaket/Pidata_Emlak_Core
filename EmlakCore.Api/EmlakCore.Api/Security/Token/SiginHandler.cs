using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
namespace EmlakCore.Api.Security.Token
{
    public class SiginHandler
    {
        public static SecurityKey GetSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
