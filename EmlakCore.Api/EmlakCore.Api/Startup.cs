using EmlakCore.Api.Security.Token;
using EmlakCore.Business.Abstract;
using EmlakCore.Business.Concrete;
using EmlakCore.Business.Helpers;
using EmlakCore.DataAccsess.Abstract;
using EmlakCore.DataAccsess.Concrete.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakCore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //Veri Eriþimi
            services.AddScoped<IMusteriService, MusteriManager>();
            services.AddScoped<IMusterilerDal, EfMusterilerDal>();
            services.AddScoped<IKullaniciService, KullaniciManager>();
            services.AddScoped<IKullanicilarDal, EfKullanicilarDal>();
            services.AddScoped<IEmlakTurleriDal, EfEmlakTurlariDal>();
            services.AddScoped<IEmlakService, EmlakManager>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IEmlaklarDal, EfEmlaklarDal>();
            services.AddScoped<IAdreslerDal, EfAdreslerDal>();
            services.AddScoped<IKiralikEmlaklarDal, EfKiralikEmlaklarDal>();
            services.AddScoped<ISatilikEmlaklarDal, EfSatilikEmlaklarDal>();
            services.AddScoped<IResimlerDal, EfResimlerDal>();
            services.AddScoped<IEmlakResimleriDal, EfEmlakResimleriDal>();
            services.AddScoped<IYetkiliService, YetkiliManager>();
            services.AddScoped<IYetkililerDal, EfYetkililerDal>();
            services.AddScoped<IIsyeriDal, EfIsyeriDal>();
            TokenOptions tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = SiginHandler.GetSecurityKey(tokenOptions.SecurityKey),
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors(x => x.AllowAnyMethod());
        }

    }
}
