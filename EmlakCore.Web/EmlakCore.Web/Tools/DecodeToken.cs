using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmlakCore.Web.Tools
{
    public class DecodeToken:IdecodeTokenService
    {
        //session nesnesi controller odaklıdır
        // aşşağıdaki kodları yazmasak httpcontexti kullanamazdık
        private IHttpContextAccessor _httpContextAccessor;
        public DecodeToken(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string MusteriNo(string Token)
        {
            string secret = "[burdaki](sifreyi)(ben)(aksam)(kendi)(sifreleme)(metodumla)[sifrelicem].";
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = "oisbizde",
                ValidAudience = "oisbizde"
            };
            var claims = handler.ValidateToken(Token, validations, out var tokenSecure).Claims;
            return claims.FirstOrDefault(x => x.Type == "KullaniciNo").Value;
        }
        public string Eposta(string Token)
        {
            string secret = "[burdaki](sifreyi)(ben)(aksam)(kendi)(sifreleme)(metodumla)[sifrelicem].";
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = "oisbizde",
                ValidAudience = "oisbizde"
            };
            var claims = handler.ValidateToken(Token, validations, out var tokenSecure).Claims;
            return claims.FirstOrDefault(x => x.Type == "Eposta").Value;
        }
        public AccessToken GetAccessToken()
        {
            return new AccessToken
            {
                Token = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Token").Value,
                RefreshToken = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "RefreshToken").Value
            };
        }
    }
}

