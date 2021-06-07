using EmlakCore.Web.ExtensionMethods;
using EmlakCore.Web.Tools;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EmlakCore.Web.Services
{
    public class EmlakCoreService : IService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public EmlakCoreService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        //https://localhost:44345/api/
        private string BaseAdres= "https://emlakcoreapi.paketpatron.com/api/";
        //https://emlakcoreapi.paketpatron.com/api/

        public HttpResponseMessage Get(string adres)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAdres);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Response = client.GetAsync(adres).Result;
                return Response;
            }
        }

        public HttpResponseMessage Get(string adres, AccessToken accessToken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAdres);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);
                HttpResponseMessage Response = client.GetAsync(adres).Result;
                if (Response.StatusCode.ToString() == System.Net.HttpStatusCode.Unauthorized.ToString())
                    if (Post(accessToken.RefreshToken))
                    {
                        return Get(adres, new AccessToken
                        {
                            Token = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Token").Value,
                            RefreshToken = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "RefreshToken").Value
                        });
                    }
                return Response;
            }
        }

        public HttpResponseMessage Post<T>(string adres, T entity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAdres);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                HttpResponseMessage mesaj = client.PostAsJsonAsync(adres, entity).Result;

                return mesaj;
            }
        }

        public HttpResponseMessage Post<T>(string adres, T entity, AccessToken accessToken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAdres);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);
                HttpResponseMessage mesaj = client.PostAsJsonAsync(adres, entity).Result;
                if (mesaj.StatusCode.ToString() == System.Net.HttpStatusCode.Unauthorized.ToString())
                    if (Post(accessToken.RefreshToken))
                    {
                        return Post<T>(adres, entity, new AccessToken
                        {
                            Token = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Token").Value,
                            RefreshToken = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "RefreshToken").Value
                        });
                    }
                return mesaj;
            }
        }

        public HttpResponseMessage Post(string adres, IFormFile file, int id, AccessToken accessToken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAdres);
                byte[] data;
                using (var br = new BinaryReader(file.OpenReadStream()))
                    data = br.ReadBytes((int)file.OpenReadStream().Length);

                ByteArrayContent bytes = new ByteArrayContent(data);
                HttpContent content = new StringContent(id.ToString(), Encoding.UTF8, "aplication/json");
                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);
                multiContent.Add(bytes, "Resim", file.FileName);
                multiContent.Add(content, "EmlakID");

                var result = client.PostAsync(adres, multiContent).Result;
                if (result.StatusCode.ToString() == System.Net.HttpStatusCode.Unauthorized.ToString())
                    if (Post(accessToken.RefreshToken))
                    {
                        return Post(adres, file, id, new AccessToken
                        {
                            Token = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Token").Value,
                            RefreshToken = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "RefreshToken").Value
                        });
                    }
                return result;
            }
        }

        public HttpResponseMessage Post(string adres, int id, AccessToken accessToken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAdres);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);
                HttpResponseMessage mesaj = client.PostAsJsonAsync(adres, id).Result;
                if (mesaj.StatusCode.ToString() == System.Net.HttpStatusCode.Unauthorized.ToString())
                    if (Post(accessToken.RefreshToken))
                    {
                        return Post(adres, id, new AccessToken
                        {
                            Token = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Token").Value,
                            RefreshToken = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "RefreshToken").Value
                        });
                    }

                return mesaj;
            }
        }
        private bool Post(string refreshToken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAdres);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                HttpResponseMessage mesaj = client.PostAsJsonAsync("Kullanici/RefreshToken/", refreshToken).Result;
                if (mesaj.IsSuccessStatusCode)
                {
                    AccessToken accessToken = mesaj.Content.ReadAsAsync<AccessToken>().Result;
                    if (_httpContextAccessor.HttpContext.User.UpdateAccesToken(accessToken))
                        return true;
                }
                return false;
            }
        }
    }
}
