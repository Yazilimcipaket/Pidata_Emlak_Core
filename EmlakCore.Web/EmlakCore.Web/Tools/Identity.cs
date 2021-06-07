using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmlakCore.Web.Tools
{
    public class Identity : IidenttyService
    {
        private IdecodeTokenService _decodeTokenService;

        private IHttpContextAccessor _httpContextAccessor;
        public Identity(IdecodeTokenService decodeTokenService, IHttpContextAccessor httpContextAccessor)
        {
            _decodeTokenService = decodeTokenService;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool GirisYap(AccessToken accessToken)
        {
            try
            {
                string musteriTip = _decodeTokenService.MusteriNo(accessToken.Token);
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                if (musteriTip.Substring(0, 1) == "7")
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Musteri"));
                else if (musteriTip.Substring(0, 1) == "3")
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Yetkili"));
                else
                    return false;
                identity.AddClaim(new Claim("Token", accessToken.Token));
                identity.AddClaim(new Claim("RefreshToken", accessToken.RefreshToken));
                identity.AddClaim(new Claim("Eposta", _decodeTokenService.Eposta(accessToken.Token)));
                var principal = new ClaimsPrincipal(identity);
                Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignInAsync(_httpContextAccessor.HttpContext, CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
