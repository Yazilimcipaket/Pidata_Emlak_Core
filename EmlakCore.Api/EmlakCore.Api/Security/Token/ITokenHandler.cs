using EmlakCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Api.Security.Token
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(Kullaniciler kullanici);
        void RemoveRefreshToken(Kullaniciler kullanici);
    }
}
