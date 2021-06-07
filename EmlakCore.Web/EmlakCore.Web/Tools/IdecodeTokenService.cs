using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Web.Tools
{
    public interface IdecodeTokenService
    {
        string MusteriNo(string Token);
        string Eposta(string Token);
        AccessToken GetAccessToken();
    }
}
