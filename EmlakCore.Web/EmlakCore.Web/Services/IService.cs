using EmlakCore.Web.Tools;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmlakCore.Web.Services
{
    public interface IService
    {
        HttpResponseMessage Post<T>(string adres, T entity);
        HttpResponseMessage Post<T>(string adres, T entity, AccessToken accessToken);
        HttpResponseMessage Post(string adres, IFormFile file, int id, AccessToken accessToken);
        HttpResponseMessage Post(string adres, int id, AccessToken accessToken);
        HttpResponseMessage Get(string adres);
        HttpResponseMessage Get(string adres, AccessToken accessToken);
    }
}
