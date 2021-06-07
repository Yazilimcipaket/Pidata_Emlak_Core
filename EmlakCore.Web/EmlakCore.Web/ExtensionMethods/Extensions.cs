using EmlakCore.Web.Tools;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace EmlakCore.Web.ExtensionMethods
{
    public static class Extensions
    {
        public static bool UpdateAccesToken(this IPrincipal currentPrincipal, AccessToken accessToken)
        {

            var identity = currentPrincipal.Identity as ClaimsIdentity;
            var existingClaimToken = identity.FindFirst("Token");
            var existingClaimRefreshToken = identity.FindFirst("RefreshToken");
            if (existingClaimRefreshToken != null && existingClaimToken != null)
            {
                identity.RemoveClaim(existingClaimToken);
                identity.RemoveClaim(existingClaimRefreshToken);
            }
            identity.AddClaim(new Claim("Token", accessToken.Token));
            identity.AddClaim(new Claim("RefreshToken", accessToken.RefreshToken));
            HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            var principal = new ClaimsPrincipal(identity);
            Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignInAsync(httpContextAccessor.HttpContext,
                CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return true;
        }
    }
}
