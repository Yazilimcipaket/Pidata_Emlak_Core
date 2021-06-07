using EmlakCore.Business.Abstract;
using EmlakCore.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmlakCore.Api.Security.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly TokenOptions tokenOptions;
        private IKullaniciService _kullaniciService;
   
        public TokenHandler(IKullaniciService kullaniciService, IOptions<TokenOptions> tokenOptions)
        {
            _kullaniciService = kullaniciService;
            this.tokenOptions = tokenOptions.Value;
        }
        public AccessToken CreateAccessToken(TblKullaniciler kullanici)
        {
            var accessTokenExpiration = DateTime.Now.AddDays(tokenOptions.AccessTokenExpiration);
            var refrehTokenExpiration = DateTime.Now.AddDays(tokenOptions.RefreshTokenExpiration);
            var securityKey = SiginHandler.GetSecurityKey(tokenOptions.SecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: accessTokenExpiration,
                claims: GetClaims(kullanici),
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            AccessToken accessToken = new AccessToken();
            accessToken.Token = token;
            accessToken.RefreshToken = CreateRefreshToken();
            accessToken.Expiration = refrehTokenExpiration;
            return accessToken;
        }
        private string CreateRefreshToken()
        {
            var numberByte = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(numberByte);
                return Convert.ToBase64String(numberByte);
            }
        }
        private IEnumerable<Claim> GetClaims(TblKullaniciler kullanici)
        {

            string role = "";

            if (kullanici != null && kullanici.KullaniciNo.Substring(0, 1) == "3")
                role = "Yetkili";
            else if (kullanici != null && kullanici.KullaniciNo.Substring(0, 1) == "7")
                role = "Musteri";
            else
                role = "Yetkisiz";
            var claims = new List<Claim>
            {
                new Claim("KullaniciNo",kullanici.KullaniciNo.ToString()),
                new Claim("Eposta", kullanici.Eposta),
                new Claim(ClaimTypes.Role,role)
            };
            return claims;
        }
        public void RemoveRefreshToken(TblKullaniciler kullanici)
        {
            throw new NotImplementedException();
        }
    }
}
